using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("TaxTile");
    }
}
