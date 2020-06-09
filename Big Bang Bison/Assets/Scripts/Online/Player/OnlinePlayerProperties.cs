using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnlinePlayerProperties : NetworkBehaviour
{
    public int playerNum; //which corner
    public int teamColor;
    public int character;
    public int controllerNum; //which controller
    public Material red;
    public Material blue;
    
    GamePlayManager gpm;
    // Start is called before the first frame update
    void Start()
    {
        //gpm = FindObjectOfType<GamePlayManager>();
        AssignTeamColor();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("player num is " + playerNum);
    }

    protected int GetPlayerNum()
    {
        return playerNum;
    }

    public void Info(int panel, int team, int charec, int cont)
    {
        teamColor = team;
        character = charec;
        controllerNum = cont;
        playerNum = panel;
        /*Debug.Log(teamColor+"in PP");
        Debug.Log(character + "in PP");
        Debug.Log(controllerNum + "in PP");
        Debug.Log(playerNum + "in PP");*/
    }

    void AssignTeamColor()
    {
        //Material material = GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        if (teamColor == 1) //if blue
        {
            GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
            ParticleSystem.MainModule m_ps = GetComponentInChildren<Animator>().gameObject.
                GetComponentInChildren<ParticleSystem>().main;
            m_ps.startColor = blue.color;
        }
        else if (teamColor == 2)
        {
            GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = red;
            ParticleSystem.MainModule m_ps = GetComponentInChildren<Animator>().gameObject.
                GetComponentInChildren<ParticleSystem>().main;
            m_ps.startColor = red.color;
        }
        
    }
}
