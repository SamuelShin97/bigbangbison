/*
    HerdBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Abstract class for behaviors, only has one function
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, a movement vector of the agent
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Each herd has a behavior attatched to it
 * All behaviors are used for the CalculateMove function
 * Some behaviors are composite, these behaviors run a set of other behaviors in its CalculateMove
 */
public abstract class HerdBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd);

    public abstract Vector3 CalculateMove(OnlineHerdAgent agent, List<Transform> context, OnlineHerd herd);
}
