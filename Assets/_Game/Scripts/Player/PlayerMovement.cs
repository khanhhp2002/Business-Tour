using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void MoveWithDice(int value, int currentTileIndex, bool isPair, Action<TileBase> onEndMoving)
    {
        StartCoroutine(MoveWithDice(value, currentTileIndex, onEndMoving));
    }

    IEnumerator MoveWithDice(int value, int currentTileIndex, Action<TileBase> onEndMoving)
    {
        int step = 0;
        while (step < value)
        {
            step++;
            int tempTileIndex = currentTileIndex + step;
            if (tempTileIndex == TileManager.Instance.TilesCount)
            {
                step = 0;
                currentTileIndex = 0;
                tempTileIndex = 0;
                value -= step;
            }
            Vector3 newPosition = TileManager.Instance.GetTile(tempTileIndex).transform.position;
            newPosition.y += 0.5f;
            transform.position = newPosition;
            yield return new WaitForSeconds(0.5f);
        }
        onEndMoving?.Invoke(TileManager.Instance.GetTile(currentTileIndex + step));
    }

    public void MoveToTile(TileBase tile)
    {
        transform.position = tile.transform.position;
    }

    public void MoveAround()
    {

    }
}
