using DG.Tweening;
using System;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private TileBase[] tiles;
    [SerializeField] private Building[] buildings;
    [SerializeField] private AirportTile _airport;
    [SerializeField] private StartTile _start;
    [SerializeField] private WorldcupTile _worldCup;
    [SerializeField] private IslandTile _island;
    [SerializeField] private TaxTile _tax;

    public AirportTile Airport { get => _airport; }
    public StartTile Start { get => _start; }
    public WorldcupTile WorldCup { get => _worldCup; }
    public IslandTile Island { get => _island; }
    public TaxTile Tax { get => _tax; }
    //event
    public Action<TileBase> OnTileClicked;
    private Tween _blinkTween;
    public TileBase GetTile(int index)
    {
        return tiles[index];
    }
    public Building GetBuilding(int index)
    {
        return buildings[index];
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
