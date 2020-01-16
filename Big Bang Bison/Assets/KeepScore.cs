using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{
    public Text BlueText;
    public Text RedText;
    public int bluePoints;
    public int redPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        BlueText.text = "Blue Points: 0";
        RedText.text = "Red Points: 0";
        bluePoints = 0;
        redPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BlueText.text = "Blue Points: " + bluePoints.ToString();
        RedText.text = "Red Points: " + redPoints.ToString();
    }
}
