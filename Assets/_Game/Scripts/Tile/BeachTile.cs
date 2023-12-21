using UnityEngine;

public class BeachTile : TileBase
{
    private PlayerColor _ownerColor;
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("BeachTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the value of current beach
        Debug.Log("BeachTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
