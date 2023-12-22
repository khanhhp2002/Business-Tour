using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] private int _tileIndex;
    [SerializeField] private SpriteRenderer _imageSprite;
    [SerializeField] private TileType _tileType;


    public SpriteRenderer ImageSprite { get => _imageSprite; }
    public int TileIndex => _tileIndex;
    public TileType TileType { get => _tileType; }

    public virtual void OnPlayerEnter(Player player)
    {

    }
    public virtual void OnMouseButtonDown()
    {
        Debug.Log("TileBase");
    }
}
