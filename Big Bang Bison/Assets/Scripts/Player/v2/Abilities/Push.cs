using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Push : MonoBehaviour
{
    //slider
    public Slider slider;
    

    public float forceFactro = 200f;

    public int PlayerNum;
    public float speed;
    public float maxRange;
    public float minRange;
    

    public bool PushActivate = true;
    public Transform SpawnPoint;
    public GameObject PushObj;

    private string push = "Ability";

    public float coolDownDuration = 5;
    private float speedCount = 2f;
    private float coolDowns = 0;

    void Start()
    {
        if (PushActivate)
        {
            push = push + PlayerNum;
            SpawnPoint.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (PushActivate && coolDowns <= 0)
        {
            if (Input.GetButton(push))
            {
                SpawnPoint.gameObject.SetActive(true);
                PositionPowerLocation();
            }
            if (Input.GetButtonUp(push))
            {
                Instantiate(PushObj, SpawnPoint.position, SpawnPoint.rotation);
                SpawnPoint.gameObject.SetActive(false);
                coolDowns = coolDownDuration;
            }
        }
        else if(coolDowns> 0)
        {
            coolDowns -= Time.deltaTime;
            SpawnPoint.gameObject.SetActive(false);
            slider.value = calSliderVal();
        }
    }

    void PositionPowerLocation()
    {
        float translationZ = -(Input.GetAxis("MouseY1")); //for some reason this is inverse
        float distanceFromPlayer = gameObject.transform.localPosition.z;

        if (distanceFromPlayer < maxRange && distanceFromPlayer > minRange)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            gameObject.transform.Translate(teleportControlSpeed);
        }
        else if (distanceFromPlayer >= maxRange && translationZ < 0)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            gameObject.transform.Translate(teleportControlSpeed);
        }
        else if (distanceFromPlayer <= minRange && translationZ > 0)
        {
            Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
            gameObject.transform.Translate(teleportControlSpeed);
        }
    }
    float calSliderVal()
    {
        return (coolDowns / coolDownDuration);
    }
}
