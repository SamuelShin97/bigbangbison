using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineAbilities : OnlinePlayerProperties
{
    //this script is the same for the push, pull and wall abilities
    //slider
    public Slider slider;


    //public float pushForce = 200f;

    public float positioningSpeed;
    public float maxRange;
    public float minRange;


    //public bool PushActivate = true;
    public Transform spawnPoint;
    public Transform powerLocation;
    public GameObject pushObj;
    public GameObject pullObj;
    public GameObject wallObj;
    public float coolDownDuration = 5;


    private string abilityInput = "Ability";
    private string mouseY = "MouseY";
    private float speedCount = 2f;
    private float coolDowns = 0;
    private GameObject abilityObj;

    void Start()
    {
        playerNum = GetComponent<OnlinePlayerProperties>().playerNum;
        character = GetComponent<OnlinePlayerProperties>().character;
        abilityInput = abilityInput + playerNum;
        mouseY = mouseY + playerNum;
        spawnPoint.gameObject.SetActive(false);
        abilityObj = DetermineAbilityObj();
        SetAbilityColor(abilityObj);
    }

    void Update()
    {
        if (coolDowns <= 0)
        {
            if (Input.GetButton(abilityInput))
            {
                spawnPoint.gameObject.SetActive(true);
                PositionPowerLocation();
            }
            if (Input.GetButtonUp(abilityInput))
            {
                Instantiate(abilityObj, spawnPoint.position, spawnPoint.rotation);
                spawnPoint.gameObject.SetActive(false);
                coolDowns = coolDownDuration;
            }
        }
        else if (coolDowns > 0)
        {
            coolDowns -= Time.deltaTime;
            spawnPoint.gameObject.SetActive(false);
            slider.value = calSliderVal();
        }
    }

    void PositionPowerLocation()
    {
        float translationZ = -(Input.GetAxis(mouseY)); //for some reason this is inverse
        float distanceFromPlayer = powerLocation.localPosition.z;

        if (distanceFromPlayer < maxRange && distanceFromPlayer > minRange)
        {
            Vector3 crossHairControlSpeed = new Vector3(0f, 0f, translationZ) * positioningSpeed * Time.deltaTime;
            powerLocation.Translate(crossHairControlSpeed);
        }
        else if (distanceFromPlayer >= maxRange && translationZ < 0)
        {
            Vector3 crossHairControlSpeed = new Vector3(0f, 0f, translationZ) * positioningSpeed * Time.deltaTime;
            powerLocation.Translate(crossHairControlSpeed);
        }
        else if (distanceFromPlayer <= minRange && translationZ > 0)
        {
            Vector3 crossHairControlSpeed = new Vector3(0f, 0f, translationZ) * positioningSpeed * Time.deltaTime;
            powerLocation.Translate(crossHairControlSpeed);
        }
    }

    float calSliderVal()
    {
        return (coolDowns / coolDownDuration);
    }

    GameObject DetermineAbilityObj()
    {
        if (character == 2)
        {
            return pushObj;
        }
        else if (character == 1)
        {
            return pullObj;
        }
        else if (character == 3)
        {
            return wallObj;
        }
        
        return null;
    }

    void SetAbilityColor(GameObject abilityObj)
    {
        if (GetComponent<OnlinePlayerProperties>().teamColor == 1)
        {
            ParticleSystem[] ps_arr = abilityObj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps_arr.Length; i++)
            {
                ParticleSystem.MainModule m_ps = ps_arr[i].main;
                m_ps.startColor = GetComponent<OnlinePlayerProperties>().blue.color;
            }

            
        }
        else if (GetComponent<OnlinePlayerProperties>().teamColor == 2)
        {
            ParticleSystem[] ps_arr = abilityObj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps_arr.Length; i++)
            {
                ParticleSystem.MainModule m_ps = ps_arr[i].main;
                m_ps.startColor = GetComponent<OnlinePlayerProperties>().red.color;
            }
        }
    }

}
