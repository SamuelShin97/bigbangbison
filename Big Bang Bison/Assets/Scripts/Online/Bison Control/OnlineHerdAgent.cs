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
public class OnlineHerdAgent : NetworkBehaviour
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
    OnlineHerd agentHerd;
    public OnlineHerd AgentHerd
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

    // Save my Rigidbody and Animator
    public Rigidbody agentBody;
    private Animator agentAnimator;

    // 0 - Idle, 1 - running, 2 - panicking, 3 - Off ground
    [SyncVar]
    public byte state = 0;

    // whether or not bison are in a hill
    public bool inHill = false;

    // whether or not bison are in the box
    // Bison box
    public bool inHerdBox = false;

    //how much the bison have grown
    [SyncVar]
    public float growth;
    public bool scoreable;
    public float mediumMaturity = 5f;
    public float fullMaturity = 15f;
    public Light glow;
    [SerializeField]
    private float idleTimer = 0;
    ParticleSystem fullGrowthPfx;
    private bool startedBisonPfx;

    // Save my rigidbody
    private void Awake()
    {
        agentBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        growth = 0f;
        scoreable = true;
        agentCollider = GetComponent<Collider>(); // save my collider
        agentAnimator = GetComponentInChildren<Animator>();
        //glow = gameObject.GetComponentInChildren<Light>();
        glow.gameObject.SetActive(false);
        fullGrowthPfx = GetComponentInChildren<ParticleSystem>();
        startedBisonPfx = false;
        soundMidPlay = FMODUnity.RuntimeManager.CreateInstance(soundMid);
        soundFullPlay = FMODUnity.RuntimeManager.CreateInstance(soundFull);
        sound = false;

    }

    // Called once when created
    public void Initialize(OnlineHerd herd)
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
        if (growth >= mediumMaturity)
        {
            LetThereBeLight();
            
        }
        if (growth >= fullMaturity)
        {
            PlayPfx();
           
        }

    }

    // Move on clients
    [ClientRpc]
    public void RpcMove(Vector3 velocity)
    {
        agentBody.AddForce(velocity * Time.deltaTime, ForceMode.Impulse);
        if (agentBody.velocity.sqrMagnitude > 150)  // caps speed
        {
            //agentBody.AddForce(-agentBody.velocity.normalized * (agentBody.velocity.magnitude - 22), ForceMode.Impulse);
            agentBody.velocity = agentBody.velocity.normalized * 12;
        }
        if (agentBody.velocity.sqrMagnitude > 1) transform.forward = agentBody.velocity; // face the direction I'm moving
    }

    // Move it
    public void Move(Vector3 velocity)
    {
        agentBody.AddForce(velocity * Time.deltaTime, ForceMode.Impulse);
        if (agentBody.velocity.sqrMagnitude > 150)  // caps speed
        {
            //agentBody.AddForce(-agentBody.velocity.normalized * (agentBody.velocity.magnitude - 22), ForceMode.Impulse);
            agentBody.velocity = agentBody.velocity.normalized * 12;
        }
        if (agentBody.velocity.sqrMagnitude > 1) transform.forward = agentBody.velocity; // face the direction I'm moving
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
        agentBody.drag = agentHerd.runningDrag;
        //Debug.Log(agent.gameObject.name + ": droving");
        //Debug.Log(timer);
        while (timer > 0 && !floating)
        {
            if (inHerdBox)
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
            agentBody.drag = agentHerd.idleDrag;
        }
    }

    // this coroutine runs with each agent that's near the player, keeps bison running for idleTimer seconds after it gets away from them
    IEnumerator Floating()
    {
        float timer = agentHerd.lostTimer;
        Debug.Log("and I oop");
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
                break;
            }
        }
        //Debug.Log(gameObject.name + ": idle");
        state = 0;
        agentBody.angularDrag = 0.5f;
        agentBody.drag = agentHerd.idleDrag;
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
