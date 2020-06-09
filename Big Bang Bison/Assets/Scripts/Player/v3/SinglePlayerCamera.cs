using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerCamera : PlayerCamera
{

    private string rightJoyStickX;
    private string rightJoyStickY;
    private Rect upperLeftPanel = new Rect(0f, 0.5f, 0.5f, 0.5f);
    private Rect upperRightPanel = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
    private Rect lowerLeftPanel = new Rect(0f, 0f, 0.5f, 0.5f);
    private Rect lowerRightPanel = new Rect(0.5f, 0f, 0.5f, 0.5f);
    float MouseX, MouseY;
    Camera m_MainCamera;
    Camera playerCam;

    void Start()
    {

        playerCam = gameObject.GetComponent<Camera>();
        playerNum = GetComponentInParent<Transform>().gameObject.GetComponentInParent<PlayerProperties>().playerNum;
        contNum = GetComponentInParent<Transform>().gameObject.GetComponentInParent<PlayerProperties>().controllerNum;

        playerCam.rect = GetCorrectPanel();
        playerCam.enabled = true;
        rightJoyStickX = "MouseX" + contNum;
        rightJoyStickY = "MouseY" + contNum;
    }

    void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {
        
        //Vector3 dir = new Vector3(0, 0, -distance); might need this
        MouseX += Input.GetAxis(rightJoyStickX) * RoatationSpeed * Time.deltaTime;
        MouseY += Input.GetAxis(rightJoyStickY) * RoatationSpeed * Time.deltaTime / 2;
        MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        
        transform.LookAt(target);
        target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        player.rotation = Quaternion.Euler(0, MouseX, 0);
    }

    Rect GetCorrectPanel()
    {
        return new Rect(0f, 0f, 1f, 1f);
    }

}
