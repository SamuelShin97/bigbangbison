using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnlinePlayerMovement : OnlinePlayerProperties
{
    // Start is called before the first frame update

    public float speed = 20.0f;
    public float rotationSpeed = 100.0f;
    public Animator anim;
    private string verticalAxis;
    private string horizontalAxis;

    void Start()
    {
        playerNum = GetComponent<OnlinePlayerProperties>().playerNum;
        Debug.Log("movement " + playerNum);
        verticalAxis = "Vertical" + playerNum;
        horizontalAxis = "Horizontal" + playerNum; 
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        float translationX = Input.GetAxis(verticalAxis);
        float translationY = Input.GetAxis(horizontalAxis);
        Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * speed * Time.deltaTime;
        transform.Translate(playerMovment, Space.Self);

        anim.SetFloat("Vertical", Input.GetAxis(verticalAxis));
        anim.SetFloat("Horizontal", Input.GetAxis(horizontalAxis));
    }
    
}
