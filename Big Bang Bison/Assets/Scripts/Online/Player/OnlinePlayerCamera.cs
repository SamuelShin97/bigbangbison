using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnlinePlayerCamera : NetworkBehaviour
{
    public float RoatationSpeed = 250;
    public Transform target, player, cam;
    public float MaxClamp = 40.0f;
    public float MinClamp = 10.0f;
    public float distance = 20.0f;

    private string rightJoyStickX;
    private string rightJoyStickY;
    /*private Rect upperLeftPanel = new Rect(0f, 0.5f, 0.5f, 0.5f);
    private Rect upperRightPanel = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
    private Rect lowerLeftPanel = new Rect(0f, 0f, 0.5f, 0.5f);
    private Rect lowerRightPanel = new Rect(0.5f, 0f, 0.5f, 0.5f);*/
    //int playerNum;
    float MouseX, MouseY;
    Camera m_MainCamera;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = 
        //playerNum = GetComponentInParent<Transform>().gameObject.GetComponentInParent<OnlinePlayerProperties>().playerNum;
        //Debug.Log("player num in camera is " + playerNum);

        //target = GetComponentInParent<Transform>(); //gets target 
        //player = GetComponentInParent<Transform>().GetComponentInParent<Transform>(); //gets player's transform
        //m_MainCamera = Camera.main;
        //m_MainCamera.rect = GetCorrectPanel();

        //m_MainCamera.enabled = true;
        rightJoyStickX = "MouseX";
        rightJoyStickY = "MouseY";
    }

    void LateUpdate()
    {
        CamControl();
    }

    public override void OnStartAuthority()
    {
        Debug.Log("onstartauthority");
        cam.gameObject.GetComponent<Camera>().enabled = true;
    }

    [Client]
    void CamControl()
    {
        if (!hasAuthority)
        {
            return;
        }
        //Vector3 dir = new Vector3(0, 0, -distance); might need this
        MouseX += Input.GetAxis(rightJoyStickX) * RoatationSpeed * Time.deltaTime;
        MouseY += Input.GetAxis(rightJoyStickY) * RoatationSpeed * Time.deltaTime / 2;
        MouseY = Mathf.Clamp(MouseY, MinClamp, MaxClamp);
        
        cam.LookAt(target);
        target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        player.rotation = Quaternion.Euler(0, MouseX, 0);
    }

    /*Rect GetCorrectPanel()
    {
        if (playerNum == 1)
        {
            return upperLeftPanel;
        }
        else if (playerNum == 2)
        {
            return upperRightPanel;
        }
        else if (playerNum == 3)
        {
            return lowerLeftPanel;
        }
        else if (playerNum == 4)
        {
            return lowerRightPanel;
        }
        return new Rect(0f, 0f, 1f, 1f);
    }*/

}
