using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("FortuneTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the total of good and bad fortune that you have received
        Debug.Log("FortuneTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
