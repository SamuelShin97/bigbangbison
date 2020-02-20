using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public int PlayerNum;

    public bool PullActivate = false;
    public Transform SpawnPoint;
    public GameObject PullObj;
    
    private string pull = "Ability";
    

    void Start()
    {
        if (PullActivate)
        {
            pull = pull + PlayerNum;
            
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PullActivate)
        {
            if (Input.GetButton(pull))
            {

            }
            if (Input.GetButtonUp(pull))
            {
                Instantiate(PullObj, SpawnPoint.position, SpawnPoint.rotation);
            }
        }
    }
}
