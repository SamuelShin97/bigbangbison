/*
    HerdBehavior.cs
    Caetano 
    12/22/19
    Caetano
    Abstract class for functions used in other bison related scripts
    Functions in file:
        CalculateMove: In, agent, context, herd - Out, poop
    Any Global variables referenced in the file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HerdBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove (HerdAgent agent, List<Transform> context, Herd herd);
    
}
