using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gameIsPaused = false;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject GamePlayManager;
    PlayerMovement[] movementScripts;

    // Start is called before the first frame update
    void Start()
    {
        movementScripts = GamePlayManager.GetComponentsInChildren<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonUp("XboxStart"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        for (int i = 0; i < movementScripts.Length; i++)
        {
            movementScripts[i].enabled = true;
        }
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
        for (int i = 0; i < movementScripts.Length; i++)
        {
            movementScripts[i].enabled = false;
        }
    }
}
