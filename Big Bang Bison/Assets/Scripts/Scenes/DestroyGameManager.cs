using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject gamePlayManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gamePlayManager = GameObject.Find("GamePlayManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null)
        {
            Destroy(gameManager);
        }
        if (gamePlayManager != null)
        {
            Destroy(gamePlayManager);
        }
    }
}
