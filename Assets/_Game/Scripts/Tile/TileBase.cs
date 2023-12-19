using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] private int _tileIndex;

    public int TileIndex => _tileIndex;

    public virtual void OnPlayerEnter(Player player)
    {

    }
}
