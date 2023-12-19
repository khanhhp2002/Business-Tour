using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldcupTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("WorldcupTile");
    }
}
