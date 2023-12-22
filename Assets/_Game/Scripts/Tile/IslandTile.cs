using UnityEngine;

public class IslandTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("IslandTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the last turn that you traveled to this jail tile
        Debug.Log("IslandTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
