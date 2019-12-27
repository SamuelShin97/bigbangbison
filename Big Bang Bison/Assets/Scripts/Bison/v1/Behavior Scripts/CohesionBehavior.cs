/*
    CohesionBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Class for Cohesion behavior object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, movement towards the center of neighbors
    
    Note: you probably want steered cohesion
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Cohesion")]
public class CohesionBehavior : FilteredHerdBehavior
{
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
        cohesionMove /= context.Count; // average, cohesionMove is now the transform of the destination

        // create offset from agent position, the actual move of the agent
        cohesionMove -= agent.transform.position;

        return cohesionMove;
    }
}
