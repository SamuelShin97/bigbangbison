
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBisonOnHill : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RedBison") || other.CompareTag("BlueBison"))
        {
            other.gameObject.GetComponent<HerdAgent>().growth += Time.deltaTime * growthSpeed;

            //Debug.Log(other.gameObject.GetComponent<HerdAgent>().growth);
        }
    }
}
