using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : PlayerProperties
{
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    private Slider holder;
    Vector3 a;
    public Transform powerLocation;
    //public bool showGizmos = false;
    public float AoERadius;
    public float speed;
    public float maxRange;
    public float minRange;
    public GameObject FromTeleporter;
    public GameObject ToTeleporter;

    //public bool TelActivate = false;
    private bool fromIsDown;
    private bool toIsDown;
    private float speedCount = 2f;
    private float coolDowns = 0;
    private string abilityInput = "RTrigger";
    private string mouseY = "MouseY";
    public float coolDownDuration = 5;
    private float elapsed;
    private bool validPlacement;


    // Start is called before the first frame update
    void Start()
    {
        playerNum = GetComponent<PlayerProperties>().playerNum;
        fromIsDown = false;
        toIsDown = false;
        powerLocation.gameObject.SetActive(false);
        mouseY = mouseY + playerNum;
        abilityInput = abilityInput + playerNum;
        SetAbilityColor(FromTeleporter);
        SetAbilityColor(ToTeleporter);
        validPlacement = false;
        //sets up cooldowns
        GameObject p = GameObject.Find("/UI Gameplay 02");
        if (playerNum == 1)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P1 Game UI/Location1");
            holder = Instantiate(slider1, location.transform.position, Quaternion.identity, p.transform);
        }
        if (playerNum == 2)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P2 Game UI/Location2");
            holder = Instantiate(slider1, location.transform.position, Quaternion.identity, p.transform);
        }
        if (playerNum == 3)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P3 Game UI/Location3");
            holder = Instantiate(slider1, location.transform.position, Quaternion.identity, p.transform);
        }
        if (playerNum == 4)
        {
            GameObject location = GameObject.Find("UI Gameplay 02/P4 Game UI/Location4");
            holder = Instantiate(slider1, location.transform.position, Quaternion.identity, p.transform);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (fromIsDown)
        {
            elapsed += Time.deltaTime;
        }
        if (coolDowns <= 0)
        {
            /*
            powerLocation.gameObject.activeSelf
             if (Input.GetAxis(abilityInput) > 0.4)
            {
                    PositionPowerLocation();
            }
            else if (powerLocation.gameObject.activeSelf && Input.GetAxis(abilityInput) < 0.3)
            {
            */


            ////fjadkfjalksdjf;lkasdjflkasd; ???
            if (Input.GetAxis(abilityInput) > 0.4)
            {
                powerLocation.gameObject.SetActive(true);
                RaycastHit ground;
                if (Physics.Raycast(powerLocation.gameObject.transform.position, Vector3.down, out ground, 10f))
                {
                    Debug.Log("on ground");
                    validPlacement = true;
                }
                else
                {
                    Debug.Log("not on ground");
                    validPlacement = false;
                }

                if (fromIsDown)
                {
                    if (validPlacement)
                    {
                        //GRANT HERE
                        //change color to normal color
                    }
                    else
                    {
                        //change to opaque or invalid color
                    }
                }

                PositionPowerLocation();
            }
            else if (powerLocation.gameObject.activeSelf && Input.GetAxis(abilityInput) < 0.3)
            {
                powerLocation.gameObject.SetActive(false);
                if (!fromIsDown)
                {
                    GameObject from = Instantiate(FromTeleporter, new Vector3(powerLocation.position.x, 2f, powerLocation.position.z), Quaternion.Euler(0f, 0f, 0f));
                    from.transform.localScale = new Vector3(AoERadius * 2, 0.1f, AoERadius * 2);
                    fromIsDown = true;
                }
                else if (!toIsDown)
                {
                    if (validPlacement)
                    {
                        GameObject from = Instantiate(ToTeleporter, new Vector3(powerLocation.position.x, 2f, powerLocation.position.z), Quaternion.Euler(0f, 0f, 0f));
                        from.transform.localScale = new Vector3(AoERadius * 2, 0.1f, AoERadius * 2);
                        coolDowns = coolDownDuration;
                        toIsDown = true;
                        fromIsDown = false;
                        toIsDown = false;
                        elapsed = 0;
                    }
                    else
                    {
                        GameObject from = GameObject.FindWithTag("From");
                        if (from != null)
                        {
                            Destroy(from);
                        }
                        coolDowns = coolDownDuration;
                        toIsDown = true;
                        fromIsDown = false;
                        toIsDown = false;
                        elapsed = 0;
                    }
                    
                }


            }
        }
        else if (coolDowns > 0)
        {
            coolDowns -= Time.deltaTime;
            powerLocation.gameObject.SetActive(false);
            holder.value = calSliderVal();
        }
        if (elapsed > 3) //disappearing time between from and to
        {
            GameObject from = GameObject.FindWithTag("From");
            if (from != null)
            {
                Destroy(from);
                fromIsDown = false;
                elapsed = 0;
            }
            
        }
    }

    void PositionPowerLocation()
    {
        float translationZ = -(Input.GetAxis(mouseY)); //for some reason this is inverse
        float distanceFromPlayer = powerLocation.localPosition.z;

        if (distanceFromPlayer < maxRange && distanceFromPlayer > minRange)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            powerLocation.transform.Translate(teleportControlSpeed);
        }
        else if (distanceFromPlayer >= maxRange && translationZ < 0)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            powerLocation.transform.Translate(teleportControlSpeed);
        }
        else if (distanceFromPlayer <= minRange && translationZ > 0)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            powerLocation.transform.Translate(teleportControlSpeed);
        }
    }

    float calSliderVal()
    {
        return (coolDowns / coolDownDuration);
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
