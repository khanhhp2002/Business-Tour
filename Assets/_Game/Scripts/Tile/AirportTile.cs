using UnityEngine;

public class AirportTile : TileBase
{
    public override void OnPlayerEnter(Player player)
    {

    }

    public override void OnMouseButtonDown()
    {
        // Show the last turn that you traveled to this airport tile
        Debug.Log("AirportTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }
}
