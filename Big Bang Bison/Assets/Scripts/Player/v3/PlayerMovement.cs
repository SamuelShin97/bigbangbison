using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerProperties
{
    // Start is called before the first frame update

    public float speed = 23.0f;
    public float rotationSpeed = 100.0f;
    public Animator anim;
    private string verticalAxis;
    private string horizontalAxis;
    private Rigidbody rb;
    bool triggered;
    static FMOD.Studio.EventInstance woosh;
    //grant fix

    void Start()
    {
        playerNum = GetComponent<PlayerProperties>().playerNum;
        //Debug.Log("movement " + playerNum);
        verticalAxis = "Vertical" + playerNum;
        horizontalAxis = "Horizontal" + playerNum;
        triggered = true;
        woosh = FMODUnity.RuntimeManager.CreateInstance("event:/UI/op3");
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
        float translationX = Input.GetAxis(verticalAxis);
        float translationY = Input.GetAxis(horizontalAxis);

        if(translationX != 0 || translationY != 0)
        {
            if (triggered)
            {
                triggered = false;
                woosh.start();
            }
        }
        else
        {
            triggered = true;
        }

        float trueSpeed = speed;
        if ( GetComponent<HerdShepherd>().pressure > 0.01)
        {
            trueSpeed -= speed / 3.5f;
        }

        //Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * speed * Time.deltaTime;

        //Vector3 playerMovment = new Vector3(translationY, 0f, translationX) * trueSpeed * Time.deltaTime;
        //transform.Translate(playerMovment, Space.Self);
        rb.velocity = new Vector3(0,0,0);
        rb.AddRelativeForce(new Vector3(translationY, rb.velocity.y, translationX) * trueSpeed * Time.deltaTime, ForceMode.VelocityChange);
        //(transform.right * translationX);

        //rb.velocity = new Vector3(translationY, rb.velocity.y, translationX) * trueSpeed;
        //rb.MovePosition(transform.position + (new Vector3(translationY, rb.velocity.y, translationX) * trueSpeed) * Time.deltaTime);

        anim.SetFloat("Vertical", Input.GetAxis(verticalAxis));
        anim.SetFloat("Horizontal", Input.GetAxis(horizontalAxis));
    }
    
}
