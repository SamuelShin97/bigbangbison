using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float RoatationSpeed = 250;
    public Transform Target, Player;
    public int playerNum;
    public float MaxClamp = 40.0f;
    public float MinClamp = 10.0f;
    public float distance = 20.0f;
    float MouseX, MouseY;
    Camera m_MainCamera;

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = 
        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;
    }

    void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {   
        //Vector3 dir = new Vector3(0, 0, -distance); might need this
        if (playerNum == 1) {
            MouseX += Input.GetAxis("MouseX1") * RoatationSpeed * Time.deltaTime;
            MouseY += Input.GetAxis("MouseY1") * RoatationSpeed * Time.deltaTime / 2;
            MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        }
        else if (playerNum == 2)
        {
            MouseX += Input.GetAxis("MouseX2") * RoatationSpeed * Time.deltaTime;
            MouseY += Input.GetAxis("MouseY2") * RoatationSpeed * Time.deltaTime/2;
            MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        }
        else if (playerNum == 3)
        {
            MouseX += Input.GetAxis("MouseX3") * RoatationSpeed * Time.deltaTime;
            MouseY += Input.GetAxis("MouseY3") * RoatationSpeed * Time.deltaTime/2;
            MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        }
        else if (playerNum == 4)
        {
            MouseX += Input.GetAxis("MouseX4") * RoatationSpeed * Time.deltaTime;
            MouseY += Input.GetAxis("MouseY4") * RoatationSpeed * Time.deltaTime/2;
            MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        }
        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
