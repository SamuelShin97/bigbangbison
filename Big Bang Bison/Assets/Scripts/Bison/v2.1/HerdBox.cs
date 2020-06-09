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
public class HerdBox : MonoBehaviour
{
    public List<HerdAgent> BisonBeingHerded = new List<HerdAgent>();
    private Herd boxHerd;
    private HerdShepherd player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<HerdShepherd>();
        boxHerd = player.myHerd;
    }

    // Keeps track of all bison that are in it
    void Update()
    {
        foreach (HerdAgent agent in BisonBeingHerded)
        {
            agent.boxPressure += player.pressure;
        }
    }

    public void TurnOff()
    {
        // Set all the bison to not be in the box anymore
        foreach (HerdAgent agent in BisonBeingHerded)
        {
            agent.inHerdBox --;
        }
        BisonBeingHerded = new List<HerdAgent>();
        gameObject.SetActive(false);
    }

    // keeps track of all bison that enter
    private void OnTriggerEnter(Collider other)
    {
        HerdAgent newAgent = other.gameObject.GetComponent<HerdAgent>();
        if (newAgent && newAgent.AgentHerd == boxHerd)
        {
            newAgent.inHerdBox ++;
            newAgent.hasMoved = true;
            BisonBeingHerded.Add(newAgent);
        }
    }

    // keeps track of all bison that exit
    private void OnTriggerExit(Collider other)
    {
        HerdAgent exitAgent = other.gameObject.GetComponent<HerdAgent>();
        if (exitAgent && exitAgent.AgentHerd == boxHerd)
        {
            exitAgent.inHerdBox --;
            BisonBeingHerded.Remove(exitAgent);
        }
    }
}
