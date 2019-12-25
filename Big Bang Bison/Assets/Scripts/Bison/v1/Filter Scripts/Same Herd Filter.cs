using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Filter/Same Herd")]
public class SameHerdFilter : ContextFilter
{
    public override List<Transform> Filter(HerdAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
        {
            HerdAgent itemAgent = item.GetComponent<HerdAgent>();
            if (itemAgent != null && itemAgent.AgentHerd == agent.AgentHerd)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
