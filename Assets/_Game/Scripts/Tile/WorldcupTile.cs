using UnityEngine;

public class WorldcupTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("WorldcupTile");
    }

    public override void OnMouseButtonDown()
    {
        // show current city that organized worldcup
        Debug.Log("WorldcupTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
