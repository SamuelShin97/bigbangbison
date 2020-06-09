using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinner : MonoBehaviour
{
    GameObject gameManager;
    Text text;
    public Text pointsBlue;
    public Text pointsRed;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int blueS = PlayerPrefs.GetInt("BlueScore");
        int redS = PlayerPrefs.GetInt("RedScore");
        pointsBlue.text = blueS.ToString();//ScoreTracker.Instance.pointsBlue;
        pointsRed.text = redS.ToString(); //ScoreTracker.Instance.pointsRed;
        
        string winner = gameManager.GetComponent<GameManager>().winner;
        if (winner == "blue")
        {
            text.color = Color.cyan;
            text.text = "Blue Team Wins!!!";
        }
        else if (winner == "red")
        {
            text.color = Color.magenta;
            text.text = "Pink Team Wins!!!";
        }
        else
        {
            text.color = Color.white;
            text.text = "Tie Game!!!";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
