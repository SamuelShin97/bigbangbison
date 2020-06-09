/*
    HerdAgent.cs
    Caetano 
    3/23/20
    Caetano
    Class for individual bison
    Functions in file:
        .AgentCollider: Out, this' collider
        .AgentHerd: Out, the herd this is in
        .Drove: Out, start droving coroutine
        .Float: Out, start floating coroutine
        .Initialize: In, Herd - Out, Sets the bison's herd to Herd
        .Move: In, velocity - Out, moves this
*/
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This would've be called the Bison class if that didn't fit the naming convention, the class for individual bison
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HerdAgent : MonoBehaviour
{
    //Sound stuff
    [FMODUnity.EventRef]
    public string soundMid;
    [FMODUnity.EventRef]
    public string soundFull;
    FMOD.Studio.EventInstance soundMidPlay;
    FMOD.Studio.EventInstance soundFullPlay;
    private bool sound;

    // Return my herd
    Herd agentHerd;
    public Herd AgentHerd
    {
        get { return agentHerd; }
    }

    // Am I being herded, near the player
    public bool nearPlayer = false;

    // Am I floating, out of play
    public bool floating = false;

    // Return my collider
    Collider agentCollider;
    public Collider AgentCollider {
        get { return agentCollider; }
    }

    public Material Stage0 { get => Stage0; set => Stage0 = value; }

    // Save my Rigidbody and Animator
    public Rigidbody agentBody;
    private Animator agentAnimator;

    // 0 - Idle, 1 - running, 2 - panicking, 3 - Off ground
    public byte state = 0;

    // whether or not bison are in a hill
    public bool inHill = false;

    // Bison box
    public int inHerdBox = 0; // the number of friendly boxes the bison are in
    public float boxPressure = 0;

    //how much the bison have grown
    public float growth;
    public bool scored;
    public float startMaturity = 1f;
    public float mediumMaturity = 5f;
    public float fullMaturity = 10f;
    public Light glow;
    Renderer[] stage0;
    public Material stage1;
    public Material stage50;
    public Material stage100;
    [SerializeField]
    private float idleTimer = 0;
    ParticleSystem fullGrowthPfx;
    private bool startedBisonPfx;

    //this is for removing unused bison
    public bool hasMoved;
    public bool readyToRemove;

    // Start is called before the first frame update
    void Start()
    {
        growth = 0f;
        scored = false;
        agentCollider = GetComponent<Collider>(); // save my collider
        agentBody = GetComponent<Rigidbody>();
        agentAnimator = GetComponentInChildren<Animator>();
        //glow = gameObject.GetComponentInChildren<Light>();
        glow.gameObject.SetActive(false);
        fullGrowthPfx = GetComponentInChildren<ParticleSystem>();
        startedBisonPfx = false;
        stage0 = GetComponentsInChildren<SkinnedMeshRenderer>();
        soundMidPlay = FMODUnity.RuntimeManager.CreateInstance(soundMid);
        soundFullPlay = FMODUnity.RuntimeManager.CreateInstance(soundFull);
        sound = false;
        hasMoved = false;
        readyToRemove = false;

    }

    // Called once when created
    public void Initialize(Herd herd)
    {
        agentHerd = herd; // save my herd
    }

    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundMidPlay, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundFullPlay, GetComponent<Transform>(), GetComponent<Rigidbody>());
        agentAnimator.SetInteger("State", state);
        agentAnimator.SetFloat("Speed", agentBody.velocity.magnitude);
        if (state == 0) agentAnimator.SetFloat("Idle", Random.value);
        if (growth >= startMaturity)
        {
            for (int i = 0; i < stage0.Length; i++)
            {
                stage0[i].material = stage1;
            }
            

        }
        // Particles when scoreable
        if (growth >= mediumMaturity)
        {
            for (int i = 0; i < stage0.Length; i++)
            {
                stage0[i].material = stage50;
            }
            PlayPfx();

        }
         // Light at full maturity
        if (growth >= fullMaturity)
        {
            for (int i = 0; i < stage0.Length; i++)
            {
                stage0[i].material = stage100;
            }
            LetThereBeLight();

        }
    }

    // Move it
    public void Move(Vector3 velocity)
    {
        agentBody.AddForce(velocity * Time.deltaTime * 100, ForceMode.Acceleration );
        if (agentBody.velocity.sqrMagnitude > 150)  // caps speed
        {
            //agentBody.AddForce(-agentBody.velocity.normalized * (agentBody.velocity.magnitude - 22), ForceMode.Impulse);
            //agentBody.velocity = agentBody.velocity.normalized * 12;
        }
        if (agentBody.velocity.sqrMagnitude > 0.5) transform.forward = agentBody.velocity; // face the direction I'm moving

        // reset box pressure after move
        boxPressure = 0;
    }

    public bool Drove()
    {
        if (state == 0)
        {
            StartCoroutine(Droving());
            return true;
        }
        return false;
    }

    public bool Float()
    {
        if (floating)
        {
            StartCoroutine(Floating());
            return true;
        }
        return false;
    }

    // this coroutine runs with each agent that's droving, keeps bison running for idleTimer seconds after the player gets rid of their herdbox
    IEnumerator Droving()
    {
        float timer = agentHerd.idleTimer;
        state = 1;
        agentBody.drag = agentHerd.drag;
        //Debug.Log(agent.gameObject.name + ": droving");
        //Debug.Log(timer);
        while (timer > 0 && !floating)
        {
            if (inHerdBox > 0)
            {
                timer = agentHerd.idleTimer;
                idleTimer = timer;
                yield return null; // if near the player then reset the timer and continue
                //Debug.Log(timer + "reset");
            }
            else if (inHill && !(growth >= fullMaturity))
            {
                break; // if I'm in a hill and not in a box, go idle
            }
            else
            {
                timer -= Time.deltaTime;
                idleTimer = timer;
                yield return null;
            }
        }
        //Debug.Log(gameObject.name + ": idle");
        if (!floating)
        {
            state = 0;
            agentBody.drag = agentHerd.drag;
        }
    }

    // this coroutine runs with each agent that's floating, keeps bison will flag bison to be destroyed after "lostTimer" seconds
    IEnumerator Floating()
    {
        float timer = agentHerd.lostTimer;
        //Debug.Log("and I oop");
        state = 3;
        agentBody.angularDrag = 0;
        //Debug.Log(agent.gameObject.name + ": droving");
        while (timer > 0)
        {
            if (floating)
            {
                timer -= Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * 5), transform.position.z);
                yield return null; // if still offstage then continue
            }
            else
            {
                // no longer offstage, go idle
                state = 0;

                agentBody.angularDrag = 0.5f;
                agentBody.drag = agentHerd.drag;
                yield break;
            }
        }
        if (state == 3) {
            // flag to be destroyed
            state = 4;
        }
    }

    void LetThereBeLight()
    {
        glow.gameObject.SetActive(true);
        if (!sound)
        {
            soundMidPlay.start();
            sound = true;
        }
    }

    void PlayPfx()
    {
        if (!startedBisonPfx)
        {
            soundFullPlay.start();
            fullGrowthPfx.Play();
            startedBisonPfx = true;
        }
        
        
        //Debug.Log(fullGrowthPfx.isPlaying);
    }
    void PlaySoundMid()
    {
        soundMidPlay.start();
    }
    void PlaySoundFull()
    {
        soundFullPlay.start();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (agentBody != null) Gizmos.DrawLine(transform.position, transform.position + transform.forward * agentBody.velocity.magnitude);
    }

}
