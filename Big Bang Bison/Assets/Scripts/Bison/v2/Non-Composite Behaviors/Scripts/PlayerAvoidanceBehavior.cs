﻿/*
    PlayerAvoidanceBehavior.cs
    Caetano 
    1/28/20
    Caetano
    Class for avoidance behavior object, when avoiding things other than bison
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, the movement vector
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Player Avoidance")]
public class PlayerAvoidanceBehavior : FilteredHerdBehavior
{

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior
        foreach (Transform item in filterContext)
        {
            Vector3 dist = item.position - agent.transform.position;
            nAvoid++;
            avoidanceMove += (agent.transform.position - item.position).normalized * (herd.neighborRadius * herd.neighborRadius / dist.sqrMagnitude) * 70; // add vector pointing away from item
        }

        if (nAvoid > 0) avoidanceMove /= nAvoid; // average, avoidanceMove is now the transform of the destination

        return avoidanceMove;
    }
}
