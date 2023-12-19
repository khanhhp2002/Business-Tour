using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private TileBase[] tiles;

    public TileBase GetTile(int index)
    {
        return tiles[index];
    }

    public int TilesCount => tiles.Length;
}
