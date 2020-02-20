using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisonMovement : MonoBehaviour
{
    // Get players
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Herd;
    private bool inPlay = true;
    public Rigidbody rb;
    public GameObject Ground;
    public bool onGround = true;
    private Vector2 groundPos;

    [SerializeField, Range(0, 10)]
    private float playerStrength = 0.75f;

    [SerializeField, Range(0, 10)]
    private float bisonStrength = 0.1f;

    [SerializeField, Range(0, 1)]
    private float bisonSpook = 0.1f;

    public float maxDistFromCenter;
    public Material offGroundMat;
    public int teamAlliance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player1 = GameObject.Find("PlayerRed");
        Player2 = GameObject.Find("PlayerBlue");
        Herd = GameObject.Find("Bison Herd");
        Ground = GameObject.Find("Ground");
        groundPos = new Vector2(Ground.transform.position.x, Ground.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Check out of bounds
        float distanceFromCenter = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), groundPos);
        if (distanceFromCenter > maxDistFromCenter) {
            onGround = false;
        }
        if (!onGround) {
            Ragdoll();
            GetComponent<Renderer>().material = offGroundMat;
        }

        if (this.inPlay)
        {
            // Apply force from players
            Vector3 force = PlayerForce(Player1);
            rb.AddForce(force, ForceMode.Impulse);
            force = PlayerForce(Player2);
            rb.AddForce(force, ForceMode.Impulse);

            // Testing bison turning AI
            TurnAway(Player1.transform.position);
            TurnAway(Player2.transform.position);

            // Apply force from bisons
            // For each bison
            for (int i = 0; i < Herd.transform.childCount; i++)
            {
                force = BisonForce(i);
                //rb.AddForce(force, ForceMode.Impulse);
            }
        }
        
    }

    // From https://forum.unity.com/threads/rotate-away-from-game-object-method.144651/
    public void TurnAway(Vector3 position)
    {
        Vector3 facing = position - transform.position;
        if (facing.magnitude < 1.0f) { return; }
        if (facing.magnitude > 10f) { return; }
        float turnSpeed = bisonSpook * (facing.magnitude+1)/10;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(facing), turnSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // Function to calculate force bison are exerting on bison
    private Vector3 BisonForce(int index)
    {
        Vector3 bisonForce; // returns this

        // Get the positions
        Vector3 bPos = Herd.transform.GetChild(index).position;
        Vector3 myPos = transform.position;

        // Get distance and direction to bison
        float dist = (myPos - bPos).magnitude;
        if (dist > 1.5)
        {
            return Vector3.zero; // Exit if bison is far away
        }

        Vector3 direction = (myPos - bPos).normalized;

        // Figure out how strong the push should be
        bisonForce = direction * (bisonStrength/dist);
        
        bisonForce.y = 0;
        return bisonForce;
    }

    // Function to calculate force player is exerting on bison
    private Vector3 PlayerForce(GameObject player)
    {
        Vector3 playerForce; // returns this

        // Get the positions
        Vector3 myPos = transform.position;
        Vector3 pPos = player.transform.position;

        // Get distance and direction to players
        float dist = (myPos - pPos).magnitude;
        if (dist > 5)
        {
            return Vector3.zero; // Exit if player is far away
        }

        Vector3 direction = (myPos - pPos).normalized;

        // Figure out how strong the push should be
        playerForce = direction * (playerStrength/dist);

        playerForce.y = 0;
        return playerForce;
    }

    // Falls out of play
    void Ragdoll()
    {
        //rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.angularDrag = 0;
        rb.drag = 0;
        rb.angularVelocity = new Vector3(Random.value, Random.value, Random.value);
        this.inPlay = false;
    }
}