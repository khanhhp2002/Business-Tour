using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTile : TileBase
{
    private PlayerColor _ownerColor;
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("CityTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the value of current city
        Debug.Log("CityTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
