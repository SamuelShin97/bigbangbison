using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    
    public float forceFactro = 200f;
    public int PlayerNum;
    public bool PushActivate = false;
    private string push = "Ability";
    List<Rigidbody> rgbBison = new List<Rigidbody>();
    Transform Target;
    void Start()
    {
        if (PushActivate)
        {
            push = push + PlayerNum;
            Target = GetComponent<Transform>();
        }
    }
    
    void FixedUpdate()
    {
        if (PushActivate)
        {
            if (Input.GetButton(push))
            {
                foreach (Rigidbody rgbBis in rgbBison)
                {
                    rgbBis.AddForce((rgbBis.position - Target.position) * forceFactro * Time.fixedDeltaTime);
                }
            }
        }
    }
    void OnTriggerEnter(Collider pushGroup)
    {
        if (pushGroup.CompareTag("Bison")) rgbBison.Add(pushGroup.GetComponent<Rigidbody>());
    }
    void OnTriggerExit(Collider pushGroup)
    {
        if (pushGroup.CompareTag("Bison")) rgbBison.Remove(pushGroup.GetComponent<Rigidbody>());
    }

}
