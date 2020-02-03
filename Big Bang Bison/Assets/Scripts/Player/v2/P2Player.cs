using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 10.0f;
    public float rotationSpeed = 100.0f;


    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
    }
    void PlayerMovment()
    {
        float translationX = Input.GetAxis("Vertical2") * Speed;
        float translationY = Input.GetAxis("Horizontal2") * Speed;

        float rotation = Input.GetAxis("Mouse X2") * rotationSpeed;
        translationX *= Time.deltaTime;
        translationY *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(translationY, 0, translationX);
        transform.Rotate(0, rotation, 0);
    }
}
