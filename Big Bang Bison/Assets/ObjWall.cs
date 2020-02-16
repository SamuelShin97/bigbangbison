using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjWall : MonoBehaviour
{
    private float timerSpeed = 2f;
    private float speed = 2f;
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
        if (elapsed < 2.2)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        elapsed += Time.deltaTime;
        if (elapsed > 6)
        {
            Destroy(this.gameObject);
        }
    }
}
