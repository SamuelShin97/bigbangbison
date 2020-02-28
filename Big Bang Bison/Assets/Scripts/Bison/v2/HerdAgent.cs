/*
    HerdAgent.cs
    Caetano 
    1/27/20
    Caetano
    Class for individual bison
    Functions in file:
        .AgentHerd: Out, the herd this is in
        .AgentCollider: Out, this' collider
        .Move: In, velocity - Out, moves this
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This would've be called the Bison class if that didn't fit the naming convention, the class for individual bison
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HerdAgent : MonoBehaviour
{
    // Return my herd
    Herd agentHerd;
    public Herd AgentHerd
    {
        get { return agentHerd; }
    }

    // Am I being herded, near the player
    public bool nearPlayer = false;

    // Return my collider
    Collider agentCollider;
    public Collider AgentCollider {
        get { return agentCollider; }
    }

    // Save my Rigidbody and Animator
    public Rigidbody agentBody;
    private Animator agentAnimator;

    // 0 - Idle, 1 - running, 2 - panicking, 3 - Off ground
    public byte state = 0;

    //how much the bison have grown
    public float growth;
    public bool scoreable;
    public float mediumMaturity;
    public float fullMaturity;
    Light glow;
    ParticleSystem fullGrowthPfx;
    private bool startedBisonPfx;

    // Start is called before the first frame update
    void Start()
    {
        growth = 0f;
        scoreable = true;
        agentCollider = GetComponent<Collider>(); // save my collider
        agentBody = GetComponent<Rigidbody>();
        agentAnimator = GetComponentInChildren<Animator>();
        glow = gameObject.GetComponentInChildren<Light>();
        glow.gameObject.SetActive(false);
        fullGrowthPfx = GetComponentInChildren<ParticleSystem>();
        startedBisonPfx = false;

    }

    // Called once when created
    public void Initialize(Herd herd)
    {
        agentHerd = herd; // save my herd
    }

    void Update()
    {
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

    // Move it
    public void Move(Vector3 velocity)
    {
        agentBody.AddForce(velocity * Time.deltaTime, ForceMode.Impulse);
        //if (agentBody.velocity.sqrMagnitude > 100)  // caps speed
        //{
        //    agentBody.AddForce(-agentBody.velocity.normalized * (agentBody.velocity.magnitude - 10), ForceMode.Impulse);
        //}
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

    // this coroutine runs with each agent that's near the player, keeps bison running for idleTimer seconds after it gets away from them
    IEnumerator Droving()
    {
        float timer = agentHerd.idleTimer;
        state = 1;
        agentBody.drag = agentHerd.runningDrag;
        //Debug.Log(agent.gameObject.name + ": droving");
        //Debug.Log(timer);
        while (timer > 0)
        {
            if (nearPlayer)
            {
                timer = agentHerd.idleTimer;
                yield return null; // if near the player then reset the timer and continue
                //Debug.Log(timer + "reset");
            }
            else
            {
                timer -= Time.deltaTime;
                //Debug.Log(timer);
                yield return null;
            }
        }
        //Debug.Log(gameObject.name + ": idle");
        state = 0;
        agentBody.drag = agentHerd.idleDrag;
    }

    void LetThereBeLight()
    {
        glow.gameObject.SetActive(true);
    }

    void PlayPfx()
    {
        if (!startedBisonPfx)
        {
            fullGrowthPfx.Play();
            startedBisonPfx = true;
        }
        
        
        //Debug.Log(fullGrowthPfx.isPlaying);
    }
    
}
