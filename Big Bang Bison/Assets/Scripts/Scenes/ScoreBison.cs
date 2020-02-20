using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBison : MonoBehaviour
{
    //how long a bison needs to grow in a hill to be considered medium/full maturity (in seconds)
    public float mediumMaturity; 
    public float fullMaturity;
    //how many points a medium/full mature bison scores
    public int mediumMaturityPoints;
    public int fullMaturityPoints;
    PlayParticleSystem playPS;
    KeepScore keepScore;


    // Start is called before the first frame update
    void Start()
    {
        //may need to change this once we put in more than one particle system in the scene
        playPS = FindObjectOfType<PlayParticleSystem>();
        keepScore = FindObjectOfType<KeepScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RedBison"))
        {
            string maturity = CheckBisonMaturity(other);
            ScoreRed(maturity, other);
        }
        else if (other.CompareTag("BlueBison"))
        {
            string maturity = CheckBisonMaturity(other);
            ScoreBlue(maturity, other);
        }
    }

    string CheckBisonMaturity(Collider bison)
    {
        float bisonMaturity = bison.gameObject.GetComponent<HerdAgent>().growth;
        if (bisonMaturity >= mediumMaturity && bisonMaturity < fullMaturity)
        {
            return "mediumMaturity";
        }
        else if (bisonMaturity >= fullMaturity)
        {
            return "fullMaturity";
        }
        else
        {
            return "immature";
        }
        
    }

    void ScoreRed(string maturityLevel, Collider bison)
    {
        if (bison.gameObject.GetComponent<HerdAgent>().scoreable)
        {
            if (string.Equals(maturityLevel, "mediumMaturity"))
            {
                UpdateScoreAndPlayPS("red", mediumMaturityPoints);
                GetRidOfBison(bison);
            }
            else if (string.Equals(maturityLevel, "fullMaturity"))
            {
                UpdateScoreAndPlayPS("red", fullMaturityPoints);
                GetRidOfBison(bison);
            }
        }
    }

    void ScoreBlue(string maturityLevel, Collider bison)
    {
        if (bison.gameObject.GetComponent<HerdAgent>().scoreable)
        {
            if (string.Equals(maturityLevel, "mediumMaturity"))
            {
                UpdateScoreAndPlayPS("blue", mediumMaturityPoints);
                GetRidOfBison(bison);
            }
            else if (string.Equals(maturityLevel, "fullMaturity"))
            {
                UpdateScoreAndPlayPS("blue", fullMaturityPoints);
                GetRidOfBison(bison);
            }
        } 
    }

    void UpdateScoreAndPlayPS(string color, int points)
    {
        if (string.Equals(color, "red"))
        {
            keepScore.redPoints = keepScore.redPoints + points;
            playPS.isBlue = false;
        }
        else
        {
            keepScore.bluePoints = keepScore.bluePoints + points;
            playPS.isBlue = true;
        }
        playPS.active = true;
    }

    void GetRidOfBison(Collider bison)
    {
        bison.gameObject.transform.localScale = new Vector3(0, 0, 0);
        bison.gameObject.GetComponent<HerdAgent>().scoreable = false;
    }
}
