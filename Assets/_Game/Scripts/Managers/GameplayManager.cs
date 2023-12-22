using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private Queue<Player> _players = new Queue<Player>();

    private Player _currentPlayer;

    private void Start()
    {
        var player = FindFirstObjectByType<Player>();
        _players.Enqueue(player);
        Invoke(nameof(ChangePlayer), 1f);
    }

    public void ChangePlayer()
    {
        if (_currentPlayer != null) _players.Enqueue(_currentPlayer);
        _currentPlayer = _players.Dequeue();
        _currentPlayer.OnStartTurn();
    }
}

