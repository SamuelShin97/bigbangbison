/*
    SteeredCohesionBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Class for Steered Cohesion behavior object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, movement towards the center of neighbors
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredHerdBehavior
{
    Vector3 currentVelocity; // does something probably
    
    [Range(0f, 5f)]
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior
        foreach (Transform item in filterContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count; // average

        // create offset from agent position, the actual move of the agent
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime); // this is what makes this "smoothed"

        return cohesionMove;
    }
}