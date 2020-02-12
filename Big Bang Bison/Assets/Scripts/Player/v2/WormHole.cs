using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{
    public Transform lookAt;
    public bool showGizmos = false;
    public float AoERadius;
    public float speed;
    public float maxRange;
    public float minRange;
    public GameObject FromTeleporter;
    public GameObject ToTeleporter;
    public bool fromIsDown;
    public bool toIsDown;

    // Start is called before the first frame update
    void Start()
    {
        fromIsDown = false;
        toIsDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Push1"))
        {
            Debug.Log("pushed rb");
            showGizmos = true;
            float translationZ = -(Input.GetAxis("MouseY1")); //for some reason this is inverse
            float distanceFromPlayer = lookAt.localPosition.z;

            if (distanceFromPlayer < maxRange && distanceFromPlayer > minRange)
            {
                Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
                lookAt.transform.Translate(teleportControlSpeed);
            }
            else if (distanceFromPlayer >= maxRange && translationZ < 0)
            {
                Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
                lookAt.transform.Translate(teleportControlSpeed);
            }
            else if (distanceFromPlayer <= minRange && translationZ > 0)
            {
                Vector3 teleportControlSpeed = new Vector3(0f, 0f, translationZ) * speed * Time.deltaTime;
                lookAt.transform.Translate(teleportControlSpeed);
            }
        }

        if (Input.GetButtonUp("Push1"))
        {
            Debug.Log("released rb");
            showGizmos = false;
            if (!fromIsDown)
            {
                GameObject from = Instantiate(FromTeleporter, new Vector3(lookAt.position.x, 2f, lookAt.position.z), Quaternion.Euler(0f, 0f, 0f));
                from.transform.localScale = new Vector3(AoERadius * 2, 0.1f, AoERadius * 2);
                fromIsDown = true;
            }
            else if (!toIsDown)
            {
                GameObject from = Instantiate(ToTeleporter, new Vector3(lookAt.position.x, 2f, lookAt.position.z), Quaternion.Euler(0f, 0f, 0f));
                from.transform.localScale = new Vector3(AoERadius * 2, 0.1f, AoERadius * 2);
                toIsDown = true;
                fromIsDown = false;
                toIsDown = false;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lookAt.position, AoERadius);
        }
    }
}
