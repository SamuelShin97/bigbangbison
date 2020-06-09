using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnlineScoreBison : MonoBehaviour
{

    //Sound stuff
    [FMODUnity.EventRef]
    public string soundMid;
    [FMODUnity.EventRef]
    public string soundFull;
    FMOD.Studio.EventInstance soundMidPlay;
    FMOD.Studio.EventInstance soundFullPlay;
    
    
    //how long a bison needs to grow in a hill to be considered medium/full maturity (in seconds)
    //public float mediumMaturity; 
    //public float fullMaturity;
    //how many points a medium/full mature bison scores
    public int mediumMaturityPoints;
    public int fullMaturityPoints;
    public PlayParticleSystem playPSBall;
    public PlayParticleSystem playPSBurst;
    public PlayParticleSystem playPSCharge;
    KeepScore keepScore;
    HerdAgent herdAgent;
    private float mediumMaturity;
    private float fullMaturity;
    public Text redScore;
    public Text blueScore;
    

    Animator red;
    Animator blue;

    // Start is called before the first frame update
    void Start()
    {
        //may need to change this once we put in more than one particle system in the scene
        //playPS = FindObjectOfType<PlayParticleSystem>();
        keepScore = FindObjectOfType<KeepScore>();
        herdAgent = FindObjectOfType<HerdAgent>();
        mediumMaturity = herdAgent.mediumMaturity;
        fullMaturity = herdAgent.fullMaturity;
        red = redScore.GetComponent<Animator>();
        blue = blueScore.GetComponent<Animator>();
        soundMidPlay = FMODUnity.RuntimeManager.CreateInstance(soundMid);
        soundFullPlay = FMODUnity.RuntimeManager.CreateInstance(soundFull);
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundMidPlay, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundFullPlay, GetComponent<Transform>(), GetComponent<Rigidbody>());
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
        Debug.Log("hit hahash");
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
        //if (bison.gameObject.GetComponent<HerdAgent>().scoreable)
        //{
            if (string.Equals(maturityLevel, "mediumMaturity"))
            {
                soundMidPlay.start();
                UpdateScoreAndPlayPS("red", mediumMaturityPoints);
                GetRidOfBison(bison);
            }
            else if (string.Equals(maturityLevel, "fullMaturity"))
            {
                soundFullPlay.start();
                UpdateScoreAndPlayPS("red", fullMaturityPoints);
                GetRidOfBison(bison);
            }
        //}
    }

    void ScoreBlue(string maturityLevel, Collider bison)
    {
        //if (bison.gameObject.GetComponent<HerdAgent>().scoreable)
        //{
            if (string.Equals(maturityLevel, "mediumMaturity"))
            {
                soundMidPlay.start();
                UpdateScoreAndPlayPS("blue", mediumMaturityPoints);
                GetRidOfBison(bison);
            }
            else if (string.Equals(maturityLevel, "fullMaturity"))
            {
                soundFullPlay.start();
                UpdateScoreAndPlayPS("blue", fullMaturityPoints);
                GetRidOfBison(bison);
            }
        //} 
    }

    void UpdateScoreAndPlayPS(string color, int points)
    {
        if (string.Equals(color, "red"))
        {
            red.Play("Red Score");
            keepScore.redPoints = keepScore.redPoints + points;
            playPSBall.isBlue = false;
            playPSBurst.isBlue = false;
            playPSCharge.isBlue = false;
        }
        else
        {
            blue.Play("Blue Score");
            keepScore.bluePoints = keepScore.bluePoints + points;
            playPSBall.isBlue = true;
            playPSBurst.isBlue = true;
            playPSCharge.isBlue = true;
        }
        playPSBall.active = true;
        playPSBurst.active = true;
        playPSCharge.active = true;
    }

    void GetRidOfBison(Collider bison)
    {
        bison.gameObject.transform.localScale = new Vector3(9999, 9999, 9999);
        //bison.gameObject.GetComponent<HerdAgent>().scoreable = false;
        bison.gameObject.GetComponentInChildren<Light>().enabled = false;
        bison.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
