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
    [SerializeField] private TMP_Text _roomId;
    [SerializeField] private TMP_InputField _roomIdInput;
    [SerializeField] private Button _createRoomButton;
    private string _customRoomId;
    public List<Photon.Realtime.Player> _playerList = new List<Photon.Realtime.Player>();
    public Action OnLeaveAndJoinNewRoom;
    // Start is called before the first frame update
    void Start()
    {
        _logStatus.text = "Attempting to connect to server...";
        PhotonNetwork.ConnectUsingSettings();
        _createRoomButton.onClick.AddListener(CreateRoom);
    }

    public override void OnConnectedToMaster()
    {
        _logStatus.text = "Attempting to join lobby...";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _logStatus.text = "Joined lobby.";
    }

    public void CreateRoom()
    {
        string roomID = RandomString.Generate(6);
        _roomId.text = roomID;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomID, roomOptions);
        _createRoomButton.enabled = false;
    }

    public void ChangeRoomVisibleStatus(bool visible)
    {
        PhotonNetwork.CurrentRoom.IsVisible = visible;
    }

    public void ChangeRoomOpenStatus(bool isOpen)
    {
        PhotonNetwork.CurrentRoom.IsOpen = isOpen;
    }

    public void JoinRoomById()
    {
        if (string.IsNullOrEmpty(_roomIdInput.text))
        {
            _logStatus.text = "Room ID is empty.";
            return;
        }
        _customRoomId = _roomIdInput.text;
        if (RoomManager.Instance.IsRoomExist(_customRoomId))
        {
            _logStatus.text = $"Attempting to join room {_roomIdInput.text}...";
            OnLeaveAndJoinNewRoom = null;
            OnLeaveAndJoinNewRoom += () =>
            {
                PhotonNetwork.JoinRoom(_customRoomId);
                RoomManager.Instance.ClearList();
            };
            LeaveRoom();
        }
        else
        {
            _logStatus.text = $"Room {_roomIdInput.text} not found.";
        }
    }

    public override void OnJoinedRoom()
    {
        _roomId.text = PhotonNetwork.CurrentRoom.Name;
        _logStatus.text = $"Joined room {_roomId.text}";
        _customRoomId = string.Empty;
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
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        if (OnLeaveAndJoinNewRoom != null)
        {
            OnLeaveAndJoinNewRoom.Invoke();
            OnLeaveAndJoinNewRoom = null;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        _playerList.Add(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        _playerList.Remove(otherPlayer);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        Debug.Log($"Master client switched to {newMasterClient.NickName}");
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
