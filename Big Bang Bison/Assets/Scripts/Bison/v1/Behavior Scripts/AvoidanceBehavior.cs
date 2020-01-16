/*
    AvoidanceBehavior.cs
    Caetano 
    12/27/19
    Caetano
    Class for avoidance behavior object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, the movement vector
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Bison Avoidance")]
public class AvoidanceBehavior : FilteredHerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior

        // if no filtered neighbors return no adjustment
        if (filterContext.Count == 0) return Vector3.zero;

        foreach (Transform item in filterContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < herd.SquareAvoidanceRadius) // if the distance to the item is within the avoidance radius
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position; // add vector pointing away from item
            }
        }

        if (nAvoid > 0) avoidanceMove /= nAvoid; // average, avoidanceMove is now the transform of the destination

        return avoidanceMove;
    }
}
