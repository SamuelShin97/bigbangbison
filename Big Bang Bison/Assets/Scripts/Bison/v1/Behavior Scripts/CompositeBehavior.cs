/*
    CompositeBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Class for CompositeBehavior object
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, bison move according to multiple behaviors
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behavior is the one attatched to herds
[CreateAssetMenu(menuName = "Herd/Behavior/Composite")]
public class CompositeBehavior : HerdBehavior
{
    public HerdBehavior[] behaviors; // the behaviors for this herd
    public float[] weights; // the weights of those behaviors

    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // handle data mismatch
        if (behaviors.Length != weights.Length)
        {
            Debug.LogError("Data missmatch in " + name, this);
            return Vector3.zero;
        }

        // set up move
        Vector3 move = Vector3.zero;

        // iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, herd) * weights[i]; // do a behavior

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i]) // the weight caps the magnitude of each behavior
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                
                move += partialMove;
            }
        }

        move.y = 0; // bison don't jump on their own
        return move;
    }
}
