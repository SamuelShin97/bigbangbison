using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    
    public float forceFactro = 200f;

    public int PlayerNum;

    public bool PushActivate = false;
    public Transform SpawnPoint;
    public GameObject PushObj;

    private string push = "Ability";
    

    void Start()
    {
        if (PushActivate)
        {
            push = push + PlayerNum;
            
        }
    }
    
    void FixedUpdate()
    {
        if (PushActivate)
        {
            if (Input.GetButton(push))
            {

            }
            if (Input.GetButtonUp(push))
            {
                Instantiate(PushObj, SpawnPoint.position, SpawnPoint.rotation);
            }
        }
    }
}
