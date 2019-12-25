/*
    StayInRadiusBehavior.cs
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

[CreateAssetMenu(menuName = "Herd/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : HerdBehavior
{
    public Vector3 center;
    public float radius = 250f;

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9) return Vector3.zero;

        return centerOffset * t * t;
    }
}
