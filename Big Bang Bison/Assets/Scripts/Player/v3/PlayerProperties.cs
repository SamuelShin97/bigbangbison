using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    public int playerNum; //which corner
    public int teamColor;
    public int character;
    public int controllerNum; //which controller
    public Material red;
    public Material blue;
    public Image cooldown;
    public Image cooldownBG;
    [SerializeField]
    public GameObject[] allCanvas = new GameObject[4];
    //[SerializeField] private GameObject staff;
    
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
            //changes player model color
            GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = blue;

            //changes herdbox color
            ParticleSystem[] herdBoxPfx = GetComponentsInChildren<ParticleSystem>();

            for (int i = 0; i < herdBoxPfx.Length; i++)
            {
                
                ParticleSystem.MainModule main_ps = herdBoxPfx[i].main;
                main_ps.startColor = blue.color;
            }

            //changes staff color
            //staff.GetComponent<MeshRenderer>().material = blue;
        }
        else if (teamColor == 2)
        {
            GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = red;


            ParticleSystem[] herdBoxPfx = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < herdBoxPfx.Length; i++)
            {
                ParticleSystem.MainModule main_ps = herdBoxPfx[i].main;
                main_ps.startColor = red.color;
            }

            //staff.GetComponent<MeshRenderer>().material = red;
        }
        
    }
}
