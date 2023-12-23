using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spRender;

    public SpriteRenderer SpRender { get => _spRender; set => _spRender = value; }
}
