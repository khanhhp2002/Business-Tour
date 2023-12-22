using UnityEngine;

public class CityTile : TileBase
{
    // 0.25 / 0.6
    private PlayerColor _ownerColor;
    private int _level;
    private Building _property;

    public Building Property { get => _property; }
    public PlayerColor OwnerColor { get => _ownerColor; }

    public override void OnPlayerEnter(Player player)
    {
        Debug.Log("CityTile");
    }

    public override void OnMouseButtonDown()
    {
        // Show the value of current city
        Debug.Log("CityTile");
        TileManager.Instance.OnTileClicked?.Invoke(this);
    }

    public void CreateProperty(int level, PlayerColor playerColor)
    {
        bool canFlip = this.ImageSprite.flipX;
        _property = Instantiate(TileManager.Instance.GetBuilding(level - 1), transform);
        Vector3 temp = Property.transform.localPosition;
        Property.SpRender.flipX = canFlip;
        //_ownerColor = playerColor;
        Property.transform.localPosition = (canFlip) ? new Vector3(-temp.x, temp.y, temp.z) : new Vector3(temp.x, temp.y, temp.z);
        _level = level;
    }

    public void UpgradeProperty()
    {

    }
}
