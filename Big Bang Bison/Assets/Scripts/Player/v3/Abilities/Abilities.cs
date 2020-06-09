using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : PlayerProperties
{
    //this script is the same for the push, pull and wall abilities
    //slider
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    private Slider use;
    private Slider holder;

    Vector3 a;

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
    //public bool startCooldown;


    private string abilityInput = "RTrigger";
    private string mouseY = "MouseY";
    private float speedCount = 2f;
    private float coolDowns = 0;
    private int abilityNum;
    private GameObject abilityObj;
    private bool spawned;
    private GameObject activeObj;

    void Start()
    {
        spawned = false;
        playerNum = GetComponent<PlayerProperties>().playerNum;
        character = GetComponent<PlayerProperties>().character;
        abilityInput = abilityInput + playerNum;
        mouseY = mouseY + playerNum;
        spawnPoint.gameObject.SetActive(false);
        abilityObj = DetermineAbilityObj();
        
        SetAbilityColor(abilityObj);
        //sets up cool down visable slider
        GameObject p = GameObject.Find("/UI Gameplay 02");
        if (character == 2) 
        {
            use = slider2;
            
        }
        else if (character == 1) //pull
        {
            use = slider1;
        }
        else if (character == 3) //wall
        {
            use = slider3;
        }

        if (playerNum == 1)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P1 Game UI/Location1");
            holder = Instantiate(use, location.transform.position, Quaternion.identity, p.transform);
        }
        else if (playerNum == 2)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P2 Game UI/Location2");
            holder = Instantiate(use, location.transform.position, Quaternion.identity, p.transform);
        }
        else if (playerNum == 3)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P3 Game UI/Location3");
            holder = Instantiate(use, location.transform.position, Quaternion.identity, p.transform);
        }
        else if (playerNum == 4)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P4 Game UI/Location4");
            holder = Instantiate(use, location.transform.position, Quaternion.identity, p.transform);
        }
        

    }

    void Update()
    {
        if (coolDowns <= 0)
        {
            if (Input.GetAxis(abilityInput) > 0.4)
            {
                spawnPoint.gameObject.SetActive(true);
                PositionPowerLocation();
            }
            else if (spawnPoint.gameObject.activeSelf && Input.GetAxis(abilityInput) < 0.3)
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
            holder.value = 1 - calSliderVal();
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
        if (character == 2) //push
        {
            return pushObj;
        }
        else if (character == 1) //pull
        {
            return pullObj;
        }
        else if (character == 3) //wall
        {
            return wallObj;
        }
        
        return null;
    }
    

    void SetAbilityColor(GameObject abilityObj)
    {
        if (GetComponent<PlayerProperties>().teamColor == 1)
        {
            ParticleSystem[] ps_arr = abilityObj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps_arr.Length; i++)
            {
                ParticleSystem.MainModule m_ps = ps_arr[i].main;
                m_ps.startColor = GetComponent<PlayerProperties>().blue.color;
            }

            
        }
        else if (GetComponent<PlayerProperties>().teamColor == 2)
        {
            ParticleSystem[] ps_arr = abilityObj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps_arr.Length; i++)
            {
                ParticleSystem.MainModule m_ps = ps_arr[i].main;
                m_ps.startColor = GetComponent<PlayerProperties>().red.color;
            }
        }
    }

}
