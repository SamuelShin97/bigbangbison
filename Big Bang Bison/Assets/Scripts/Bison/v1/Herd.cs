/*
    Herd.cs
    Caetano 
    12/22/19
    Caetano
    Class for bison behavior
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, poop
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour
{
    public HerdAgent agentPrefab;
    List<HerdAgent> agents = new List<HerdAgent>();
    public HerdBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 200;
    const float DENSITY = 0.2f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighBorRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {
        get { return squareAvoidanceRadius; }
    }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighBorRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighBorRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Vector3 spawn = ((Vector3)Random.insideUnitCircle * startingCount * DENSITY);
            spawn.z = spawn.y;
            spawn.y = 0;
            HerdAgent newAgent = Instantiate(
                agentPrefab,
                spawn,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Bison " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (HerdAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 8f);
            //Vector3 move = behavior.CalculateMove(agent, context, this);
            //move *= driveFactor;
            //if (move.sqrMagnitude > squareMaxSpeed) move = move.normalized * maxSpeed;
            //agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(HerdAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
