using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        player.MoveToTile(TileManager.Instance.GetTile(0));
    }

    public override void OnMouseButtonDown()
    {
        Debug.Log("AirportTile");
    }
}
