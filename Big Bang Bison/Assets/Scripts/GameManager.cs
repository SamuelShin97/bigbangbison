using UnityEngine;
using System.Collections;

using System.Collections.Generic;        //Allows us to use Lists. 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text text;
    public float gameTime_seconds; //in seconds
    public float speed = 1;
    public string winner;
    public GameObject hills;
    Transform[] hillCollection;
    KeepScore keepScore;
    float timePassed_seconds; //in seconds;
    public int hillFrequency_seconds; //in seconds
    
    //Awake is always called before any Start functions
    void Awake()
    {
        DontDestroyOnLoad(this);
        keepScore = FindObjectOfType<KeepScore>();
        winner = "hi";
        hillCollection = hills.GetComponentsInChildren<Transform>();
        timePassed_seconds = 0.0f;

        hillCollection = ConstructHillArray(hillCollection);
    }

    //Update is called every frame.
    void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            gameTime_seconds -= Time.deltaTime * speed;
            timePassed_seconds += Time.deltaTime * speed;
            string minutes = Mathf.Floor((gameTime_seconds % 3600) / 60).ToString("00");
            string seconds = (gameTime_seconds % 60).ToString("00");
            text.text = minutes + ":" + seconds;

            if (gameTime_seconds <= 0.0)
            {
                winner = DetermineWinner();
                Debug.Log("end game");
                SceneManager.LoadScene(2);
            }

            HillChange(timePassed_seconds);
        }
    }

    string DetermineWinner()
    {
        int bluePoints = keepScore.bluePoints;
        int redPoints = keepScore.redPoints;

        if (bluePoints > redPoints)
        {
            return "blue";
        }
        else if (redPoints > bluePoints)
        {
            return "red";
        }
        return "tie";
    }

    Transform[] ConstructHillArray(Transform[] hillArray)
    {
        Transform[] newHillArray = new Transform[3];
        int index = 0;
        foreach(Transform child in hillArray)
        {
            if (child.CompareTag("Hill"))
            {
                //Debug.Log(child.name);
                newHillArray[index] = child;
                index++;
            }
        }
        return newHillArray;
    }

    void HillChange(float timePassed)
    {
        
    }
}
