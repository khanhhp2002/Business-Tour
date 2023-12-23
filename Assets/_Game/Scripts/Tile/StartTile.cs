using UnityEngine;

public class StartTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("StartTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show total rounds you have passed
        Debug.Log("StartTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
