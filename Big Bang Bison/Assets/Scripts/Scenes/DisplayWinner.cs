using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinner : MonoBehaviour
{
    GameObject gameManager;
    Text text;
    
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string winner = gameManager.GetComponent<GameManager>().winner;
        if (winner == "blue")
        {
            text.color = Color.blue;
            text.text = "Blue Team Wins!!!";
        }
        else if (winner == "red")
        {
            text.color = Color.red;
            text.text = "Red Team Wins!!!";
        }
        else
        {
            text.color = Color.magenta;
            text.text = "Tie Game!!!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
