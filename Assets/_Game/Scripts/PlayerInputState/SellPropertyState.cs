using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPropertyState : IPlayerInputState
{
    private Player _thisPlayer;
    public void OnEnter(Player player)
    {
        _thisPlayer = player;
        TileManager.Instance.OnTileClicked += OnPlayerSelectTile;
    }
    public void OnExecute(Player player)
    {
    }

    public void OnExit(Player player)
    {
        TileManager.Instance.OnTileClicked -= OnPlayerSelectTile;
    }
    private void OnPlayerSelectTile(TileBase tile)
    {
        CityTile temp = tile as CityTile;
        if (temp != null && temp.OwnerColor == _thisPlayer.PlayerColor)
        {
            //let player choose tiles to sell
        }
    }
}
