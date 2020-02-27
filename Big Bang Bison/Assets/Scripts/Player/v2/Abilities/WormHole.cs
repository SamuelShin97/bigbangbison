using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{
    public Transform powerLocation;
    //public bool showGizmos = false;
    public float AoERadius;
    public float speed;
    public float maxRange;
    public float minRange;
    public GameObject FromTeleporter;
    public GameObject ToTeleporter;
    public bool fromIsDown;
    public bool toIsDown;
    private float speedCount = 2f;
    private float coolDowns;

    // Start is called before the first frame update
    void Start()
    {
        fromIsDown = false;
        toIsDown = false;
        powerLocation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Ability4") && coolDowns <= 0)
        {
            Debug.Log("pushed rb");
            //showGizmos = true;
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
        else if (coolDowns > 0)
        {
            coolDowns -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Ability4"))
        {
            Debug.Log("released rb");
            //showGizmos = false;
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
                toIsDown = true;
                fromIsDown = false;
                toIsDown = false;
            }
            
        }
    }

    /*private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lookAt.position, AoERadius);
        }
    }*/
}
