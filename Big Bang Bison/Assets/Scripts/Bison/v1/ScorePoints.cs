using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoints : MonoBehaviour
{
    private bool insideSendOff = false;
    public Text blueText;
    public Text redText;
    private bool scoreable;
    KeepScore keepScore;
    PlayParticleSystem playPS;

    // Start is called before the first frame update
    void Start()
    {
        blueText.text = "Blue Points: 0";
        redText.text = "Red Points: 0";
        scoreable = true;
        keepScore = FindObjectOfType<KeepScore>();
        playPS = FindObjectOfType<PlayParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        HillInteraction bison = GetComponent<HillInteraction>();
        if (other.CompareTag("Send"))
        {
            insideSendOff = true;
            //Debug.Log("entered send off");
            yield return new WaitForSeconds(2);

            if (insideSendOff)
            {
                if (bison.isMedium && scoreable)
                {
                    //Debug.Log("scored 1 points");
                    Score(1);
                    playPS.medium = true;
                    transform.localScale = new Vector3(0, 0, 0);
                    scoreable = false;
                }
                else if (bison.isLarge && scoreable)
                {
                    //Debug.Log("scored 2 points");
                    Score(2);
                    playPS.medium = false;
                    transform.localScale = new Vector3(0, 0, 0);
                    scoreable = false;
                }

            }

        }
    }

    private bool BlueOrRed() //this function returns true if bison is blue, false if red
    {
        GameObject instance = gameObject;
        if (instance.CompareTag("BlueBison"))
        {
            Debug.Log("bison is blue");
            return true;
        }
        Debug.Log("bison is red");
        return false;
    }

    public void Score(int points)
    {
        if (BlueOrRed() == true)
        {
            keepScore.bluePoints = keepScore.bluePoints + points;
            playPS.isBlue = true;
        }
        else
        {
            keepScore.redPoints = keepScore.redPoints + points;
            playPS.isBlue = false;
        }
        playPS.active = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Send"))
        {
            Debug.Log("leaving send off");
            insideSendOff = false;
        }
    }
}
