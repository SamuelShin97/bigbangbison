using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Image biomeBG;

    // Start is called before the first frame update
    void Start()
    {
        biomeBG.GetComponent<Image>().color = new Color(29, 226, 110);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void change_color()
    {

    }
}
