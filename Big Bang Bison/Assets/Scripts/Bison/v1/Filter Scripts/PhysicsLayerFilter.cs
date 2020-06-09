/*
    PhysicsLayerFilter.cs
    Caetano 
    12/26/19
    Caetano
    Abstract class for filters
    Functions in file:
        Filter: In, agent, original - Out, a filtered version of the original context

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A filter to only look at a specified layer
[CreateAssetMenu(menuName = "Herd/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    // Looks at  all objects in the original context, then returns a new list with only the filtered objects
    public override List<Transform> Filter(HerdAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>(); // the list to return

        foreach (Transform item in original) // for each transform
        {
            if (mask == (mask | (1 << item.gameObject.layer))) // if the gameObject is on the right layer
            {
                filtered.Add(item); // add it to the filtered list
            }
        }

        return filtered;
    }

    public override List<Transform> Filter(OnlineHerdAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>(); // the list to return

        foreach (Transform item in original) // for each transform
        {
            if (mask == (mask | (1 << item.gameObject.layer))) // if the gameObject is on the right layer
            {
                filtered.Add(item); // add it to the filtered list
            }
        }

        return filtered;
    }
}
