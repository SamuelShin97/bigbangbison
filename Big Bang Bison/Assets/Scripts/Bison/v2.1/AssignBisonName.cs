using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignBisonName : MonoBehaviour
{
    TextMesh textMesh;
    GameObject SettingsInformation;
    
    // Start is called before the first frame update
    void Start()
    {
        SettingsInformation = GameObject.Find("SettingsInformation");
        if (SettingsInformation.GetComponent<SettingsInformation>().BisonNamesEnabled)
        {
            textMesh = GetComponent<TextMesh>();
            textMesh.text = gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
