/*
    AvoidanceBehavior.cs
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

[CreateAssetMenu(menuName = "Herd/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredHerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filterContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < herd.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position;
            }
        }

        if (nAvoid > 0) avoidanceMove /= nAvoid; // cohesionMove is now the transform of the destination

        // create offset from agent position, the actual move of the agent
        //avoidanceMove -= agent.transform.position;

        return avoidanceMove;
    }
}
