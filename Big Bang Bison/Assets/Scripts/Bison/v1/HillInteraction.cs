using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillInteraction : MonoBehaviour
{
    public int mediumSize;
    public int largeSize;
    private bool insideHill = false;
    private bool isMedium = false;
    private bool isLarge = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hill"))
        {
            //Debug.Log("inside hill");
            insideHill = true;
            yield return new WaitForSeconds(2);
            
            if (insideHill)
            {
                //Debug.Log("inside hill still");
                
                if (!isMedium && !isLarge)
                {
                    //Debug.Log("get medium");
                    transform.localScale = new Vector3(mediumSize, mediumSize, mediumSize);
                    isMedium = true;
                }
            }
            yield return new WaitForSeconds(2);
            if (insideHill && isMedium && !isLarge)
            {
                //Debug.Log("get large");
                transform.localScale = new Vector3(largeSize, largeSize, largeSize);
                isLarge = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hill"))
        {
            //Debug.Log("leaving hill");
            insideHill = false;
        }
    }
}
