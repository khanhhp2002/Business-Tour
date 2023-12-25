using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToTileState : IPlayerInputState
{
    private Player _thisPlayer;
    private float _timer;
    public void OnEnter(Player player)
    {
        _thisPlayer = player;
        _timer = 10f;
        TileManager.Instance.OnTileClicked += OnPlayerSelectTile;
    }
    public void OnExecute(Player player)
    {
        if (_timer <= 0)
        {
            _thisPlayer.PlayerMovement.MoveToTile(TileManager.Instance.Start, _thisPlayer);
        }
        _timer -= Time.deltaTime;
    }
    public void OnExit(Player player)
    {
        TileManager.Instance.OnTileClicked -= OnPlayerSelectTile;
    }
    private void OnPlayerSelectTile(TileBase tile)
    {
        CityTile temp = tile as CityTile;
        if (temp != null)
        {
            _thisPlayer.PlayerMovement.MoveToTile(temp, _thisPlayer);
        }
    }
}
