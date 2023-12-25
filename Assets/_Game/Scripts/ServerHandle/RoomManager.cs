using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : PhotonSingleton<RoomManager>
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private Transform _roomContainer;
    private Color32 _joinableColor;
    private Color32 _fullColor;
    private RoomInfo _currentJoinRoomInfo;
    public List<Room> _availableRoomList = new List<Room>();
    private ObjectPool<Room> _roomPool;

    private void Start()
    {
        _roomPool = new ObjectPool<Room>(_roomPrefab);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"OnRoomListUpdate {roomList.Count}");
        ClearList();

        foreach (var roomInfo in roomList)
        {
            if (roomInfo.PlayerCount == 0 || !roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList) continue;
            Room temp = _roomPool.Pull(_roomContainer);
            temp.SetRoomData(roomInfo);
            _availableRoomList.Add(temp);
        }
    }

    public bool IsRoomExist(string id)
    {
        foreach (var room in _availableRoomList)
        {
            if (room.Id.Equals(id))
            {
                return true;
            }
        }
        return false;
    }

    public int RoomPlayerCount(string id)
    {
        foreach (var room in _availableRoomList)
        {
            if (room.Id == id)
            {
                return room.PlayerCount;
            }
        }
        return -1;
    }
    public void ClearList()
    {
        if (_availableRoomList.Count == 0) return;
        foreach (var room in _availableRoomList)
        {
            room.ReturnToPool();
        }

        _availableRoomList.Clear();
    }
}
