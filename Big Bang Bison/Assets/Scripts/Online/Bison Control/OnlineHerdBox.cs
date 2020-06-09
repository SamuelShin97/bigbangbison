/*
    HerdBox.cs
    Caetano 
    3/14/20
    Caetano
    Class for box players use to herd
    Functions in file:
        .Disable: Out, sets all bison that were in the box to not be
        .Enable: Out, sets all bison in the box to be in the box
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OnlineHerdBox : MonoBehaviour
{
    public List<OnlineHerdAgent> BisonBeingHerded = new List<OnlineHerdAgent>();
    private OnlineHerd boxHerd;
    private OnlineHerdShepherd player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<OnlineHerdShepherd>();
        boxHerd = player.myHerd;
    }

    // Keeps track of all bison that are in it
    void Update()
    {

    }

    public void TurnOff()
    {
        // Set all the bison to not be in the box anymore
        foreach (OnlineHerdAgent agent in BisonBeingHerded)
        {
            agent.inHerdBox = false;
        }
        BisonBeingHerded = new List<OnlineHerdAgent>();
        gameObject.SetActive(false);
    }

    // keeps track of all bison that enter
    private void OnTriggerEnter(Collider other)
    {
        OnlineHerdAgent newAgent = other.gameObject.GetComponent<OnlineHerdAgent>();
        if (newAgent.AgentHerd == boxHerd)
        {
            newAgent.inHerdBox = true;
            BisonBeingHerded.Add(newAgent);
        }
    }

    // keeps track of all bison that exit
    private void OnTriggerExit(Collider other)
    {
        OnlineHerdAgent exitAgent = other.gameObject.GetComponent<OnlineHerdAgent>();
        if (exitAgent)
        {
            exitAgent.inHerdBox = false;
            BisonBeingHerded.Remove(exitAgent);
        }
    }
}
