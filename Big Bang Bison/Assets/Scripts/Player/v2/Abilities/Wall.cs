using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public int PlayerNum;

    public bool PullActivate = false;
    public Transform SpawnPoint;
    public GameObject WallObj;
    public float AoeRadius = 5.0f;
    private string wall = "Ability";
    private bool showGizmos = false;

    void Start()
    {
        if (PullActivate)
        {
            wall = wall + PlayerNum;

        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PullActivate)
        {
            if (Input.GetButton(wall))
            {
                
            }
            if (Input.GetButtonUp(wall))
            {
                Instantiate(WallObj, (SpawnPoint.position), SpawnPoint.rotation);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(SpawnPoint.position, AoeRadius);
        }
    }
}
