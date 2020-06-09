using Mirror;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

    private NetworkManagerBBB room;
    private NetworkManagerBBB Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerBBB;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnNetworkDestroy()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}

