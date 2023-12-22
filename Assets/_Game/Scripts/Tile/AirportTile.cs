using UnityEngine;

public class AirportTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {
        player.MoveToTile(TileManager.Instance.GetTile(0));
    }

    public override void OnMouseButtonDown()
    {
        // Show the last turn that you traveled to this airport tile
        Debug.Log("AirportTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
