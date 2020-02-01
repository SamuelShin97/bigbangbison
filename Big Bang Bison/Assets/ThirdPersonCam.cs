using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float RoatationSpeed = 3;
    public Transform Target, Player;
    public int playerNum;
    float MouseX, MouseY;

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = 
    }

    void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {   
        //Vector3 dir = new Vector3(0, 0, -distance); might need this
        if (playerNum == 1) {
            MouseX += Input.GetAxis("MouseX1") * RoatationSpeed;
            MouseY += Input.GetAxis("MouseY1") * RoatationSpeed;
            MouseY = Mathf.Clamp(MouseY, 0, 50);
        }
        else if (playerNum == 2)
        {
            MouseX += Input.GetAxis("MouseX2") * RoatationSpeed;
            MouseY += Input.GetAxis("MouseY2") * RoatationSpeed;
            MouseY = Mathf.Clamp(MouseY, 0, 50);
        }
        else if (playerNum == 3)
        {
            MouseX += Input.GetAxis("MouseX3") * RoatationSpeed;
            MouseY += Input.GetAxis("MouseY3") * RoatationSpeed;
            MouseY = Mathf.Clamp(MouseY, 0, 50);
        }
        else if (playerNum == 4)
        {
            MouseX += Input.GetAxis("MouseX4") * RoatationSpeed;
            MouseY += Input.GetAxis("MouseY4") * RoatationSpeed;
            MouseY = Mathf.Clamp(MouseY, 0, 50);
        }
        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
