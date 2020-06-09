using UnityEngine;


public class MainMenuu : MonoBehaviour
{
    [SerializeField] private NetworkManagerBBB networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }
}

