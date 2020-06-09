using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPull : MonoBehaviour
{
    private float timerSpeed = 2f;
    public float forceFactro = 900f;
    private float elapsed;
    List<Rigidbody> rgbBison = new List<Rigidbody>();
    Transform Target;
    

    private void Start()
    {
        Target = GetComponent<Transform>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
        foreach (Rigidbody rgbBis in rgbBison)
        {
            rgbBis.AddForce((Target.position - rgbBis.position) * forceFactro * Time.fixedDeltaTime);
        }
        elapsed += Time.deltaTime;
        if (elapsed > 5)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider pullGroup)
    {
        if (pullGroup.CompareTag("RedBison") || pullGroup.CompareTag("BlueBison")) rgbBison.Add(pullGroup.GetComponent<Rigidbody>());
    }
    void OnTriggerExit(Collider pullGroup)
    {
        if (pullGroup.CompareTag("RedBison") || pullGroup.CompareTag("BlueBison")) rgbBison.Remove(pullGroup.GetComponent<Rigidbody>());
    }
}
