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
        Debug.Log("FortuneTile");
    }
}
