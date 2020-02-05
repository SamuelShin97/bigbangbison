using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilitys : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lookAt;
    public Transform camTransform;
    public float pushAmount;
    public float pushRadius;
    public int PlayerNum;
    //public float distance = 10.0f;
    public bool showGizmos = true;

    // Update is called once per frame
    void Update()
    {
        if (PlayerNum == 1)
        {
            if (Input.GetButton("Push1"))
            {
                Push();
                print(lookAt.position);
            }
        }
        if (PlayerNum == 2)
        {
            if (Input.GetButton("Push2"))
            {
                Push();
                print(lookAt.position);
            }
        }
        if (PlayerNum == 3)
        {
            if (Input.GetButton("Push3"))
            {
                Push();
                print(lookAt.position);
            }
        }
        if (PlayerNum == 4)
        {
            if (Input.GetButton("Push4"))
            {
                Push();
                print(lookAt.position);
            }
        }
    }

    private void Push()
    {

        Collider[] colliders = Physics.OverlapSphere(lookAt.position, pushRadius);

        foreach(Collider pushBison in colliders)
        {
            if (pushBison.CompareTag("Bison"))
            {
                Rigidbody pushBody = pushBison.GetComponent<Rigidbody>();
                pushBody.AddExplosionForce(pushAmount, lookAt.position, pushRadius);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lookAt.position, pushRadius);
        }
    }
}
