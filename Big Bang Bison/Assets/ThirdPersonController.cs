using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float Speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public int PlayerNum;

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
    }
    void PlayerMovment()
    {

        if (PlayerNum == 1){
            float translationX = Input.GetAxis("Vertical1");
            float translationY = Input.GetAxis("Horizontal1");
            Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * Speed *Time.deltaTime;
            transform.Translate(playerMovment, Space.Self);
        }
        else if (PlayerNum == 2)
        {
            float translationX = Input.GetAxis("Vertical2");
            float translationY = Input.GetAxis("Horizontal2");
            Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * Speed * Time.deltaTime;
            transform.Translate(playerMovment, Space.Self);
        }
        else if (PlayerNum == 3)
        {
            float translationX = Input.GetAxis("Vertical3");
            float translationY = Input.GetAxis("Horizontal3");
            Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * Speed * Time.deltaTime;
            transform.Translate(playerMovment, Space.Self);
        }
        else if (PlayerNum == 4)
        {
            float translationX = Input.GetAxis("Vertical4");
            float translationY = Input.GetAxis("Horizontal4");
            Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * Speed * Time.deltaTime;
            transform.Translate(playerMovment, Space.Self);
        }


    }
}
