using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Manager : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    private float timer;
    private int numReadyPlayers = 0, maxNumberPlayers = 0;
    private bool timeStamped;
    private bool gameReady = false;
    private bool[] readyPlayers = new bool[4];
    private bool[] countedPlayers = new bool[4];
    public GameObject readyText;
    private float[] timers = new float[2];

    void Awake()
    {
        for (int i = 0; i < 4; i++) {
            readyPlayers[i] = false;
            countedPlayers[i] = false;
        }
        timers[0] = Time.time;
        timers[1] = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        // Check how many players are ready
        check_max_num_players(0);
        check_max_num_players(1);
        check_max_num_players(2);
        check_max_num_players(3);
        check_player_ready(0);
        check_player_ready(1);
        check_player_ready(2);
        check_player_ready(3);

        if (maxNumberPlayers == numReadyPlayers && numReadyPlayers == 4 && gameReady == false && Time.time > timers[1] + 0.1f) //maxNumberPlayers > 0
        {
            timers[0] = Time.time;
            Debug.Log("PLAYERS READY");
            readyText.GetComponent<anim_effects>().zoom_in();
            gameReady = true;
        }
        if (Time.time > timers[0] + 0.1f)
        {
            if (gameReady == true && Input.GetButtonDown("B1") || Input.GetButtonDown("B2") || Input.GetButtonDown("B3") || Input.GetButtonDown("B4"))
            {
                timers[1] = Time.time;
                readyText.GetComponent<anim_effects>().disappear();
                Debug.Log("Max Num: " + maxNumberPlayers);
                Debug.Log("Current Num: " + numReadyPlayers);
                gameReady = false;
            }
        }

        // Go back to Title
        if (players_active() && players_back() && Time.time > timer +0.1f) { SceneManager.LoadScene(0); }
    }

    bool players_active()
    {
        // If no one has joined, allow players to go back to Title screen after a delay
        if (players[0].GetComponent<Panel_Control>().panelState == 0
            && players[1].GetComponent<Panel_Control>().panelState == 0
            && players[2].GetComponent<Panel_Control>().panelState == 0
            && players[3].GetComponent<Panel_Control>().panelState == 0)
        {
            start_timer();
            return true;
        }
        else { timeStamped = false; } // Reset timer
        return false;
    }

    bool players_back()
    {
        return (Input.GetButtonDown("B1")
            || Input.GetButtonDown("B2")
            || Input.GetButtonDown("B3")
            || Input.GetButtonDown("B4"));
    }

    void start_timer() {
        if (timeStamped == false)
        {
            timeStamped = true;
            timer = Time.time;
        }
    }

    void check_player_ready(int num)
    {
        if (players[num].GetComponent<Panel_Control>().panelState == 2 && readyPlayers[num] == false)
        {
            numReadyPlayers++;
            readyPlayers[num] = true;
        }
        else if(players[num].GetComponent<Panel_Control>().panelState == 1 && readyPlayers[num] == true)
        {
            numReadyPlayers--;
            readyPlayers[num] = false;
        }
    }
    void check_max_num_players (int num)
    {
        if (players[num].GetComponent<Panel_Control>().panelState == 1 && countedPlayers[num] == false)
        {
            maxNumberPlayers++;
            countedPlayers[num] = true;
        }
        else if (players[num].GetComponent<Panel_Control>().panelState == 0 && countedPlayers[num] == true)
        {
            maxNumberPlayers--;
            countedPlayers[num] = false;
        }
    }
}
