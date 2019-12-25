/*
    AlignmentBehavior.cs
    Caetano 
    12/24/19
    Caetano
    Abstract class for functions used in other bison related scripts
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, poop
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Alignment")]
public class AlignmentBehavior : FilteredHerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors maintain current adjustment
        if (context.Count == 0) return agent.transform.forward;

        // add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filterContext)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count; // cohesionMove is now the transform of the destination

        return alignmentMove;
    }
}