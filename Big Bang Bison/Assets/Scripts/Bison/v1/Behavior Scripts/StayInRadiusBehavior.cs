/*
    StayInRadiusBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Class for StayInRadius object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, bison moves to stay in a circle
    
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : HerdBehavior
{
    public Vector3 center; // the center of the circle
    public float radius = 250f; // the radius of the circle

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9) return Vector3.zero; // if within 90% of the circle, move towards the center

        return centerOffset * t * t; // stronger the further from the center
    }
}
