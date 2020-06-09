using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContSelect : MonoBehaviour
{

    private bool[] Panels = new bool[4];
    private bool[] Controllers = new bool[4];
    protected int[] players = new int [4];
    private string buttonStir;
    private Panel_Control panel_Control;
    void Awake()
    {
        panel_Control = GameObject.FindObjectOfType<Panel_Control> ();
    }
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            players[i] = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if (Panels[3] == false)
        {
            for (int j = 1; j < 5;j++)
            {
                buttonStir = "A" + (j);
                
                if (Controllers[j-1] == false && Input.GetButton(buttonStir) == true)
                {
                    
                    Controllers[j-1] = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Panels[i] == false)
                        {
                            setController(i, j);
                            Panels[i] = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    void setController(int panel, int contNumber)
    {
        Debug.Log(contNumber+"pan"+panel);
        players[panel] = contNumber;
        panel_Control.ContNumPass(players[panel], panel);
    }
    
}
