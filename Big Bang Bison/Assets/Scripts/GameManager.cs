using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;        //Allows us to use Lists. 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
//using Mirror;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public float gameTime_seconds; //in seconds
    public float speed = 0;
    public string winner;
    public GameObject hills;
    public int hillFrequency_seconds; //in seconds
    public int[] scores; // 0 - Blue team, 1 - Red team
    Transform[] hillCollection;
    KeepScore keepScore;
    float timePassed_seconds; //in seconds;
    bool hillChange = false;
    bool transition = false;
    Transform activeHill;
    int index;
    List<int> activatedHills = new List<int>();
    // UI/UX
    public Image TimerBackground;
    public Image timerColor;
    public Image timeMarker; 
    public Image[] hillTimers = new Image[3];
    public Image[] timeSections = new Image[3];
    float transitionTimer = 3;
    int currentSection = 0;

    GameObject[] crystalSpawners;
    GameObject[] desertSpawners;
    GameObject[] tropicalSpawners;
    BisonSpawn bisonSpawn;
    public int bisonCount; //can't be greater than 5
    public GameObject endText1;
    public GameObject endText2;
    [SerializeField] private float transitionTime = 10.0f;
    ListOfNames m_listOfNames = new ListOfNames();
    List<int> usedIndexes = new List<int>();
    string[] bisonNames;
    int bisonNamesindex = 0;
    [SerializeField] GameObject herd1;
    [SerializeField] GameObject herd2;
    [SerializeField] int maxBisonAllowed;
    bool refreshed;

    //sending data to parameterSetByName
    private ParameterSetByName parameterSetByName;


    //Awake is always called before any Start functions
    void Awake()
    {
        
    }

    void Start()
    {
        foreach(Image x in hillTimers) { x.color = new Color(255, 255, 255, 0f); }
        parameterSetByName = GameObject.FindObjectOfType<ParameterSetByName>();
        DontDestroyOnLoad(this);
        keepScore = FindObjectOfType<KeepScore>();
        Debug.Log(keepScore);
        winner = "hi";
        hillCollection = hills.GetComponentsInChildren<Transform>();
        timePassed_seconds = 1.0f;

        hillCollection = ConstructHillArray(hillCollection);
        foreach (Transform element in hillCollection)
        {
            element.gameObject.SetActive(false);
        }
        index = Random.Range(0, hillCollection.Length);
        activeHill = hillCollection[index];
        activeHill.gameObject.SetActive(true);
        activatedHills.Add(index);
        changeTimerColor(activeHill);

        crystalSpawners = GameObject.FindGameObjectsWithTag("crystalSpawner");
        desertSpawners = GameObject.FindGameObjectsWithTag("desertSpawner");
        tropicalSpawners = GameObject.FindGameObjectsWithTag("tropicalSpawner");
        bisonSpawn = crystalSpawners[0].GetComponent<BisonSpawn>();

        int totalBison = bisonCount * 2 * (crystalSpawners.Length + desertSpawners.Length + tropicalSpawners.Length);
        bisonNames = new string[totalBison];
        bisonNames = GenerateNames(totalBison);
        for (int k = 0; k < bisonNames.Length; k++)
        {
            //Debug.Log("bisonNames: " + bisonNames[k]);
            //Debug.Log("bisonNames: " + bisonNames[k]);
        }
        determineSpawnLocation(bisonCount, 0);
        parameterSetByName.TimeGet(gameTime_seconds, activeHill);
        refreshed = false;
        // Start time marker
        timeMarker.GetComponent<Transform>().DOLocalRotate(new Vector3(0f, 0f, 0f), 360f, RotateMode.Fast).SetEase(Ease.Linear);
    }

    //Update is called every frame.
    void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        parameterSetByName.TimeGet(gameTime_seconds, activeHill);
        if (currentScene == 2 || currentScene == 4) //if game scene
        {
            gameTime_seconds -= Time.deltaTime * speed;
            timePassed_seconds += Time.deltaTime * speed;
            int timePassedTruncated = (int)(timePassed_seconds);
            string minutes = Mathf.Floor((gameTime_seconds % 3600) / 60).ToString("0");
            string seconds = (gameTime_seconds % 60).ToString("00");

            if (seconds == "60")
            {
                seconds = "59";
                //int minute = System.Convert.ToInt32(minutes);
                //minute -= 1;
                //minutes = minute.ToString("0");
            }
            
            if (gameTime_seconds >= 0.0f)
            {
                // to prevent the timer from going lower than 0:00
                timerText.text = minutes + ":" + seconds;
            }

            //if(timePassed_seconds >= 5)
            if (gameTime_seconds <= 0.0)
            {
                //LoadNextScene();
                winner = DetermineWinner();
                DOTween.Sequence()
                .Append(endText1.transform.DOScale(1f, 0.25f))
                .Append(endText2.transform.DOScale(1f, 0.25f));
                // countdown transition to next scene
                transitionTimer -= Time.deltaTime * speed;
                if(transitionTimer <= 0) { LoadNextScene(); }
            }
            if (timePassedTruncated % 30 == 0 && timePassedTruncated % hillFrequency_seconds != 0)
            {
                Debug.Log("refresh spawn");
                if (!refreshed)
                {
                    refreshed = true;
                    int totalBison = herd1.GetComponentsInChildren<HerdAgent>().Length + herd2.GetComponentsInChildren
                                <HerdAgent>().Length;
                    determineSpawnLocation(bisonCount, totalBison);
                    refreshed = false;
                }

            }
            StartCoroutine(HillChange(timePassedTruncated));
            //TransitionHills(timePassedTruncated);
        }
    }

    void LoadNextScene() { SceneManager.LoadScene(3); }

    string[] GenerateNames(int count)
    {
        string[] setOfNames = new string[count];
        for (int i = 0; i < count; i++)
        {
            int index = RandomBisonName();
            setOfNames[i] = m_listOfNames.names[index];
        }
        return setOfNames;
    }

    int RandomBisonName()
    {
        int rng = -1;
        int length = m_listOfNames.GetLength();

        do
        {
            rng = Random.Range(0, length);
            //Debug.Log("repeat");
        }
        while (usedIndexes.Contains(rng));

        usedIndexes.Add(rng);


        if (usedIndexes.Count >= length)
        {
            ClearUsedIndexes();
        }

        //Debug.Log("index: " + rng);
        return rng;
    }

    void ClearUsedIndexes()
    {
        usedIndexes.Clear();
    }

    string [] HowManyBisonToSpawn(int count, int bisonToSpawn)
    {
        string[] nameSet;

        /*if (bisonToSpawn >= count)
        {
            nameSet = new string[count];
        }*/
        //else
        //{
        nameSet = new string[bisonToSpawn];
        //}

        for (int j = 0; j < nameSet.Length; j++)
        {
            if (bisonNamesindex == bisonNames.Length - 1)
            {
                bisonNamesindex = 0;
            }
            nameSet[j] = bisonNames[bisonNamesindex];
            bisonNamesindex++;
        }
        return nameSet;
    }

    void SpawnAtLocation(GameObject[] spawners, int count, int totalBison)
    {
        if (totalBison < maxBisonAllowed)
        {
            int cyanBison = herd1.GetComponentsInChildren<HerdAgent>().Length;
            int pinkBison = herd2.GetComponentsInChildren<HerdAgent>().Length;
            int cyanBisonToSpawn = (maxBisonAllowed / 2) - cyanBison;
            int pinkBisonToSpawn = (maxBisonAllowed / 2) - pinkBison;

            Debug.Log("cyanBisonToSpawn: " + cyanBisonToSpawn);
            Debug.Log("pinkBisonToSpawn: " + pinkBisonToSpawn);

            int iterations = 0;
            int index = 0;

            string[] nameSetCyan = HowManyBisonToSpawn(count, cyanBisonToSpawn);
            string[] nameSetPink = HowManyBisonToSpawn(count, pinkBisonToSpawn);

            while (cyanBisonToSpawn > 0 || pinkBisonToSpawn > 0)
            {
                index = (iterations) % 3;
                Debug.Log("index " + index);
                if (cyanBisonToSpawn > 0)
                {
                    Debug.Log("spawning cyan");
                    
                    spawners[index].GetComponent<BisonSpawn>().Spawn(1, nameSetCyan[iterations], "cyan");
                    //wait = true;
                    cyanBisonToSpawn--;
                }
                
                if (pinkBisonToSpawn > 0)
                {
                    Debug.Log("spawning pink");
                    
                    spawners[index].GetComponent<BisonSpawn>().Spawn(1, nameSetPink[iterations], "pink");
                    //wait = true;
                    pinkBisonToSpawn--;
                }
                iterations++;
            }
            /*for (int i = 0; i < spawners.Length; i++)
            {
                //Debug.Log("spawners length " + spawners.Length);
                
                Debug.Log("cyanBisonToSpawn: " + cyanBisonToSpawn);
                Debug.Log("pinkBisonToSpawn: " + pinkBisonToSpawn);
                //bool wait = false;

                if (cyanBisonToSpawn > 0)
                {
                    Debug.Log("spawning cyan");
                    string[] nameSet = HowManyBisonToSpawn(count, cyanBisonToSpawn);
                    spawners[i].GetComponent<BisonSpawn>().Spawn(nameSet.Length, nameSet, "cyan");
                    //wait = true;
                }
                
                if (pinkBisonToSpawn > 0)
                {
                    Debug.Log("spawning pink");
                    string[] nameSet = HowManyBisonToSpawn(count, pinkBisonToSpawn);
                    spawners[i].GetComponent<BisonSpawn>().Spawn(nameSet.Length, nameSet, "pink");
                    //wait = true;
                }
                
            }*/
        }
    }

    void determineSpawnLocation(int count, int totalBison)
    {
        if (activeHill.gameObject.name == "Crystal Hill")
        {
            SpawnAtLocation(crystalSpawners, count, totalBison);
            
        }
        else if (activeHill.gameObject.name == "Desert Hill")
        {
            SpawnAtLocation(desertSpawners, count, totalBison);
            
        }
        else if (activeHill.gameObject.name == "Tropical Hill")
        {
           SpawnAtLocation(tropicalSpawners, count, totalBison);
            
        }
    }

    void DestroyUnusedBison(GameObject herd)
    {
        HerdAgent[] agents = herd.GetComponentsInChildren<HerdAgent>();
        foreach (HerdAgent agent in agents)
        {
            if (agent.hasMoved == false)
            {
                agent.readyToRemove = true;
            }
        }
    }

    string DetermineWinner()
    {
        int bluePoints = keepScore.bluePoints;
        int redPoints = keepScore.redPoints;

        scores = new int[] { bluePoints, redPoints };

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

    IEnumerator HillChange(float timePassed)
    {
        float warningTime = timePassed % hillFrequency_seconds;
        if (warningTime - hillFrequency_seconds == -5)
        {
            Debug.Log("Biome about to change!");
        }

        if (timePassed == hillFrequency_seconds)
        {
            if (!hillChange)
            {
                //Debug.Log("change hill");
                hillChange = true;
                DestroyUnusedBison(herd1);
                DestroyUnusedBison(herd2);
                activeHill.gameObject.SetActive(false);
                activeHill = hillCollection[RandomSelectionExcept()];
                Debug.Log("next hill is " + activeHill);
                yield return new WaitForSeconds(0.5f);
                int totalBison = herd1.GetComponentsInChildren<HerdAgent>().Length + herd2.GetComponentsInChildren
                                <HerdAgent>().Length;
                
                determineSpawnLocation(bisonCount, totalBison);
                activeHill.gameObject.SetActive(true);
                changeTimerColor(activeHill);
                
                //transition = true;
                timePassed_seconds = 0.0f;
            }
        }
        else
        {
            hillChange = false;
        }
    }

    void TransitionHills(float timePassed)
    {
        if (transition)
        {
            if (timePassed == transitionTime)
            {
                transition = false;
                Debug.Log("activating hill");
                activeHill.gameObject.SetActive(true);
                changeTimerColor(activeHill);
                timePassed_seconds = 0.0f;
                
            }
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
            // StartCaveTimer();
            makeCaveColor(currentSection);
        }
        else if (hill.gameObject.name == "Desert Hill")
        {
            // StartDesertTimer();
            makeDesertColor(currentSection);
        }
        else if (hill.gameObject.name == "Tropical Hill")
        {
            //StartForestTimer();
            makeForestColor(currentSection);
        }
        currentSection++;
    }
    
    void StartCaveTimer()
    {
        DOTween.Sequence()
        .Append(hillTimers[0].GetComponent<Image>().DOColor(new Color(255, 255, 255, 1), 0.1f).SetEase(Ease.Linear))
        .Append(hillTimers[0].GetComponent<Transform>().DORotate(new Vector3(0, 0, -180), 120f).SetEase(Ease.Linear))
        .Append(hillTimers[0].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.1f).SetEase(Ease.Linear));
    }

    void StartDesertTimer()
    {
        DOTween.Sequence()
        .Append(hillTimers[1].GetComponent<Image>().DOColor(new Color(255, 255, 255, 1), 0.1f).SetEase(Ease.Linear))
        .Append(hillTimers[1].GetComponent<Transform>().DORotate(new Vector3(0, 0, -180), 120f).SetEase(Ease.Linear))
        .Append(hillTimers[1].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.1f).SetEase(Ease.Linear));
    }

    void StartForestTimer()
    {
        DOTween.Sequence()
        .Append(hillTimers[2].GetComponent<Image>().DOColor(new Color(255, 255, 255, 1), 0.1f).SetEase(Ease.Linear))
        .Append(hillTimers[2].GetComponent<Transform>().DORotate(new Vector3(0, 0, -180), 120f).SetEase(Ease.Linear))
        .Append(hillTimers[2].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.1f).SetEase(Ease.Linear));
    }

    void makeCaveColor(int num)
    {
        timeSections[num].GetComponent<Image>().color = new Color32(17, 188, 0, 255);
    }
    void makeDesertColor(int num)
    {
        timeSections[num].GetComponent<Image>().color = new Color32(227, 193, 0, 255);
    }
    void makeForestColor(int num)
    {
        timeSections[num].GetComponent<Image>().color = new Color32(226, 61, 217, 255);
    }
}
