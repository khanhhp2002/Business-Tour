using System.Collections.Generic;
using UnityEngine;

public class FlagManager : Singleton<FlagManager>
{
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Transform _flagContainer;
    private ObjectPool<PlayerProfileUI> _flagPool;
    private List<PlayerProfileUI> _activeFlags = new List<PlayerProfileUI>();
    private void Start()
    {
        _flagPool = new ObjectPool<PlayerProfileUI>(_flagPrefab, 4);
        Launcher.Instance.OnLeaveRoom += ClearAll;
        Launcher.Instance.OnPlayerJoined += AddPlayer;
        Launcher.Instance.OnPlayerLeft += RemovePlayer;
    }

    public void AddPlayer(Photon.Realtime.Player newPlayer)
    {
        PlayerProfileUI flag = _flagPool.Pull(_flagContainer);
        flag.PlayerName.text = newPlayer.NickName;
        flag.PlayerId = newPlayer.ActorNumber;
        _activeFlags.Add(flag);
    }

    public void RemovePlayer(Photon.Realtime.Player newPlayer)
    {
        foreach (PlayerProfileUI flag in _activeFlags)
        {
            if (flag.PlayerId == newPlayer.ActorNumber)
            {
                _activeFlags.Remove(flag);
                flag.ReturnToPool();
                break;
            }
        }
    }

    public void ClearAll()
    {
        foreach (PlayerProfileUI flag in _activeFlags)
        {
            flag.ReturnToPool();
        }
        _activeFlags.Clear();
    }

    public void AddAllPlayer(Photon.Realtime.Player[] newPlayers)
    {
        foreach (Photon.Realtime.Player newPlayer in newPlayers)
        {
            AddPlayer(newPlayer);
        }
    }
}
