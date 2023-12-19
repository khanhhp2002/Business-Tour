using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("BeachTile");
    }
}
