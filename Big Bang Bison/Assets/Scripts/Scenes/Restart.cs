using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButton("XboxSubmit"))
        {
            Debug.Log("restart game1");
            RestartGame();
        }*/
    }

    public void RestartGame()
    {
        Debug.Log("restart game2");
        SceneManager.LoadScene(0);
    }
}
