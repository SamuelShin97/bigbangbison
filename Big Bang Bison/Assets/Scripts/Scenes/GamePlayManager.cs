using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    static public bool[] playersReady = new bool[4];
    static public int[] team = new int[4]; //values are 1 or 2| 1 = blue, 2 = red 
    static public int[] controllerNum = new int[4]; //values are 1,2,3 or 4 
    static public int[] charecter = new int[4]; //values are 1,2,3 or 4| 1 = blackhole, 2 = push, 3 = wall, 4 = teleport

    public GameObject playerPull;
    public GameObject playerPush;
    public GameObject playerWall;
    public GameObject playerTeleport;

    private Vector3[] startingPosition = { new Vector3(-2, 2, 2), new Vector3(2, 2, 2),
    new Vector3(-2, 2, -2), new Vector3(2, 2, -2)};
    private PlayerProperties properties;

    private int team1;
    private int team2;
    private int one, two, three, four;
    int counter;
    private bool ReadyAll;
    Scene m_scene;
    private void Awake()
    {
        m_scene = SceneManager.GetActiveScene();

        if (m_scene.buildIndex == 4)
        {
            startingPosition = new Vector3[] {new Vector3(-2, 0, 2), new Vector3(2, 0, 2), new Vector3(-2, 0, -2), new Vector3(2, 0, -2)};
        }
        
        if (m_scene.buildIndex == 2 || m_scene.buildIndex == 4) //if game scene
        {
            for (int i = 0; i < 4; i++)
            {
                if (charecter[i] == 1)
                {
                    GameObject Player = Instantiate(playerPull, startingPosition[i], Quaternion.identity, transform);
                    Player.GetComponent<PlayerProperties>().Info(i+1, team[i], charecter[i], controllerNum[i]);
                }
                else if (charecter[i] == 2)
                {
                    GameObject Player = Instantiate(playerPush, startingPosition[i], Quaternion.identity, transform);
                    Player.GetComponent<PlayerProperties>().Info(i + 1, team[i], charecter[i], controllerNum[i]);
                }
                else if (charecter[i] == 3)
                {
                    GameObject Player = Instantiate(playerWall, startingPosition[i], Quaternion.identity, transform);
                    Player.GetComponent<PlayerProperties>().Info(i + 1, team[i], charecter[i], controllerNum[i]);
                }
                else if (charecter[i] == 4)
                {
                    GameObject Player = Instantiate(playerTeleport, startingPosition[i], Quaternion.identity, transform);
                    Player.GetComponent<PlayerProperties>().Info(i + 1, team[i], charecter[i], controllerNum[i]);
                }
            }

            properties = GameObject.FindObjectOfType<PlayerProperties>();
        }
        

        
    }

    void Update()
    {
        if (m_scene.buildIndex == 1) //if not the game scene
        {
            counter = 0;
            team1 = 0;
            team2 = 0;
            for (int i = 0; i < 4; i++)
            {
                
                if (playersReady[i] == true)
                {
                    counter++;
                }
                if (team[i] == 1)
                {
                    team1++;
                }
                else if (team[i] == 2)
                {
                    team2++;
                    
                }
                ReadyAll = checkAllDif();
            }
            Debug.Log(" counter = " + counter + ", team1 = " + team1 + ", team2 = " + team2);
            if (counter == 4 && team1 == 2 && team2 ==2 && Input.GetButtonDown("Go") && ReadyAll == true)
            {
                SceneManager.LoadScene(2);
            }
            else if (counter > 0 && Input.GetButtonDown("select"))
            {
                SceneManager.LoadScene(2);
            }
            else if (counter > 0 && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.L))
            {
                SceneManager.LoadScene(4);
            }
        }
        if (m_scene.name == "PreProduction_Level_Large")
        {

        }

            

    }
    public void Ready(int panel, bool ready, int teamNum, int controller, int charec)
    {
        playersReady[panel] = ready;
        team[panel] = teamNum;
        controllerNum[panel] = controller;
        charecter[panel] = charec;
    }
    public bool checkAllDif()
    {
        one = 0;
        two = 0;
        three = 0;
        four = 0;
        for (int j = 0; j < 4; j++)
        {
            if (charecter[j] == 1)
            {
                one++;
            }
            else if (charecter[j] == 2)
            {
                two++;
            }
            else if (charecter[j] == 3)
            {
                three++;
            }
            else if(charecter[j] == 4)
            {
                four++;
            }
        }
        if (one == 1 && two == 1 && three == 1 && four == 1)
        {
            Debug.Log("All 4 diff");
            return true;
        }
        else
        {
            return false;
        }
            
    }







}
