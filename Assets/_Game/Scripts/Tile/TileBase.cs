using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] private int _tileIndex;
    [SerializeField] private SpriteRenderer _imageSprite;

    public SpriteRenderer ImageSprite { get => _imageSprite; }
    public int TileIndex => _tileIndex;
    public virtual void OnPlayerEnter(Player player)
    {

    }
    public virtual void OnMouseButtonDown()
    {
        Debug.Log("TileBase");
    }
}
