using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class canvas_control : MonoBehaviour
{
    public GameObject[] makeActive = new GameObject[0];
    public GameObject prev_panel;
    public GameObject next_panel;
    public GameObject to_highlight;
    public EventSystem button_manager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B1")) { step_back(); }
    }

    void step_back()
    {
        prev_panel.SetActive(true);
        if (to_highlight != null) { button_manager.SetSelectedGameObject(to_highlight); }
        activate_objects();
        this.gameObject.SetActive(false);
    }

    void step_forward()
    {
        next_panel.SetActive(true);
        if (to_highlight != null) { button_manager.SetSelectedGameObject(to_highlight); }
        this.gameObject.SetActive(false);
    }

    void activate_objects()
    {
        if(makeActive.Length > 0) { foreach(GameObject x in makeActive) { x.SetActive(true); } }
    }
}
