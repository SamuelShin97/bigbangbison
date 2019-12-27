/*
    FilteredHerdBehavior.cs
    Caetano 
    12/26/19
    Caetano
    Abstract class for filtered behaviors
    Functions in file:
    
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* These behaviors all have a filter on what they see from the context
 */
public abstract class FilteredHerdBehavior : HerdBehavior
{
    public ContextFilter filter;
}
