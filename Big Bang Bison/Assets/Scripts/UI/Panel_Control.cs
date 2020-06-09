using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Panel_Control : MonoBehaviour
{
    // Wait state objects
    private Image bg;
    private GameObject text;
    private SpriteRenderer button;
    // Select and Ready State Objects
    private GameObject[] models = new GameObject[4];
    private GameObject[] icons = new GameObject[4];
    private GameObject[] abilityNames = new GameObject[4];
    private Color[] colors = new Color[2];
    public GameObject[] teamIcons = new GameObject[2];
    private GameObject[] arrows = new GameObject[4];
    private GameObject promptPanel;
    private GameObject box;
    //private GameObject charColor;
    public Material[] materials;
    private int currentChar, currentColor, currentRow = 0;
    public int panelState = 0;
    //Setup for controller
    private int time = 10;
    public int ControllerNum = 0;
    public bool ContSet = false;
    private string ContStringA;
    private string ContStringB;
    private string ContStringH;
    private string ContStringV;
    private float _LastX, _LastY;
    public int panelNum;
    private float timerH, timerV;
    private bool moveH, moveV = true;
    private GamePlayManager gamePlayManager;
    private bool done;

    //UI SFX
    [FMODUnity.EventRef]
    public string startA;

    [FMODUnity.EventRef]
    public string arrow;

    [FMODUnity.EventRef]
    public string confirm;

    [FMODUnity.EventRef]
    public string backB;

    FMOD.Studio.EventInstance startAInst;

    FMOD.Studio.EventInstance arrowInst;

    FMOD.Studio.EventInstance confirmInst;

    FMOD.Studio.EventInstance backBInst;


    void Awake()
    {
        //allows data to be sent to gamePlayManager script
        gamePlayManager = GameObject.FindObjectOfType<GamePlayManager>();
    }
    void Start()
    {
        bg = transform.Find("Join BG").gameObject.GetComponent<Image>();
        text = transform.Find("Join Prompt").gameObject;
        button = transform.Find("Button A (1)").gameObject.GetComponent<SpriteRenderer>();
        // Get models and icons
        models[0] = transform.Find("Model Pull").gameObject;
        models[1] = transform.Find("Model Push").gameObject;
        models[2] = transform.Find("Model Wall").gameObject;
        models[3] = transform.Find("Model Teleport").gameObject;
        icons[0] = transform.Find("Icon Pull").gameObject;
        icons[1] = transform.Find("Icon Push").gameObject;
        icons[2] = transform.Find("Icon Wall").gameObject;
        icons[3] = transform.Find("Icon Teleport").gameObject;
        abilityNames[0] = transform.Find("Name Pull").gameObject;
        abilityNames[1] = transform.Find("Name Push").gameObject;
        abilityNames[2] = transform.Find("Name Wall").gameObject;
        abilityNames[3] = transform.Find("Name Teleport").gameObject;
        arrows[0] = transform.Find("Arrow Icon Left").gameObject;
        arrows[1] = transform.Find("Arrow Icon Right").gameObject;
        arrows[2] = transform.Find("Arrow Color Left").gameObject;
        arrows[3] = transform.Find("Arrow Color Right").gameObject;
        box = transform.Find("Current Row").gameObject;
        //colors[0] = new Color32(0, 255, 233, 255);
        //colors[1] = new Color32(245, 10, 233, 255);
        //colors[2] = new Color32(195, 118, 0, 255);
        //colors[3] = new Color32(34, 195, 0, 255);
        //charColor = transform.Find("Color Select").gameObject;
        promptPanel = transform.Find("Player Ready").gameObject;

        // Derender models and icons
        for (int i = 1; i < models.Length; i++)
        {
            models[i].SetActive(false);
            icons[i].transform.localScale = new Vector3(12f, 12f, 1f);
        }
        //charColor.GetComponent<Image>().color = colors[currentColor];
        box.SetActive(false);
        //charColor.SetActive(false);
        promptPanel.SetActive(false);
        teamIcons[0].SetActive(false);
        teamIcons[1].SetActive(false);

        //UI Sound
        startAInst = FMODUnity.RuntimeManager.CreateInstance(startA);
        arrowInst = FMODUnity.RuntimeManager.CreateInstance(arrow);
        confirmInst = FMODUnity.RuntimeManager.CreateInstance(confirm);
        backBInst = FMODUnity.RuntimeManager.CreateInstance(backB);
        panelManager();
    }

    internal void ContPass(int v)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timerH + 0.1f) { moveH = true; }
        if(Time.time > timerV + 0.1f) { moveV = true; }
        //ControllerNum = _ContSelect.P1;
        if (ControllerNum > 0 && ContSet == false)
        {
            
            ContSet = true;
            ContStringA = "A" + ControllerNum;
            ContStringB = "B" + ControllerNum;
            ContStringH = "Horizontal" + ControllerNum;
            ContStringV = "Vertical" + ControllerNum;
        }
        if(ContSet == true){
            float x = Input.GetAxisRaw(ContStringH);
            float y = Input.GetAxis(ContStringV);
            if (_LastX != x)
            {
                if (moveH)
                {
                    if (Input.GetAxis(ContStringH) == -1 && panelState == 1) { move_left(); }
                    else if (Input.GetAxis(ContStringH) == 1 && panelState == 1) { move_right(); }
                }
            }
            if (_LastY != y)
            {
                if (moveV)
                {
                    if (Input.GetAxis(ContStringV) == -1 && panelState == 1) { row_up(); }
                    else if (Input.GetAxis(ContStringV) == 1 && panelState == 1) { row_down(); }
                }
            }
            if (Input.GetButtonDown(ContStringA) && panelState < 2) { select(); }
            else if (Input.GetButtonDown(ContStringB)) { deselect(); }
            _LastX = x;
            _LastY = y;
        }
    }

    // Cycle between models/colors
    void move_right()
    {
        timerH = Time.time;
        moveH = false;
        if (currentRow == 0)
        {
            jumpAnim(arrows[1], 215f, arrows[1].transform.localPosition.x);
            models[currentChar].SetActive(false);
            icons[currentChar].transform.DOScale(new Vector3(11f, 11f, 1f), 0.1f);
            abilityNames[currentChar].SetActive(false);

            currentChar++;
            if (currentChar > 3) { currentChar = 0; }

            models[currentChar].SetActive(true);
            abilityNames[currentChar].SetActive(true);
            icons[currentChar].transform.DOScale(new Vector3(18f, 18f, 1f), 0.1f);
        }

        else if (currentRow == 1)
        {
            jumpAnim(arrows[3], 95f, arrows[3].transform.localPosition.x);
            //pulseAnim(charColor, 1.2f, 1f);
            teamIcons[currentColor].SetActive(false);
            currentColor++;
            if (currentColor > 1) { currentColor = 0; }
            //charColor.GetComponent<Image>().color = colors[currentColor];
            teamIcons[currentColor].SetActive(true);
            update_colors();
        }
        arrowInst.start();
    }

    void move_left()
    {
        timerH = Time.time;
        moveH = false;
        if (currentRow == 0)
        {
            jumpAnim(arrows[0], -215f, arrows[0].transform.localPosition.x);
            models[currentChar].SetActive(false);
            icons[currentChar].transform.DOScale(new Vector3(13f, 13f, 1f), 0.1f);
            abilityNames[currentChar].SetActive(false);

            currentChar--;
            if (currentChar < 0) { currentChar = 3; }

            models[currentChar].SetActive(true);
            abilityNames[currentChar].SetActive(true);
            icons[currentChar].transform.DOScale(new Vector3(17f, 17f, 1f), 0.1f);
        }
        else if (currentRow == 1)
        {
            jumpAnim(arrows[2], -95f, arrows[2].transform.localPosition.x);
            //pulseAnim(charColor, 1.2f, 1f);
            teamIcons[currentColor].SetActive(false);
            currentColor--;
            if (currentColor < 0) { currentColor = 1; }
            //charColor.GetComponent<Image>().color = colors[currentColor];
            teamIcons[currentColor].SetActive(true);
            update_colors();
        }

        arrowInst.start();

    }

    void update_colors()
    {
        for (int i = 0; i < 4; i++)
        {
            //models[i].transform.Find("Model").GetComponent<Renderer>().material.SetColor("_Color", colors[currentColor]);
            models[i].transform.Find("Model").GetComponent<Renderer>().material = materials[currentColor];
        }

    }

    void row_down()
    {
        timerV = Time.time;
        moveV = false;
        currentRow++;
        if (currentRow > 1) { currentRow = 0; }
        move_box();

        arrowInst.start();
    }

    void row_up()
    {
        timerV = Time.time;
        moveV = false;
        currentRow--;
        if (currentRow < 0) { currentRow = 1; }
        move_box();

        arrowInst.start();
    }

    void move_box()
    {
        if (currentRow == 0) { box.transform.localPosition = new Vector3(0f, -140f, 0f); }
        else if (currentRow == 1) { box.transform.localPosition = new Vector3(0f, -223, 0f); }
    }

    void select()
    {
        if (panelState == 0)
        {
            models[currentChar].GetComponent<Animator>().SetInteger("Current State", 0);
            startAInst.start();
        }
        else if(panelState == 1)
        {
            animation_advance();
            confirmInst.start();
        }

        if (panelState < 2) { panelState++; }
        panelManager();
    }

    void deselect()
    {
        if (panelState > 0) {
            panelState--;
            animation_return();
        }
        panelManager();

        backBInst.start();
    }
    void clearAll()
    {
        bg.enabled = false;
        text.SetActive(false);
        button.enabled = false;

        foreach (GameObject x in icons) { x.GetComponent<SpriteRenderer>().enabled = false; }
        foreach (GameObject x in arrows) { x.GetComponent<SpriteRenderer>().enabled = false; }
        foreach (GameObject x in abilityNames) { x.SetActive(false); }
        foreach (GameObject x in teamIcons) { x.SetActive(false); }
        promptPanel.SetActive(false);
        //charColor.SetActive(false);
        box.SetActive(false);
    }
    void activateSelect()
    {
        foreach (GameObject x in icons) { x.GetComponent<SpriteRenderer>().enabled = true; }
        foreach (GameObject x in arrows) { x.GetComponent<SpriteRenderer>().enabled = true; }
        models[currentChar].SetActive(true);
        abilityNames[currentChar].SetActive(true);
        //charColor.SetActive(true);
        teamIcons[currentColor].SetActive(true);
        box.SetActive(true);
    }

    void panelManager()
    {
        clearAll();

        if (panelState == 0)
        {
            bg.enabled = true;
            text.SetActive(true);
            models[currentChar].SetActive(false);
            button.enabled = true;
        }
        else if (panelState == 1) {
            activateSelect();
            gamePlayManager.Ready(panelNum, false, currentColor + 1, ControllerNum, currentChar+1);
            
        }
        else if (panelState == 2)
        {
            promptPanel.SetActive(true);
            promptPanel.transform.localScale = new Vector3(0, 0, 0);
            //sends Panel, ready state, team num and Controler Num to GamePlayManager
            gamePlayManager.Ready(panelNum, true, currentColor + 1, ControllerNum, currentChar+1);
            DOTween.Sequence()
            .Append(promptPanel.transform.DOScale(1f, .1f));
        }
    }
    public void ContNumPass(int Num, int pan)
    {
        Debug.Log(panelNum + "panmn" + pan);
        if (pan == panelNum)
        {
            ControllerNum = Num;
            Debug.Log(Num +"pan"+ pan);
        }
    }

    // Animation Manager
    public void animation_advance() { models[currentChar].GetComponent<Animator>().SetInteger("Current State", models[currentChar].GetComponent<Animator>().GetInteger("Current State") + 1); }
    public void animation_return() { models[currentChar].GetComponent<Animator>().SetInteger("Current State", models[currentChar].GetComponent<Animator>().GetInteger("Current State") - 1); }

    void jumpAnim(GameObject a, float targetPos, float returnPos)
    {
        DOTween.Sequence()
        .Append(a.transform.DOLocalMoveX(targetPos, 0.1f))
        .Append(a.transform.DOLocalMoveX(returnPos, 0.1f));
    }

    void pulseAnim(GameObject a, float targetScale, float returnScale)
    {
        DOTween.Sequence()
        .Append(a.transform.DOScale(targetScale, 0.1f))
        .Append(a.transform.DOScale(returnScale, 0.1f));
    }
}
// Update is called once per frame

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && panelState == 1) { move_left(); }
        else if (Input.GetKeyDown(KeyCode.D) && panelState == 1) { move_right(); }

        if (Input.GetKeyDown(KeyCode.W) && panelState == 1) { row_up(); }
        else if (Input.GetKeyDown(KeyCode.S) && panelState == 1) { row_down(); }

        if (Input.GetKeyDown(KeyCode.Q)) { select(); }
        else if (Input.GetKeyDown(KeyCode.E)) { deselect(); }
    }
    */
