using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class OnlineGamePlayManager : NetworkBehaviour
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
    //Transform spawnPoint;

    int counter;
    //int connectedPlayers;
    Scene m_scene;
    private void Awake()
    {
        //spawnPoint = FindObjectOfType<ScoreBison>().gameObject.transform;
        //connectedPlayers = 0;
        /*m_scene = SceneManager.GetActiveScene();
        if (m_scene.name == "PreProduction_Level_Large")
        {
            for (int i = 0; i < 4; i++)
            {
                if (charecter[i] == 1)
                {
                    GameObject Player = Instantiate(playerPull, startingPosition[i], Quaternion.identity, transform);
                    Player.GetComponent<PlayerProperties>().Info(i + 1, team[i], charecter[i], controllerNum[i]);
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
        }*/



    }

    void Update()
    {
        /*if (m_scene.name != "PreProduction_Level_Large")
        {
            counter = 0;
            for (int i = 0; i < 4; i++)
            {

                if (playersReady[i] == true)
                {
                    counter++;
                }
            }
            if (counter == 4)
            {
                Debug.Log("hit 1");
                SceneManager.LoadScene(2);
            }
            else if (counter > 0 && Input.GetButtonDown("select"))
            {
                Debug.Log(counter);
                Debug.Log("hit 2");
                SceneManager.LoadScene(2);
            }
        }
        if (m_scene.name == "PreProduction_Level_Large")
        {

        }*/



    }
    public void Ready(int panel, bool ready, int teamNum, int controller, int charec)
    {
        playersReady[panel] = ready;
        team[panel] = teamNum;
        controllerNum[panel] = controller;
        charecter[panel] = charec;
        /*Debug.Log(playersReady[0]);
        Debug.Log(team[0]);
        Debug.Log(controllerNum[0]);
        Debug.Log(charecter[0]);*/
    }

    public override void OnStartServer() => NetworkManagerBBB.OnServerReadied += SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        //connectedPlayers++;
        //playerWall.GetComponent<OnlinePlayerProperties>().playerNum = connectedPlayers;
        GameObject playerInstance = Instantiate(playerWall, startingPosition[2], Quaternion.identity);
        NetworkServer.Spawn(playerInstance, conn);
    }






}

