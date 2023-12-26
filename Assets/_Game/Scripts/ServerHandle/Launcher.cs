using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class Launcher : PhotonSingleton<Launcher>
{
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
        //Using the setting of PUN(Photon Unity Network) if success return OnConnectedToMaster
        PhotonNetwork.ConnectUsingSettings();
        _leaveRoomButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound((int)SFXType.ButtonClick);
            LeaveRoom();
        });
    }

    public override void OnConnectedToMaster()
    {
        //Join a lobby of server if success return OnJoinedLobby
        PhotonNetwork.JoinLobby();
        //Sync scene following the host
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
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
        if (!PhotonNetwork.InLobby) return;
        string roomID = RandomString.Generate(6);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        //Create a room in lobby if success return OnJoinedRoom
        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    public void JoinRoomById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        if (RoomManager.Instance.IsRoomExist(id))
        {
            //Join a room by id if success return OnJoinedRoom
            PhotonNetwork.JoinRoom(id);
        }
        else
        {
        }
    }

    public override void OnJoinedRoom()
    {
        SoundManager.Instance.PlaySound((int)SFXType.JoinRoom);
        OnJoinRoom?.Invoke(PhotonNetwork.CurrentRoom.Name);
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        FlagManager.Instance.AddAllPlayer(players);
        _playerList.AddRange(players);
        OnPlayerCountChanged?.Invoke(players.Length);
        RoomManager.Instance.ClearList();
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            _playerList.Clear();
        }
    }

    public override void OnLeftRoom()
    {
        SoundManager.Instance.PlaySound((int)SFXType.LeaveRoom);
        OnLeaveRoom?.Invoke();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        SoundManager.Instance.PlaySound((int)SFXType.JoinRoom);
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

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        ChatManager.Instance.SentMessage($"Player {newMasterClient.NickName} is now the master client.");
    }

    public void ChangeRoomVisibleStatus(bool visible)
    {
        PhotonNetwork.CurrentRoom.IsVisible = visible;
    }

    public void ChangeRoomOpenStatus(bool isOpen)
    {
        PhotonNetwork.CurrentRoom.IsOpen = isOpen;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
    }
}