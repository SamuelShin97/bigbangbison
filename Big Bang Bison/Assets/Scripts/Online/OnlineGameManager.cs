using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;        //Allows us to use Lists. 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class OnlineGameManager : NetworkBehaviour
{
    public Text timerText;
    [SyncVar]
    public float gameTime_seconds; //in seconds
    public float speed = 0;
    public string winner;
    public GameObject hills;
    public int hillFrequency_seconds; //in seconds
    Transform[] hillCollection;
    OnlineKeepScore keepScore;
    float timePassed_seconds; //in seconds;
    bool hillChange = false;
    [SyncVar]
    Transform activeHill;
    [SyncVar]
    int index;
    List<int> activatedHills = new List<int>();
    public Image TimerBackground;
    public Image timerColor;
    Animator timerAnimator;
    Animator timerEnding;
    GameObject[] crystalSpawners;
    GameObject[] desertSpawners;
    GameObject[] tropicalSpawners;
    BisonSpawn bisonSpawn;
    public int bisonCount;
    public GameObject endText1;
    public GameObject endText2;

    //Awake is always called before any Start functions
    void Awake()
    {
        timerText = GameObject.Find("TimeText").GetComponent<Text>();
        hills = GameObject.Find("Hills");
        TimerBackground = GameObject.Find("CurrentBiome").GetComponent<Image>();
        timerColor = GameObject.Find("BiomeIndicator").GetComponent<Image>();
        endText1 = GameObject.Find("Time");
        endText2 = GameObject.Find("Up");

        Debug.Log("online game manager awake");
        DontDestroyOnLoad(this);
        keepScore = FindObjectOfType<OnlineKeepScore>();
        Debug.Log(keepScore);
        winner = "hi";
        hillCollection = hills.GetComponentsInChildren<Transform>();
        timePassed_seconds = 1.0f;
        
        hillCollection = ConstructHillArray(hillCollection);
        foreach(Transform element in hillCollection)
        {
            element.gameObject.SetActive(false);
        }
        index = Random.Range(0, hillCollection.Length);
        activeHill = hillCollection[index];
        activeHill.gameObject.SetActive(true);
        activatedHills.Add(index);
        timerAnimator = timerColor.GetComponent<Animator>();
        timerEnding = TimerBackground.GetComponent<Animator>();
        changeTimerColor(activeHill);
        if (timerAnimator != null)
        {
            Debug.Log("animator found");
        }
        crystalSpawners = GameObject.FindGameObjectsWithTag("crystalSpawner");
        desertSpawners = GameObject.FindGameObjectsWithTag("desertSpawner");
        tropicalSpawners = GameObject.FindGameObjectsWithTag("tropicalSpawner");
        bisonSpawn = crystalSpawners[0].GetComponent<BisonSpawn>();
    }

    private void Start()
    {
        Debug.Log("online game manager start");
        determineSpawnLocation(bisonCount);
    }

    //Update is called every frame.
    void Update()
    {
        
        //Debug.Log("online game manager update");
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 6)
        {
            gameTime_seconds -= Time.deltaTime * speed;
            timePassed_seconds += Time.deltaTime * speed;
            int timePassedTruncated = (int)(timePassed_seconds);
            string minutes = Mathf.Floor((gameTime_seconds % 3600) / 60).ToString("00");
            string seconds = (gameTime_seconds % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;

            if (gameTime_seconds <= 0.0)
            {
                winner = DetermineWinner();
                Debug.Log("end game");
                DOTween.Sequence()
                .Append(endText1.transform.DOScale(1f, 0.25f))
                .Append(endText2.transform.DOScale(1f, 0.25f))
                .AppendInterval(2.5f)
                .OnComplete(LoadNextScene);
            }
            HillChange(timePassedTruncated);
        }
    }

    void LoadNextScene(){ SceneManager.LoadScene(3); }

    void SpawnAtLocation(GameObject[] spawners, int count)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            //spawners[i].GetComponent<BisonSpawn>().SpawnAll(count);
        }
    }

    void determineSpawnLocation(int count)
    {
        if (activeHill.gameObject.name == "Crystal Hill")
        {
            SpawnAtLocation(crystalSpawners, count);
            /*foreach (BisonSpawn spawn in crystalSpawners)
             {
                 spawn.SpawnAll(4);
             }*/
        }
        else if (activeHill.gameObject.name == "Desert Hill")
        {
            SpawnAtLocation(desertSpawners, count);
            /*foreach (BisonSpawn spawn in desertSpawners)
            {
                spawn.SpawnAll(4);
            }*/
        }
        else if (activeHill.gameObject.name == "Tropical Hill")
        {
            SpawnAtLocation(tropicalSpawners, count);
            /*foreach (BisonSpawn spawn in tropicalSpawners)
            {
                spawn.SpawnAll(4);
            }*/
        }
    }

    string DetermineWinner()
    {
        int bluePoints = keepScore.bluePoints;
        int redPoints = keepScore.redPoints;

        if (bluePoints > redPoints)
        {
            return "blue";
        }
        else if (redPoints > bluePoints)
        {
            return "red";
        }
        return "tie";
    }

    Transform[] ConstructHillArray(Transform[] hillArray)
    {
        Transform[] newHillArray = new Transform[3];
        int index = 0;
        foreach(Transform child in hillArray)
        {
            if (child.CompareTag("Hill"))
            {
                //Debug.Log(child.name);
                newHillArray[index] = child;
                index++;
                //Debug.Log("Hill name: " + child.GetComponent<GameObject>().name);
            }
        }
        return newHillArray;
    }


    void HillChange(float timePassed)
    {
        if(timePassed == 55f || timePassed == 115f || timePassed == 175f)
        {
            Debug.Log("Biome about to change!");
            fadeTimerColor(activeHill);
        }

        if (timePassed == hillFrequency_seconds)
        {
            if (!hillChange)
            {
                Debug.Log("change hill");

                activeHill.gameObject.SetActive(false);
                activeHill = hillCollection[RandomSelectionExcept()];
                activeHill.gameObject.SetActive(true);
                changeTimerColor(activeHill);
                determineSpawnLocation(bisonCount);
                hillChange = true;
                timePassed_seconds = 0.0f;
            }
        }
        else
        {
            hillChange = false;
        }
    }

    int RandomSelectionExcept()
    {
        int rng = -1;
        do
        {
            rng = Random.Range(0, hillCollection.Length);
        }
        while (activatedHills.Contains(rng));

        activatedHills.Add(rng);
        
        if (activatedHills.Count >= 3)
        {
            ReseetActivatedHills();
        }
        return rng;
    }

    void ReseetActivatedHills()
    {
        activatedHills.Clear();
    }

    void changeTimerColor(Transform hill)
    {
        if(hill.gameObject.name == "Crystal Hill")
        {
            //timerColor.GetComponent<Image>().color = new Color32(29, 226, 110, 255);
            timerEnding.Play("Crystal Timer");
            //get access to Caetano's spawning script and call the spawning function
            
            //for each spawner in the zone
        }
        else if (hill.gameObject.name == "Desert Hill")
        {
            //timerColor.GetComponent<Image>().color = new Color32(253, 224, 32, 255);
            timerEnding.Play("Desert Timer");
        }
        else if (hill.gameObject.name == "Tropical Hill")
        {
            //timerColor.GetComponent<Image>().color = new Color32(216, 53, 172, 255);
            timerEnding.Play("Tropical Timer");
        }
    }
    
    void fadeTimerColor(Transform hill)
    {
        // timerAnimator.Play("EndDesert");
         if (hill.gameObject.name == "Desert Hill" || hill.gameObject.name == "Crystal Hill")
        {
            timerAnimator.Play("EndDesert");
        }
        else if (hill.gameObject.name == "Tropical Hill")
        {
            timerAnimator.Play("EndTropical");
        }
    }
}
