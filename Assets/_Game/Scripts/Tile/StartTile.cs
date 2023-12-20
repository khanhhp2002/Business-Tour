using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("StartTile");
    }

    public override void OnMouseButtonDown()
    {
        Debug.Log("StartTile");
    }
}
