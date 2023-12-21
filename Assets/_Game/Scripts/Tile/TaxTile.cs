using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("TaxTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the total tax that each player already paid on this game
        Debug.Log("TaxTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
