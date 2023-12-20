using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
