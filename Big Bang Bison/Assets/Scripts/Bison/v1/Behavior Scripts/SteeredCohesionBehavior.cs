/*
    SteeredCohesionBehavior.cs
    Caetano 
    12/24/19
    Caetano
    Abstract class for functions used in other bison related scripts
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, bison move towards their neighbors
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredHerdBehavior
{

    Vector3 currentVelocity;
    
    [Range(0f, 5f)]
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filterContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count; // cohesionMove is now the transform of the destination

        // create offset from agent position, the actual move of the agent
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}