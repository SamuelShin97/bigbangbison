/*
    HerdAgent.cs
    Caetano 
    1/27/19
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

    // Return my collider
    Collider agentCollider;
    public Collider AgentCollider {
        get { return agentCollider; }
    }

    // Save my Rigidbody
    public Rigidbody agentBody;

    // 0 - Idle, 1 - running, 2 - panicking, 3 - Off ground
    public byte state = 0;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>(); // save my collider
        agentBody = GetComponent<Rigidbody>();
    }

    // Called once when created
    public void Initialize(Herd herd)
    {
        agentHerd = herd; // save my herd
    }

    void Update()
    {
        
    }

    // Move it
    public void Move(Vector3 velocity)
    {
        agentBody.AddForce(velocity * Time.deltaTime, ForceMode.Impulse);
        if (agentBody.velocity.sqrMagnitude > 100)
        {
            agentBody.AddForce(-agentBody.velocity.normalized * (agentBody.velocity.magnitude - 10), ForceMode.Impulse);
        }
        if (agentBody.velocity.sqrMagnitude > 1) transform.forward = agentBody.velocity; // face this direction
        //transform.position += velocity * Time.deltaTime; // move this direction
    }
}
