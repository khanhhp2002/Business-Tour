using DG.Tweening;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private TileBase[] tiles;

    private Tween _blinkTween;
    public TileBase GetTile(int index)
    {
        return tiles[index];
    }
    public void BlinkImage(TileBase tile, bool canBlink)
    {
        if (canBlink && _blinkTween == null)
            _blinkTween = tile.ImageSprite.DOFade(0.5f, 0.25f).SetLoops(-1, LoopType.Yoyo);
        else
        {
            _blinkTween.Rewind();
            _blinkTween.Kill();
            _blinkTween = null;
        }
    }
    public int TilesCount => tiles.Length;
}
