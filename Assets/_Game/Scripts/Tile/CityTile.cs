using UnityEngine;

public class CityTile : TileBase
{
    // 0.25 / 0.6
    private PlayerColor _ownerColor;
    private int _level;
    private Building _property;
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
        _ownerColor = playerColor;
        _property = Instantiate(TileManager.Instance.GetBuilding(level - 1), transform);
        _property.transform.localPosition = new Vector3(0.25f, 0.6f, 0f);

    }

    public void UpgradeProperty()
    {

    }
}
