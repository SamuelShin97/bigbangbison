using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineKeepScore : MonoBehaviour
{
    public Text blueText;
    public Text redText;
    public int bluePoints;
    public int redPoints;
    

    // Start is called before the first frame update
    void Start()
    {
        blueText.text = "0";
        redText.text = "0";
        bluePoints = 0;
        redPoints = 0;
        PlayerPrefs.SetInt("BlueScore", bluePoints);
        PlayerPrefs.SetInt("RedScore", redPoints);
    }

    // Update is called once per frame
    void Update()
    {
        blueText.text =  bluePoints.ToString();
        redText.text =  redPoints.ToString();
        PlayerPrefs.SetInt("BlueScore", bluePoints);
        PlayerPrefs.SetInt("RedScore", redPoints);
    }
}
