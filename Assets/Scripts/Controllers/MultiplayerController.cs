using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class MultiplayerController : MonoBehaviourPunCallbacks
{
    public bool IsMultiplayer
    {
        get => _isMultiplayer;
        set => _isMultiplayer = value;
    }

    private bool _isMultiplayer;

    private GameController _gameController;
    private PanelMultiplayer _panelMultiplayer;

    [Inject]
    private void Construct(GameController gameController, PanelMultiplayer panelMultiplayer)
    {
        _gameController = gameController;
        _panelMultiplayer = panelMultiplayer;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            _panelMultiplayer.ShowInput();
            Debug.Log(_isMultiplayer);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            _panelMultiplayer.ShowStatus("CONNECTING...");

            Debug.Log("Connecting");
        }   
    }

    public override void OnConnectedToMaster()
    {
        _isMultiplayer = true;
        PhotonNetwork.JoinLobby();

        Debug.Log("Connected");
    }

    public override void OnJoinedLobby()
    {
        _panelMultiplayer.ShowInput();

        Debug.Log("Lobby");
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
        _panelMultiplayer.ShowStatus("JOINING...");
    }

    public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name);
        _panelMultiplayer.ShowStatus("CREATING...");
    }

    public override void OnJoinedRoom()
    {
        _gameController.Game();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _panelMultiplayer.ShowInput();
        _panelMultiplayer.ShowStatusThenHide(message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _panelMultiplayer.ShowInput();
        _panelMultiplayer.ShowStatusThenHide(message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _panelMultiplayer.ShowStatusThenHide("Disconnected");
    }

    public GameObject SpawnPlayer(string player, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(player, position, rotation);
    }

    public void LeaveRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        IsMultiplayer = false;
        PhotonNetwork.LeaveRoom();
        _panelMultiplayer.ShowInput(); 
    }

    public void LeaveServer()
    {
        if (!PhotonNetwork.IsConnected) return;

        IsMultiplayer = false;
        PhotonNetwork.Disconnect();
    }

    public void GameOverMultiplayer(byte eventCode)
    {
        object[] content = null;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(eventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == 0)
            _gameController.Defeat();
        if (eventCode == 1)
            _gameController.Win();
    }
}
