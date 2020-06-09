/*
    OnlineHerdShepherd.cs
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
using Mirror;

public class OnlineHerdShepherd : NetworkBehaviour
{
    public OnlineHerdBox myHerdBox;
    public OnlineHerd myHerd;
    private int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        myHerdBox = GetComponentInChildren<OnlineHerdBox>();
        if (GetComponent<OnlinePlayerProperties>()) {
            playerNum = GetComponent<OnlinePlayerProperties>().playerNum;
            myHerd = GameObject.Find("Herd " + GetComponent<OnlinePlayerProperties>().teamColor).GetComponent<OnlineHerd>();
        }
        else if (GetComponent<ThirdPersonController>()) playerNum = GetComponent<ThirdPersonController>().PlayerNum;
    }

    // Checks if the box should be up or not
    
    void Update()
    {
        /*if (!hasAuthority)
        {
            return;
        }
        float pressure = Input.GetAxis("LTrigger" + playerNum);
        if (pressure > 0.1)
        {
            if (!myHerdBox.gameObject.activeSelf) // turn on the box if it's off
            {
                TurnOn();
            }
        }
        else if (myHerdBox.gameObject.activeSelf) // Turn off the box
        {
            TurnOff();
        }*/
        RpcUpdate();
    }
    [ClientRpc]
    void RpcUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }
        float pressure = Input.GetAxis("LTrigger" + playerNum);
        if (pressure > 0.1)
        {
            if (!myHerdBox.gameObject.activeSelf) // turn on the box if it's off
            {
                TurnOn();
            }
        }
        else if (myHerdBox.gameObject.activeSelf) // Turn off the box
        {
            TurnOff();
        }
    }

    private void TurnOff()
    {
        myHerdBox.TurnOff();
        myHerdBox.gameObject.SetActive(false);
    }

    private void TurnOn()
    {
        myHerdBox.gameObject.SetActive(true);
    }
}
