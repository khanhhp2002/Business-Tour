using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private Queue<Player> _players = new Queue<Player>();

    private Player _currentPlayer;

    private GameplayState _gameplayState = GameplayState.None;

    private void Start()
    {
        var player = FindFirstObjectByType<Player>();
        _players.Enqueue(player);
        _currentPlayer = _players.Dequeue();
        Invoke(nameof(ChangeToNextState), 1f);
    }

    public void ChangeToNextState()
    {
        switch (_gameplayState)
        {
            case GameplayState.None:
                _gameplayState = GameplayState.StartTurn;
                _currentPlayer.OnStartTurn();
                break;
            case GameplayState.StartTurn:
                _gameplayState = GameplayState.PlayerTurn;
                _currentPlayer.OnGetTask();
                break;
            case GameplayState.PlayerTurn:
                _gameplayState = GameplayState.EndTurn;
                _currentPlayer.OnEndTurn();
                break;
            case GameplayState.EndTurn:
                _gameplayState = GameplayState.StartTurn;
                _players.Enqueue(_currentPlayer);
                _currentPlayer = _players.Dequeue();
                _currentPlayer.OnStartTurn();
                break;
        }
        UIManager.Instance.currentStateValue.text = _gameplayState.ToString();
    }
}

