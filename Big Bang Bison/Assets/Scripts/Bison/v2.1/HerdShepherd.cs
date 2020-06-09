/*
    HerdShepherd.cs
    Caetano 
    3/23/20
    Caetano
    Class for players use to herd
    Functions in file:
        .Disable: Out, sets all bison that were in the box to not be
        .Enable: Out, sets all bison in the box to be in the box
        .Move: In, velocity - Out, moves this
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdShepherd : MonoBehaviour
{
    public HerdBox myHerdBox;
    public Herd myHerd;
    private int playerNum;
    public float pressure;

    private ParticleSystem[] boxParticles;

    private void Awake()
    {
        boxParticles = GetComponentsInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myHerdBox = GetComponentInChildren<HerdBox>();
        if (GetComponent<PlayerProperties>()) {
            playerNum = GetComponent<PlayerProperties>().playerNum;
            myHerd = GameObject.Find("Herd " + GetComponent<PlayerProperties>().teamColor).GetComponent<Herd>();
        }
        else if (GetComponent<ThirdPersonController>()) playerNum = GetComponent<ThirdPersonController>().PlayerNum;
    }

    // Checks if the box should be up or not
    void Update()
    {
        pressure = Input.GetAxis("LTrigger" + playerNum);
        if (pressure > 0.01)
        {
            if (!myHerdBox.gameObject.activeSelf) // turn on the box if it's off
            {
                TurnOn();
            }

            foreach (ParticleSystem p in boxParticles)
            {

                if (p.gameObject.name.Contains("Trail")) continue; // Don't change the speed of the sprial

                var main = p.main;
                main.simulationSpeed = pressure * 2.0f;
            }

        } else if (myHerdBox.gameObject.activeSelf) // Turn off the box
        {
            TurnOff();
        }
    }
    
    private void TurnOff()
    {
        foreach (ParticleSystem p in boxParticles)
        {
            if (p.gameObject.name.Contains("Trail")) // Inverted behavior for the spiral under the player
            {
                p.Play();
                continue;
            }

            var main = p.main;
            main.simulationSpeed = 0;
            p.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        myHerdBox.TurnOff();
        myHerdBox.gameObject.SetActive(false);
    }

    private void TurnOn()
    {
        foreach (ParticleSystem p in boxParticles)
        {
            if (p.gameObject.name.Contains("Trail")) // Inverted behavior for the spiral under the player
            {
                p.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                continue;
            }
            p.Play();
        }
        myHerdBox.gameObject.SetActive(true);
    }
}
