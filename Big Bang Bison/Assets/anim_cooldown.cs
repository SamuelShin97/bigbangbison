using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_cooldown : MonoBehaviour
{
    private bool coolingDown = false;
    private float timer;
    private float[] cooldownTimes = new float[4];
    public int ability;

    [SerializeField]
    Image[] cooldowns = new Image[4];

    [SerializeField]
    Image[] backgrounds = new Image[4];

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Image x in cooldowns) { x.enabled = false; }
        foreach (Image x in backgrounds) { x.enabled = false; }
        cooldownTimes[0] = 3.0f;
        cooldownTimes[1] = 3.0f;
        cooldownTimes[2] = 3.0f;
        cooldownTimes[3] = 5.0f;
        setup_icon(ability);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) { activate_ability(ability); }

        if (coolingDown) { cooldowns[ability].fillAmount = (Time.time - timer) / cooldownTimes[ability]; }
    }

    void activate_ability(int a)
    {
        cooldowns[0].fillAmount = 0f;
        coolingDown = true;
        timer = Time.time;
    }

    void setup_icon(int a)
    {
        cooldowns[a].enabled = true;
        backgrounds[a].enabled = true;
    }
}
