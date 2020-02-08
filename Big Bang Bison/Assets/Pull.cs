using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    
    public float forceFactro = 200f;
    public int PlayerNum;
    public int PullActivate;
    private string pull = "Ability";
    List<Rigidbody> rgbBison = new List<Rigidbody>();
    Transform Target;
    // Start is called before the first frame update
    //public float distance = 10.0f;
    //public bool showGizmos = true;
    void Start()
    {
        if (PullActivate == 1)
        {
            pull = pull + PlayerNum;
            Target = GetComponent<Transform>();
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PullActivate == 1)
        {
            if (Input.GetButton(pull))
            {
                foreach (Rigidbody rgbBis in rgbBison)
                {
                    rgbBis.AddForce((Target.position - rgbBis.position) * forceFactro * Time.fixedDeltaTime);
                }
            }
        }
    }
    

    void OnTriggerEnter(Collider pullGroup)
    {
        if (pullGroup.CompareTag("Bison")) rgbBison.Add(pullGroup.GetComponent<Rigidbody>());
    }
    void OnTriggerExit(Collider pullGroup)
    {
        if (pullGroup.CompareTag("Bison")) rgbBison.Remove(pullGroup.GetComponent<Rigidbody>());
    }
}
