/*
    PlayerAvoidanceBehavior.cs
    Caetano 
    2/27/20
    Caetano
    Class for avoidance behavior object, when avoiding players
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
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < herd.SquareAvoidanceRadius) // if the distance to the item is within the avoidance radius
            {
                Vector3 dist = agent.transform.position - item.position;
                nAvoid++;
                // float forceMult = (dist.sqrMagnitude / herd.neighborRadius * herd.neighborRadius) * 100;
                // float forceMult = herd.neighborRadius - Vector3.Distance(agent.transform.position, item.position);
                //Debug.Log("player is " + Vector3.Distance(agent.transform.position, item.position) + " away.");

                avoidanceMove += dist; // add vector pointing away from item
            }
        }

        if (nAvoid > 0) avoidanceMove /= nAvoid; // average, avoidanceMove is now the transform of the destination

        return avoidanceMove;
    }

    public override Vector3 CalculateMove(OnlineHerdAgent agent, List<Transform> context, OnlineHerd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context); // this is a filtered behavior
        foreach (Transform item in filterContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < herd.SquareAvoidanceRadius) // if the distance to the item is within the avoidance radius
            {
                Vector3 dist = agent.transform.position - item.position;
                nAvoid++;
                // float forceMult = (dist.sqrMagnitude / herd.neighborRadius * herd.neighborRadius) * 100;
                // float forceMult = herd.neighborRadius - Vector3.Distance(agent.transform.position, item.position);
                //Debug.Log("player is " + Vector3.Distance(agent.transform.position, item.position) + " away.");

                avoidanceMove += dist; // add vector pointing away from item
            }
        }

        if (nAvoid > 0) avoidanceMove /= nAvoid; // average, avoidanceMove is now the transform of the destination

        return avoidanceMove;
    }
}
