using DG.Tweening;
using Photon.Realtime;
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
        TileBase destinationTile = FindDestination(currentTileIndex, value);
        while (0 < value--)
        {
            MoveToTile(ref currentTileIndex);
            yield return new WaitForSeconds(_moveDelayTime);
        }
        onEndMoving?.Invoke(destinationTile);
        TileManager.Instance.BlinkImage(destinationTile, false);
        DiceManager.Instance.OnOffDice(false);
        _isMoving = false;
    }
    public void MoveToTile(ref int tileIndex)
    {
        tileIndex++;
        if (tileIndex == TileManager.Instance.TilesCount) tileIndex = 0;
        Transform temp = TileManager.Instance.GetTile(tileIndex).transform;
        Vector3 newPosition = temp.position;
        transform.transform.DOJump(newPosition, 1f, 1, _moveDelayTime / 2f)
            .OnComplete(() => temp.DOMoveY(temp.position.y - _yOffset, _moveDelayTime / 2f).SetLoops(2, LoopType.Yoyo));
    }
    public void MoveToTile(TileBase tile, Player player)
    {
        transform.position = tile.transform.position;
        player.SetNewOccuiedTile(tile);
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
        TileBase destinationTile = FindDestination(currentTileIndex);
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
        onEndMoving.Invoke(destinationTile);
        TileManager.Instance.BlinkImage(destinationTile, false);
        _isMoving = false;
    }
    private TileBase FindDestination(int originTileIndex, int value = 0)
    {
        TileBase destinationTile;
        if (value == 0)
        {
            destinationTile = TileManager.Instance.GetTile(value);
        }
        else
        {
            destinationTile = TileManager.Instance.GetTile((originTileIndex + value) % TileManager.Instance.TilesCount);
        }
        TileManager.Instance.BlinkImage(destinationTile, true);
        return destinationTile;
    }
}
