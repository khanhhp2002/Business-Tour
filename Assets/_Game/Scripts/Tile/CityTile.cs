using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("CityTile");
    }
}
