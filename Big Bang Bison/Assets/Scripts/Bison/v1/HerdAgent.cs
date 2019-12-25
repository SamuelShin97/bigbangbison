/*
    HerdAgent.cs
    Caetano 
    12/23/19
    Caetano
    Class for individual bison
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, poop
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HerdAgent : MonoBehaviour
{
    Herd agentHerd;
    public Herd AgentHerd
    {
        get { return agentHerd; }
    }

    Collider agentCollider;
    public Collider AgentCollider {
        get { return agentCollider; }
    }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Herd herd)
    {
        agentHerd = herd;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
