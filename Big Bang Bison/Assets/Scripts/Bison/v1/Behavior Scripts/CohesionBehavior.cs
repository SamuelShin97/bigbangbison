/*
    CohesionBehavior.cs
    Caetano 
    12/23/19
    Caetano
    Abstract class for functions used in other bison related scripts
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, bison move towards their neighbors
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Cohesion")]
public class CohesionBehavior : HerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // if no neighbors return no adjustment
        if (context.Count == 0) return Vector3.zero;

        // add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in context)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count; // cohesionMove is now the transform of the destination

        // create offset from agent position, the actual move of the agent
        cohesionMove -= agent.transform.position;

        return cohesionMove;
    }
}
