using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameManager : MonoBehaviour
{
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null)
        {
            Destroy(gameManager);
        }
    }
}
