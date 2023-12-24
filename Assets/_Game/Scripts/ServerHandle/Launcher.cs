using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
public class Launcher : PhotonSingleton<Launcher>
{
    [SerializeField] private TMP_Text _logStatus;
    [SerializeField] private TMP_Text _roomId;
    [SerializeField] private TMP_InputField _roomIdInput;
    private string _customRoomId;
    private bool _isLeaveAndJoinNewRoom = false;
    public List<RoomInfo> _roomList = new List<RoomInfo>();
    public List<Photon.Realtime.Player> _playerList = new List<Photon.Realtime.Player>();
    // Start is called before the first frame update
    void Start()
    {
        _logStatus.text = "Attempting to connect to server...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _logStatus.text = "Attempting to join lobby...";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _logStatus.text = "Joined lobby.";
        if (_isLeaveAndJoinNewRoom)
        {
            _isLeaveAndJoinNewRoom = false;
            PhotonNetwork.JoinRoom(_customRoomId);
        }
        else
        {
            CreateRoom();
        }
    }

    public void CreateRoom()
    {
        string roomID = RandomString.Generate(6);
        _roomId.text = roomID;
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

    public void JoinRoomById()
    {
        if (string.IsNullOrEmpty(_roomIdInput.text))
        {
            _logStatus.text = "Room ID is empty.";
            return;
        }
        _customRoomId = _roomIdInput.text;
        if (FindRoomById(_roomIdInput.text))
        {
            _logStatus.text = $"Attempting to join room {_roomIdInput.text}...";
            _isLeaveAndJoinNewRoom = true;
            LeaveRoom();
        }
        else
        {
            _logStatus.text = $"Room {_roomIdInput.text} not found.";
        }
    }

    private bool FindRoomById(string id)
    {
        foreach (var room in _roomList)
        {
            if (room.Name == id)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnJoinedRoom()
    {
        _logStatus.text = $"Joined room {(!string.IsNullOrEmpty(_customRoomId) ? _customRoomId : _roomId.text)}";
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
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList.Clear();
        _roomList = roomList;
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
