using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectPlayer1 : MonoBehaviour
{
    // Canvases
    public Canvas teamSelect;
    public Canvas characterSelect;

    // All models
    public int numModels;
    public GameObject[] P1Models;
    public GameObject[] P2Models;
    public GameObject[] P3Models;
    public GameObject[] P4Models;
    
    // Currently enabled models
    private Renderer P1model;
    private Renderer P2model;
    private Renderer P3model;
    private Renderer P4model;
    private int P1Current;
    private int P2Current;
    private int P3Current;
    private int P4Current;

    // These values exist globally in case players return from game screen???
    private bool teamsDone;
    static private bool P1Done;
    static private bool P2Done;
    static private bool P3Done;
    static private bool P4Done;
    private int newSelect;
    private KeyCode input;

    // Images and Text Game Objcets
    public Text P1TeamSelect;
    public Text P2TeamSelect;
    public Text P3TeamSelect;
    public Text P4TeamSelect;
    public GameObject P1cursor;
    public GameObject P2cursor;
    public GameObject P3cursor;
    public GameObject P4cursor;

    // Gameobject coordinate values
    private float[] TeamSelectX;
    private float[] P1cursorX;
    private float[] P1cursorY;
    private float[] P2cursorX;
    private float[] P2cursorY;
    private float[] P3cursorX;
    private float[] P3cursorY;
    private float[] P4cursorX;
    private float[] P4cursorY;
    private int currentModel;
    private int newModel;
    public Text selectText;

    // Start is called before the first frame update
    void Start()
    {
        currentModel = 0;
        // Disable all model Renderers
        for(int i = 0; i < numModels; i++)
        {
            P1model = P1Models[i].GetComponent<Renderer>();
            P1model.enabled = false;
            P2model = P2Models[i].GetComponent<Renderer>();
            P2model.enabled = false;
            P3model = P3Models[i].GetComponent<Renderer>();
            P3model.enabled = false;
            P4model = P4Models[i].GetComponent<Renderer>();
            P4model.enabled = false;
        }
        // Conditional variable setup
        teamsDone = false;
        P1Done = false;
        P2Done = false;
        P3Done = false;
        P4Done = false;

        // Set up all cursor coordinates
        TeamSelectX = new float[3];
        TeamSelectX[0] = -2.0f;
        TeamSelectX[0] = 0f;
        TeamSelectX[0] = 2.0f;
        // Cursor coordinates
        P1cursorX = new float[4];
        P1cursorX[0] = -2.25f;
        P1cursorX[1] = 0.25f;
        P1cursorX[2] = -2.25f;
        P1cursorX[3] = 0.25f;
        P1cursorY = new float[4];
        P1cursorY[0] = 0.96f;
        P1cursorY[1] = 0.96f;
        P1cursorY[2] = 0.22f;
        P1cursorY[3] = 0.22f;
        P2cursorX = new float[4];
        P2cursorX[0] = 0.25f;
        P2cursorX[1] = 2.25f;
        P2cursorX[2] = 0.25f;
        P2cursorX[3] = 2.25f;
        P2cursorY = new float[4];
        P2cursorY[0] = 0.96f;
        P2cursorY[1] = 0.96f;
        P2cursorY[2] = 0.22f;
        P2cursorY[3] = 0.22f;
        P3cursorX = new float[4];
        P3cursorX[0] = -2.25f;
        P3cursorX[1] = 0.25f;
        P3cursorX[2] = -2.25f;
        P3cursorX[3] = 0.25f;
        P3cursorY = new float[4];
        P3cursorY[0] = -1.96f;
        P3cursorY[1] = -1.96f;
        P3cursorY[2] = -1.22f;
        P3cursorY[3] = -1.22f;
        P4cursorX = new float[4];
        P4cursorX[0] = 0.25f;
        P4cursorX[1] = 2.25f;
        P4cursorX[2] = 0.25f;
        P4cursorX[3] = 2.25f;
        P4cursorY = new float[4];
        P4cursorY[0] = -1.96f;
        P4cursorY[1] = -1.96f;
        P4cursorY[2] = -1.22f;
        P4cursorY[3] = -1.22f;
        
        /*
        // Enable all Player 1st model Renderer
        P1model = P1Models[0].GetComponent<Renderer>();
        P1model.enabled = true;
        P2model = P2Models[0].GetComponent<Renderer>();
        P2model.enabled = true;
        P3model = P3Models[0].GetComponent<Renderer>();
        P3model.enabled = true;
        P4model = P4Models[0].GetComponent<Renderer>();
        P4model.enabled = true;
        */

        // Cursor setup
        P1cursor.transform.position = new Vector3(P1cursorX[0], P1cursorY[0], 0);
        P1Current = 0;
        P2Current = 0;
        P3Current = 0;
        P4Current = 0;

        // Enable team select canvas first, disable character select
        teamSelect.enabled = true;
        characterSelect.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (teamsDone == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print("team select move 1 left");
                move_left_team_select(P1TeamSelect);
            }
        }
        // Once all players have chosen a team
        else if(teamsDone == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                all_player_model_select(KeyCode.D);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                all_player_model_select(KeyCode.A);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                all_player_model_select(KeyCode.X);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                all_player_model_select(KeyCode.Z);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                all_player_model_select(KeyCode.W);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                all_player_model_select(KeyCode.Q);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                all_player_model_select(KeyCode.R);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                all_player_model_select(KeyCode.E);
            }
            if (Input.GetKeyDown("1"))
            {
                character_select(0);
            }
            else if (Input.GetKeyDown("2"))
            {
                character_select(1);
            }
            else if (Input.GetKeyDown("3"))
            {
                character_select(2);
            }
            else if (Input.GetKeyDown("4"))
            {
                character_select(3);
            }
            if (P1Done == true && P2Done == true && P3Done == true && P4Done == true)
            {
                selectText.text = "Press Return to Start";
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("PreProduction_Level_Large");
                }
            }
            else
            {
                selectText.text = "Press A to Select";
            }

        }
    }

    void all_player_model_select(KeyCode newChar)
    {
        // Get Renderer components of old and new models
        Renderer modelA = null;
        Renderer modelB = null;

        // Check which Player's model need to be changed
        if ((newChar == KeyCode.A || newChar == KeyCode.D) && P1Done == false)
        {
            currentModel = P1Current;
            if(newChar == KeyCode.D)
            {
                newSelect = Mathf.Abs(P1Current + 1) % numModels;
            }
            else if(newChar == KeyCode.A)
            {
                if (P1Current == 0)
                {
                    newSelect = numModels - 1;
                }
                else
                {
                    newSelect = P1Current - 1;
                }
            }
            modelA = P1Models[P1Current].GetComponent<Renderer>();
            modelB = P1Models[newSelect].GetComponent<Renderer>();
            P1cursor.transform.position = new Vector3(P1cursorX[newSelect], P1cursorY[newSelect], 0);
            P1Current = newSelect;
        }
        else if ((newChar == KeyCode.Z || newChar == KeyCode.X) && P2Done == false)
        {
            currentModel = P2Current;
            if (newChar == KeyCode.X)
            {
                newSelect = Mathf.Abs(P2Current + 1) % numModels;
            }
            else if (newChar == KeyCode.Z)
            {
                if (P2Current == 0)
                {
                    newSelect = numModels - 1;
                }
                else
                {
                    newSelect = P2Current - 1;
                }
            }
            modelA = P2Models[P2Current].GetComponent<Renderer>();
            modelB = P2Models[newSelect].GetComponent<Renderer>();
            P2cursor.transform.position = new Vector3(P2cursorX[newSelect], P2cursorY[newSelect], 0);
            P2Current = newSelect;
        }
        else if ((newChar == KeyCode.Q || newChar == KeyCode.W) && P3Done == false)
        {
            currentModel = P3Current;
            if (newChar == KeyCode.W)
            {
                newSelect = Mathf.Abs(P3Current + 1) % numModels;
            }
            else if (newChar == KeyCode.Q)
            {
                if (P3Current == 0)
                {
                    newSelect = numModels - 1;
                }
                else
                {
                    newSelect = P3Current - 1;
                }
            }
            modelA = P3Models[P3Current].GetComponent<Renderer>();
            modelB = P3Models[newSelect].GetComponent<Renderer>();
            P3cursor.transform.position = new Vector3(P3cursorX[newSelect], P3cursorY[newSelect], 0);
            P3Current = newSelect;
        }
        else if ((newChar == KeyCode.E || newChar == KeyCode.R) && P4Done == false)
        {
            currentModel = P4Current;
            if (newChar == KeyCode.E)
            {
                newSelect = Mathf.Abs(P4Current + 1) % numModels;
            }
            else if (newChar == KeyCode.R)
            {
                if (P4Current == 0)
                {
                    newSelect = numModels - 1;
                }
                else
                {
                    newSelect = P4Current - 1;
                }
            }
            modelA = P4Models[P4Current].GetComponent<Renderer>();
            modelB = P4Models[newSelect].GetComponent<Renderer>();
            P4cursor.transform.position = new Vector3(P4cursorX[newSelect], P4cursorY[newSelect], 0);
            P4Current = newSelect;
        }

        // Disable old model and enable new
        modelA.enabled = false;
        modelB.enabled = true;
        //Set current model to the newly enabled one
        currentModel = newModel;

        // Move cursor to selected icon
    }

    void character_select(int PlayerNumber)
    {
        if (PlayerNumber == 0 && P1Done == false)
        {
            P1Done = true;
            P1cursor.transform.localScale = new Vector3((float)0.2, (float)0.2, 1);
        }
        else if (PlayerNumber == 0 && P1Done == true)
        {
            P1Done = false;
            P1cursor.transform.localScale = new Vector3((float)0.1, (float)0.1, 1);
        }
        if (PlayerNumber == 1 && P2Done == false)
        {
            P2Done = true;
            P2cursor.transform.localScale = new Vector3((float)0.2, (float)0.2, 1);
        }
        else if (PlayerNumber == 1 && P2Done == true)
        {
            P2Done = false;
            P2cursor.transform.localScale = new Vector3((float)0.1, (float)0.1, 1);
        }
        if (PlayerNumber == 2 && P3Done == false)
        {
            P3Done = true;
            P3cursor.transform.localScale = new Vector3((float)0.2, (float)0.2, 1);
        }
        else if (PlayerNumber == 2 && P3Done == true)
        {
            P3Done = false;
            P3cursor.transform.localScale = new Vector3((float)0.1, (float)0.1, 1);
        }
        if (PlayerNumber == 3 && P4Done == false)
        {
            P4Done = true;
            P4cursor.transform.localScale = new Vector3((float)0.2, (float)0.2, 1);
        }
        else if (PlayerNumber == 3 && P4Done == true)
        {
            P4Done = false;
            P4cursor.transform.localScale = new Vector3((float)0.1, (float)0.1, 1);
        }
    }

    void move_left_team_select(Text Player)
    {
        if (P1TeamSelect.rectTransform.anchorMax.x > 0.25f)
        {
            P1TeamSelect.rectTransform.anchorMax = new Vector2(P1TeamSelect.rectTransform.anchorMax.x - 0.25f, 0.7f);
            P1TeamSelect.rectTransform.anchorMin = P1TeamSelect.rectTransform.anchorMax;

        }
    }
}
