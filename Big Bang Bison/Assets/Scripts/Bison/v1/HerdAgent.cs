/*
    HerdAgent.cs
    Caetano 
    12/24/19
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

    // Any effects specific to this bison. Things like fully grown, slowed, sped up, panicking, etc... 
    public List<string> Effects;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>(); // save my collider
    }

    // Called once when created
    public void Initialize(Herd herd)
    {
        agentHerd = herd; // save my herd
    }

    // Move it
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity; // face this direction
        transform.position += velocity * Time.deltaTime; // move this direction
    }
}
