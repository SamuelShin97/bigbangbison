using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoints : MonoBehaviour
{
    private bool insideSendOff = false;
    public Text BlueText;
    public Text RedText;
    KeepScore keepScore;
    PlayParticleSystem playPS;

    // Start is called before the first frame update
    void Start()
    {
        BlueText.text = "Blue Points: 0";
        RedText.text = "Red Points: 0";
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
                if (bison.isMedium)
                {
                    //Debug.Log("scored 1 points");
                    Score(1);
                    
                    transform.position = new Vector3(99999, 99999, 99999);
                }
                else if (bison.isLarge)
                {
                    //Debug.Log("scored 2 points");
                    Score(2);
                    transform.position = new Vector3(99999, 99999, 99999);
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
        }
        else
        {
            keepScore.redPoints = keepScore.redPoints + points;
        }
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
