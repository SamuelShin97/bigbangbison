using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPush : MonoBehaviour
{
    private float timerSpeed = 2f;
    public float forceFactro;
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
            rgbBis.AddForce((rgbBis.position - Target.position) * forceFactro);
        }
        elapsed += Time.deltaTime;
        if (elapsed > 2)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider pushGroup)
    {
        if (pushGroup.CompareTag("RedBison") || pushGroup.CompareTag("BlueBison")) rgbBison.Add(pushGroup.GetComponent<Rigidbody>());
    }
    void OnTriggerExit(Collider pushGroup)
    {
        if (pushGroup.CompareTag("RedBison") || pushGroup.CompareTag("BlueBison")) rgbBison.Remove(pushGroup.GetComponent<Rigidbody>());
    }
}
