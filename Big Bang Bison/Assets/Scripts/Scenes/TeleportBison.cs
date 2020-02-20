using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBison : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ToTeleportObject to = FindObjectOfType<ToTeleportObject>();
        if (to != null)
        {
            InitiateTeleport(to);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedBison") || other.CompareTag("BlueBison"))
        {
            colliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    void InitiateTeleport(ToTeleportObject toTeleportLoc)
    {
        foreach (Collider c in colliders)
        {
            c.gameObject.transform.position = toTeleportLoc.gameObject.transform.position;
        }
        Destroy(toTeleportLoc.gameObject);
        Destroy(this.gameObject);
    }
}
