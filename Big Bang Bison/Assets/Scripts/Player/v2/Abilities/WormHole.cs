using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormHole : MonoBehaviour
{
    public Slider slider;
    public int playerNum;
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
    
    public float coolDownDuration = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        fromIsDown = false;
        toIsDown = false;
        powerLocation.gameObject.SetActive(false);
        abilityInput = abilityInput + playerNum;
        
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
        float translationZ = -(Input.GetAxis("MouseY4")); //for some reason this is inverse
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
}
