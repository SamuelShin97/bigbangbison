using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsInformation : MonoBehaviour
{
    public bool BisonNamesEnabled;
    GameObject instance;
    public Button OnButton;
    public Button OffButton;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = GameObject.Find("SettingsInformation");

        if (instance != null && instance != gameObject)
        {
            Debug.Log("destroying settingsinfo");
            Destroy(gameObject);
        }
        BisonNamesEnabled = false;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableBisonNames()
    {
        BisonNamesEnabled = true;
    }

    public void DisableBisonNames()
    {
        BisonNamesEnabled = false;
    }

    public void setOnColor()
    {
        ColorBlock color = OnButton.colors;
        color.normalColor = Color.cyan;
        OnButton.colors = color;

        ColorBlock color2 = OffButton.colors;
        color2.normalColor = Color.grey;
        OffButton.colors = color2;
    }

    public void setOffColor()
    {
        ColorBlock color = OffButton.colors;
        color.normalColor = Color.cyan;
        OffButton.colors = color;

        ColorBlock color2 = OnButton.colors;
        color2.normalColor = Color.grey;
        OnButton.colors = color2;
    }
}
