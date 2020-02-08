using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceFactro = 200f;
    public int PlayerNum;
    public int PushActivate;
    private string push = "Ability";
    List<Rigidbody> rgbBison = new List<Rigidbody>();
    Transform Target;
    void Start()
    {
        if (PushActivate == 1)
        {
            push = push + PlayerNum;
            Target = GetComponent<Transform>();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PushActivate == 1)
        {
            if (Input.GetButton(push))
            {
                foreach (Rigidbody rgbBis in rgbBison)
                {
                    rgbBis.AddForce((Target.position + rgbBis.position) * forceFactro * Time.fixedDeltaTime);
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
