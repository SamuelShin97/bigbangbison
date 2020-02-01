using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public int PlayerNum;
    //public string Horizontal;
    //public string Vertical;
    //public string MouseX;


    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
    }
    void PlayerMovment()
    {
        

        if (PlayerNum == 1){
            float translationX = Input.GetAxis("Vertical1") * Speed;
            float translationY = Input.GetAxis("Horizontal1") * Speed;

            float rotation = Input.GetAxis("MouseX1") * rotationSpeed;
            translationX *= Time.deltaTime;
            translationY *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(translationY, 0, translationX);
            transform.Rotate(0, rotation, 0);
        }
        else if (PlayerNum == 2)
        {
            float translationX = Input.GetAxis("Vertical2") * Speed;
            float translationY = Input.GetAxis("Horizontal2") * Speed;

            float rotation = Input.GetAxis("MouseX2") * rotationSpeed;
            translationX *= Time.deltaTime;
            translationY *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(translationY, 0, translationX);
            transform.Rotate(0, rotation, 0);
        }
        else if (PlayerNum == 3)
        {
            float translationX = Input.GetAxis("Vertical3") * Speed;
            float translationY = Input.GetAxis("Horizontal3") * Speed;

            float rotation = Input.GetAxis("MouseX3") * rotationSpeed;
            translationX *= Time.deltaTime;
            translationY *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(translationY, 0, translationX);
            transform.Rotate(0, rotation, 0);
        }
        else if (PlayerNum == 4)
        {
            float translationX = Input.GetAxis("Vertical4") * Speed;
            float translationY = Input.GetAxis("Horizontal4") * Speed;

            float rotation = Input.GetAxis("MouseX4") * rotationSpeed;
            translationX *= Time.deltaTime;
            translationY *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(translationY, 0, translationX);
            transform.Rotate(0, rotation, 0);
        }


    }
}
