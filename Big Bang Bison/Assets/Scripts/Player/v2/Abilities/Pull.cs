using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public int PlayerNum;
    public float speed;
    public float maxRange;
    public float minRange;

    public bool PullActivate = false;
    public Transform SpawnPoint;
    public GameObject PullObj;
    
    private string pull = "Ability";
    public float coolDownDuration = 5;
    private float speedCount = 2f;
    private float coolDowns = 0;

    void Start()
    {
        if (PullActivate)
        {
            pull = pull + PlayerNum;
            SpawnPoint.gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (PullActivate && coolDowns <= 0)
        {
            if (Input.GetButton(pull))
            {
                SpawnPoint.gameObject.SetActive(true);
                PositionPowerLocation();
            }
            if (Input.GetButtonUp(pull))
            {
                Instantiate(PullObj, SpawnPoint.position, SpawnPoint.rotation);
                SpawnPoint.gameObject.SetActive(false);
                coolDowns = coolDownDuration;
            }
        }
        else if (coolDowns > 0)
        {
            coolDowns -= Time.deltaTime;
            SpawnPoint.gameObject.SetActive(false);
        }
    }

    void PositionPowerLocation()
    {
        float translationZ = -(Input.GetAxis("MouseY2")); //for some reason this is inverse
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
}
