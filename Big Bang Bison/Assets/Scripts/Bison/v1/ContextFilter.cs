/*
    ContextFilter.cs
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

/* Each behavior gets a context
 * Some behaviors are filtered
 */
public abstract class ContextFilter : ScriptableObject
{
    public abstract List<Transform> Filter(HerdAgent agent, List<Transform> original);

    public abstract List<Transform> Filter(OnlineHerdAgent agent, List<Transform> original);
}
