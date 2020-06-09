using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineGrowBisonOnHill : NetworkBehaviour
{
    public float growthSpeed;
    public List<HerdAgent> onHill;

    // Start is called before the first frame update
    void Start()
    {
        onHill = new List<HerdAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RedBison") || other.CompareTag("BlueBison"))
        {
            other.gameObject.GetComponent<HerdAgent>().growth += Time.deltaTime * growthSpeed;

            //Debug.Log(other.gameObject.GetComponent<HerdAgent>().growth);
        }
    }
}
