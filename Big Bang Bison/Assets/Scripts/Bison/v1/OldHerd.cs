/*
    Herd.cs
    Caetano 
    12/27/19
    Caetano
    Class for bison behavior
    Functions in file:
        GetNearbyObjects: In, agent - Out, list of nearby objects not ignored by bison
        .SquareAvoidanceRadius: Out, the square of the avoidance radius
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Herd is an object that will contain and manage each HerdAgent (Bison).   Herd = Flock
public class OldHerd : MonoBehaviour
{
    // These two are set in the editor
    public HerdAgent agentPrefab; // The prefab used when instantiating new agents
    public HerdBehavior behavior; // The behavior run on each agent

    // A list of all the agents in this herd
    List<HerdAgent> agents = new List<HerdAgent>(); // it starts empty

    // The number of agents to spawn in this herd
    [Range(1, 500)]
    public int startingCount = 200; // defaults to 200

    // A constant, this could be changed. Smaller number = tighter group spawned
    const float DENSITY = 0.2f;

    // These variables affect the behaviors in the herd
    [Range(0f, 20f)]
    public float driveFactor = 4f; // increases the intensity of behaviors
    [Range(1f, 100f)]
    public float maxSpeed = 5f; // the max speed
    [Range(1f, 20f)]
    public float neighborRadius = 1.5f; // how close something has to be to be detected as a neighbor
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f; // the radius for agents to avoid things, relative to neighborRadius

    // Math is faster when done with squares, save them here
    float squareMaxSpeed;
    float squareNeighBorRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {
        get { return squareAvoidanceRadius; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Math is faster when done with squares, calculate them here
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighBorRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighBorRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // Make all the Bison
        for (int i = 0; i < startingCount; i++)
        {
            Vector3 spawn = (Vector3)Random.insideUnitCircle * startingCount * DENSITY; // random point in a circle with an x and y, and a radius of startingCount * DENSITY
            spawn.z = spawn.y; // we want z
            spawn.y = 0; // not y

            // Yo, it's a new bison
            HerdAgent newAgent = Instantiate(
                agentPrefab,
                spawn,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)), // random start rotation
                transform // herd position
                );
            newAgent.name = "Bison " + i;
            newAgent.Initialize(this); // agent has startup function
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate through all agents and  move them
        foreach (HerdAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent); // get the context

            Vector3 move = behavior.CalculateMove(agent, context, this); // figure out how much the agent should move
            move *= driveFactor; // make it bigger
            if (move.sqrMagnitude > squareMaxSpeed) move = move.normalized * maxSpeed; // cap it at max
            agent.Move(move); // tell it to move itself

            // colors crowded bison
            // agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 8f);
        }
    }

    // Gets nearby objects
    List<Transform> GetNearbyObjects(HerdAgent agent)
    {
        int layerMask = ~(1 << 10); // used to ignore objects in layer 10

        List<Transform> context = new List<Transform>(); // the list to return

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius, layerMask); // gets all colliders near the agent
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider) // if the collider sin't its own
            {
                context.Add(c.transform); // add it to the context
            }
        }
        return context;
    }
}
