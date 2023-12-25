using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour, IPoolable<Room>
{
    [SerializeField] private Button _joinButton;
    [SerializeField] private TMP_Text _roomId;
    [SerializeField] private TMP_Text _roomPlayerCountText;

    public string Id => _roomInfo.Name;
    public int PlayerCount => _roomInfo.PlayerCount;
    private RoomInfo _roomInfo;
    private Action<Room> _returnAction;

    public void SetRoomData(RoomInfo roomInfo)
    {
        _joinButton.onClick.RemoveAllListeners();
        _roomInfo = roomInfo;
        _roomId.text = roomInfo.Name;
        _roomPlayerCountText.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
        _joinButton.enabled = roomInfo.IsOpen && roomInfo.PlayerCount < roomInfo.MaxPlayers;
        _joinButton.onClick.AddListener(JoinRoom);
    }

    public void Initialize(Action<Room> returnAction)
    {
        _returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(this);
    }

    public void JoinRoom()
    {
        Launcher.Instance.JoinRoomById(_roomInfo.Name);
        RoomManager.Instance.ClearList();
    }
}
