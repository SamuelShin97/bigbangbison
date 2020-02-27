using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{
    public Text blueText;
    public Text redText;
    public int bluePoints;
    public int redPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        blueText.text = "Blue Points: 0";
        redText.text = "Red Points: 0";
        bluePoints = 0;
        redPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        blueText.text = "Blue Points: " + bluePoints.ToString();
        redText.text = "Red Points: " + redPoints.ToString();
        */
        blueText.text =  bluePoints.ToString();
        redText.text =  redPoints.ToString();

    }
}
