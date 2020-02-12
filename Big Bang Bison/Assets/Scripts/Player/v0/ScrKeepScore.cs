using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrKeepScore : MonoBehaviour
{
    public GameObject Herd;
    public Material bisonP1Material;
    public Material bisonP2Material;
    public int bisonP1Count;
    public int bisonP2Count;
    public GameObject victoryText;
    public bool gameover = false;

    void Start()
    {
        Herd = GameObject.Find("Bison Herd");
        foreach(Transform bison in Herd.transform)
        {
            if (bison.GetComponent<BisonMovement>().teamAlliance == 1) {
                bisonP1Count++;
            }
            else if (bison.GetComponent<BisonMovement>().teamAlliance == 2) {
                bisonP2Count++;
            }
        }
    }

    void Update()
    {
        // count bison of both teams
        bisonP1Count = 0;
        bisonP2Count = 0;
        foreach(Transform bison in Herd.transform)
        {
            if (bison.GetComponent<BisonMovement>().onGround) {
                if (bison.GetComponent<BisonMovement>().teamAlliance == 1) {
                    bisonP1Count++;
                }
                else if (bison.GetComponent<BisonMovement>().teamAlliance == 2) {
                    bisonP2Count++;
                }
            }
        }

        // if bison on either team is 0, the other team wins
        if (bisonP1Count < 1) {
            if (!gameover) {
                victoryText.GetComponent<Text>().text = "BLUE WINS\nPress R to restart";
                gameover = true;
            }
        }
        else if (bisonP2Count < 1) {
            if (!gameover) {
                victoryText.GetComponent<Text>().text = "RED WINS\nPress R to restart";
                gameover = true;
            }
        }
        else {
            victoryText.GetComponent<Text>().text = "";
        }


        // able to reset level with R
        if (Input.GetKeyUp(KeyCode.R)) {
            SceneManager.LoadScene("SceneMain");
        }
    }
}
