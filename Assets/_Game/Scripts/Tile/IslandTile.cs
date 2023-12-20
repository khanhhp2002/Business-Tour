using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("IslandTile");
    }

    public override void OnMouseButtonDown()
    {
        Debug.Log("IslandTile");
    }
}
