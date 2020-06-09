/*
    AlignmentBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Class for alignment behavior object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, the vector to face towards
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Alignment")]
public class AlignmentBehavior : FilteredHerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors, maintain current alignment
        if (context.Count == 0) return agent.transform.forward;

        // add all neighbor's alignment together and average
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior
        foreach (Transform item in filterContext)
        {
            alignmentMove += item.transform.forward; // add the facing direction
        }
        alignmentMove /= context.Count; // average, alignmentMove is now the destination alignment

        return alignmentMove;
    }

    public override Vector3 CalculateMove(OnlineHerdAgent agent, List<Transform> context, OnlineHerd herd)
    {
        // if no neighbors, maintain current alignment
        if (context.Count == 0) return agent.transform.forward;

        // add all neighbor's alignment together and average
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior
        foreach (Transform item in filterContext)
        {
            alignmentMove += item.transform.forward; // add the facing direction
        }
        alignmentMove /= context.Count; // average, alignmentMove is now the destination alignment

        return alignmentMove;
    }
}