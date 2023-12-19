using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private TileBase _occupiedTile;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void MoveWithDice()
    {
        int dice1Value = Random.Range(1, 7);
        int dice2Value = Random.Range(1, 7);
        Debug.Log($"Dice1: {dice1Value}, Dice2: {dice2Value}");
        int value = dice1Value + dice2Value;
        bool isPair = dice1Value == dice2Value;
        _playerMovement.MoveWithDice(value, _occupiedTile.TileIndex, isPair, SetNewOccuiedTile);
    }

    public void MoveToTile(TileBase tile)
    {
        _playerMovement.MoveToTile(tile);
        SetNewOccuiedTile(tile);
    }
    public void MoveAround()
    {
        _playerMovement.MoveAround(_occupiedTile.TileIndex, SetNewOccuiedTile);
    }
    private void SetNewOccuiedTile(TileBase tile)
    {
        _occupiedTile = tile;
        _occupiedTile.OnPlayerEnter(this);
    }
}
