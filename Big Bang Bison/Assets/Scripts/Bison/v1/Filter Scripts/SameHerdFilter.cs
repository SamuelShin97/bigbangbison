/*
    SameHerdFilter.cs
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

// A filter to only look at members of the same herd
[CreateAssetMenu(menuName = "Herd/Filter/Same Herd")]
public class SameHerdFilter : ContextFilter
{
    // Looks at  all objects in the original context, then returns a new list with only the filtered objects
    public override List<Transform> Filter(HerdAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>(); // list to return

        foreach (Transform item in original)
        {
            HerdAgent itemAgent = item.GetComponent<HerdAgent>();
            if (itemAgent != null && itemAgent.AgentHerd == agent.AgentHerd) // if this item is in the same herd
            {
                filtered.Add(item); // add it to the list
            }
        }
        return filtered;
    }

    // Looks at  all objects in the original context, then returns a new list with only the filtered objects
    public override List<Transform> Filter(OnlineHerdAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>(); // list to return

        foreach (Transform item in original)
        {
            HerdAgent itemAgent = item.GetComponent<HerdAgent>();
            if (itemAgent != null && itemAgent.AgentHerd == agent.AgentHerd) // if this item is in the same herd
            {
                filtered.Add(item); // add it to the list
            }
        }
        return filtered;
    }
}
