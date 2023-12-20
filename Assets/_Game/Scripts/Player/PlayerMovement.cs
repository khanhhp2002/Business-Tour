using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveDelayTime;

    private bool _isMoving = false;
    private float _yOffset = 0.075f;
    public void MoveWithDice(int value, int currentTileIndex, bool isPair, Action<TileBase> onEndMoving)
    {
        if (_isMoving) return;
        _isMoving = true;
        StartCoroutine(MoveWithDice(value, currentTileIndex, onEndMoving));
    }
    IEnumerator MoveWithDice(int value, int currentTileIndex, Action<TileBase> onEndMoving)
    {
        while (0 < value--)
        {
            MoveToTile(ref currentTileIndex);
            yield return new WaitForSeconds(_moveDelayTime);
        }
        onEndMoving?.Invoke(TileManager.Instance.GetTile(currentTileIndex));
        _isMoving = false;
    }
    public void MoveToTile(ref int tileIndex)
    {
        tileIndex++;
        if (tileIndex == TileManager.Instance.TilesCount) tileIndex = 0;
        Transform temp = TileManager.Instance.GetTile(tileIndex).transform;
        Vector3 newPosition = temp.position;
        newPosition.y += 0.5f;
        transform.position = newPosition;
        temp.DOMoveY(temp.position.y - _yOffset, _moveDelayTime / 2f).SetLoops(2, LoopType.Yoyo);
    }
    public void MoveToTile(TileBase tile)
    {
        transform.position = tile.transform.position;
    }

    public void MoveAround(int originTileIndex, Action<TileBase> onEndMoving)
    {
        if (_isMoving) return;
        _isMoving = true;
        StartCoroutine(MoveAroundCo(originTileIndex, onEndMoving));
    }
    IEnumerator MoveAroundCo(int originTileIndex, Action<TileBase> onEndMoving)
    {
        bool movedOneRound = false;
        int currentTileIndex = originTileIndex;
        while (!movedOneRound)
        {
            MoveToTile(ref currentTileIndex);
            yield return new WaitForSeconds(_moveDelayTime);
            if (currentTileIndex == originTileIndex) movedOneRound = true;
        }
        while (currentTileIndex != 0)
        {
            MoveToTile(ref currentTileIndex);
            yield return new WaitForSeconds(_moveDelayTime);
        }
        onEndMoving.Invoke(TileManager.Instance.GetTile(currentTileIndex));
        _isMoving = false;

    }
}
