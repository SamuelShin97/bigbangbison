using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineTeleport : OnlinePlayerProperties
{
    public Slider slider;

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
    private string abilityInput = "Ability";
    private string mouseY = "MouseY";
    public float coolDownDuration = 5;


    // Start is called before the first frame update
    void Start()
    {
        playerNum = GetComponent<OnlinePlayerProperties>().playerNum;
        fromIsDown = false;
        toIsDown = false;
        powerLocation.gameObject.SetActive(false);
        mouseY = mouseY + playerNum;
        abilityInput = abilityInput + playerNum;
        SetAbilityColor(FromTeleporter);
        SetAbilityColor(ToTeleporter);
    }


    // Update is called once per frame
    void Update()
    {
        if (coolDowns <= 0)
        {
            ////fjadkfjalksdjf;lkasdjflkasd;
            if (Input.GetButton(abilityInput) && coolDowns <= 0)
            {
                PositionPowerLocation();
            }
            if (Input.GetButtonUp(abilityInput))
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
                    GameObject from = Instantiate(ToTeleporter, new Vector3(powerLocation.position.x, 2f, powerLocation.position.z), Quaternion.Euler(0f, 0f, 0f));
                    from.transform.localScale = new Vector3(AoERadius * 2, 0.1f, AoERadius * 2);
                    coolDowns = coolDownDuration;
                    toIsDown = true;
                    fromIsDown = false;
                    toIsDown = false;
                }


            }
        }
        else if (coolDowns > 0)
        {
            coolDowns -= Time.deltaTime;
            powerLocation.gameObject.SetActive(false);
            slider.value = calSliderVal();
        }
    }

    void PositionPowerLocation()
    {
        powerLocation.gameObject.SetActive(true);
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
        if (GetComponent<OnlinePlayerProperties>().teamColor == 1)
        {
            ParticleSystem[] ps_arr = abilityObj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps_arr.Length; i++)
            {
                ParticleSystem.MainModule m_ps = ps_arr[i].main;
                m_ps.startColor = GetComponent<PlayerProperties>().blue.color;
            }


        }
        else if (GetComponent<OnlinePlayerProperties>().teamColor == 2)
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
