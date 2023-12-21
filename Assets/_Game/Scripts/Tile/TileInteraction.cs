using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private TileBase _tileBase;

    public void OnMouseDown()
    {
        _tileBase.OnMouseButtonDown();
    }
}
