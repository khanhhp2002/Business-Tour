using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class Launcher : PhotonSingleton<Launcher>
{
    [SerializeField] private TMP_Text _logStatus;
    [SerializeField] private Button _leaveRoomButton;
    [SerializeField] private NamePanel _namePanel;
    public List<Photon.Realtime.Player> _playerList = new List<Photon.Realtime.Player>();
    public Action<string> OnJoinRoom;
    public Action OnLeaveRoom;
    public Action<int> OnPlayerCountChanged;
    public Action<Photon.Realtime.Player> OnPlayerJoined;
    public Action<Photon.Realtime.Player> OnPlayerLeft;
    // Start is called before the first frame update
    void Start()
    {
        _logStatus.text = "Attempting to connect to server...";
        PhotonNetwork.ConnectUsingSettings();
        _leaveRoomButton.onClick.AddListener(LeaveRoom);
    }

    public override void OnConnectedToMaster()
    {
        _logStatus.text = "Attempting to join lobby...";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _logStatus.text = "Joined lobby.";
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            _namePanel.gameObject.SetActive(true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void CreateRoom()
    {
        string roomID = RandomString.Generate(6);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    public void ChangeRoomVisibleStatus(bool visible)
    {
        PhotonNetwork.CurrentRoom.IsVisible = visible;
    }

    public void ChangeRoomOpenStatus(bool isOpen)
    {
        PhotonNetwork.CurrentRoom.IsOpen = isOpen;
    }

    public void JoinRoomById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logStatus.text = "Room ID is empty.";
            return;
        }
        if (RoomManager.Instance.IsRoomExist(id))
        {
            _logStatus.text = $"Attempting to join room {id}...";
            PhotonNetwork.JoinRoom(id);
        }
        else
        {
            _logStatus.text = $"Room {id} not found.";
        }
    }

    public override void OnJoinedRoom()
    {
        OnJoinRoom?.Invoke(PhotonNetwork.CurrentRoom.Name);
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        FlagManager.Instance.AddAllPlayer(players);
        _playerList.AddRange(players);
        OnPlayerCountChanged?.Invoke(players.Length);
        RoomManager.Instance.ClearList();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _logStatus.text = $"Failed to create room: {message}";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _logStatus.text = $"Failed to join room: {message}";
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _logStatus.text = $"Failed to join random room: {message}";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _logStatus.text = $"Disconnected: {cause}";
    }

    public override void OnLeftRoom()
    {
        _logStatus.text = $"Left room.";
        OnLeaveRoom?.Invoke();
    }

    public void LeaveRoom()
    {
        Debug.Log("Leave room");
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            _playerList.Clear();
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ChatManager.Instance.SentMessage($"Player {newPlayer.NickName} joined client.");
        OnPlayerJoined?.Invoke(newPlayer);
        _playerList.Add(newPlayer);
        OnPlayerCountChanged?.Invoke(_playerList.Count);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        ChatManager.Instance.SentMessage($"Player {otherPlayer.NickName} left client.");
        OnPlayerLeft?.Invoke(otherPlayer);
        _playerList.Remove(otherPlayer);
        OnPlayerCountChanged?.Invoke(_playerList.Count);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        ChatManager.Instance.SentMessage($"Player {newMasterClient.NickName} is now the master client.");
    }

    public void KickPlayer(string playerNickName)
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == playerNickName)
            {
                PhotonNetwork.CloseConnection(player);
            }
        }
    }
}