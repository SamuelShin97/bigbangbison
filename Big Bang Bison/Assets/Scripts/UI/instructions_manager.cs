using UnityEngine;
using UnityEngine.UI;

public class instructions_manager : MonoBehaviour
{
    public Image instructionPage;

    public Button back;
    public GameObject[] allPages= new GameObject[4];
    public Image[] pages = new Image[4];
    private int x = 0;
    private bool moveable = true;
    private float timer;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(instructionPage.enabled == true && Input.GetAxis("Horizontal1") >= 0.2 && moveable == true) { right(); }
        else if (instructionPage.enabled == true && Input.GetAxis("Horizontal1") <= -0.2 && moveable == true) { left(); }
        else if (instructionPage.enabled == true && Input.GetButtonDown("B1")) { back.onClick.Invoke(); }
        if (Time.time > timer + 0.1f) { moveable = true; }
    }

    public void restart()
    {
        for(int i = 0; i < allPages.Length; i++){ allPages[i].SetActive(false); }
        allPages[0].SetActive(true);
    }
    //navigate images
    void right()
    {
        allPages[x].SetActive(false);
        x++;
        if (x >= allPages.Length) { x = 0; }
        allPages[x].SetActive(true);
        moveable = false;
        timer = Time.time;
    }

    void left()
    {
        allPages[x].SetActive(false);
        x--;
        if (x < 0) { x = allPages.Length - 1; }
        allPages[x].SetActive(true);
        moveable = false;
        timer = Time.time;
    }
/*
    void right()
    {
        pages[x].enabled = false;
        x++;
        if(x >= allPages.Length) { x = 0; }
        pages[x].enabled = true;
        moveable = false;
        timer = Time.time;
        Debug.Log(timer);
    }

    void left()
    {
        pages[x].enabled = false;
        x--;
        if (x < 0) { x = allPages.Length-1; }
        pages[x].enabled = true;
        moveable = false;
        timer = Time.time;
    }
    */
}
