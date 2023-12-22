using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private TileBase _occupiedTile;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerColor _playerColor;

    private Stack<PlayerTaskType> playerTasks = new Stack<PlayerTaskType>();

    public PlayerInput PlayerInput => _playerInput;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnStartTurn()
    {
        Debug.Log("OnStartTurn");
        playerTasks.Push(PlayerTaskType.RollDice);
        Invoke(nameof(OnGetTask), 1f);
    }

    public void OnGetTask()
    {
        Debug.Log("OnGetTask");
        PlayerTaskType currentTask = playerTasks.Pop();
        Debug.Log($"Current Task: {currentTask}");
        switch (currentTask)
        {
            case PlayerTaskType.RollDice:
                _playerInput.SetState(new RollDiceState());
                break;
            case PlayerTaskType.BuyProperty:
                _playerInput.SetState(new BuyPropertyState());
                break;
            default:
                break;
        }
    }
    private void OnEndTask()
    {
        Debug.Log("OnEndTask");
        if (playerTasks.Count == 0)
        {
            Invoke(nameof(OnEndTurn), 1f);
        }
        else
        {
            Invoke(nameof(OnGetTask), 1f);
        }
    }

    public void OnEndTurn()
    {
        Debug.Log("OnEndTurn");
        GameplayManager.Instance.ChangePlayer();
    }

    public void MoveWithDice()
    {
        int dice1Value = Random.Range(1, 7);
        int dice2Value = Random.Range(1, 7);
        Debug.Log($"Dice1: {dice1Value}, Dice2: {dice2Value}");
        int value = dice1Value + dice2Value;
        bool isPair = dice1Value == dice2Value;
        if (isPair) playerTasks.Push(PlayerTaskType.RollDice);
        _playerMovement.MoveWithDice(value, _occupiedTile.TileIndex, isPair, SetNewOccuiedTile);
    }

    public void MoveToTile(TileBase tile)
    {
        _playerMovement.MoveToTile(tile);
        SetNewOccuiedTile(tile);
    }

    public void BuyProperty(int level)
    {
        (_occupiedTile as CityTile).CreateProperty(level, _playerColor);
        Invoke(nameof(OnEndTask), 1f);
    }

    public void UpgradeProperty()
    {
        (_occupiedTile as CityTile).UpgradeProperty();
        Invoke(nameof(OnEndTask), 1f);
    }

    public void MoveAround()
    {
        _playerMovement.MoveAround(_occupiedTile.TileIndex, SetNewOccuiedTile);
    }
    private void SetNewOccuiedTile(TileBase tile)
    {
        _occupiedTile = tile;
        _occupiedTile.OnPlayerEnter(this);

        Invoke(nameof(OnEndTask), 1f);
    }
    private void CheckTileStatus(TileBase tile)
    {
        switch (tile.TileType)
        {
            case TileType.CityTile:
                CityTile temp = tile as CityTile;
                if (temp.Property == null)
                    playerTasks.Push(PlayerTaskType.BuyProperty);
                else if (temp.OwnerColor == this._playerColor)
                    playerTasks.Push(PlayerTaskType.UpgradeProperty);
                else
                    playerTasks.Push(PlayerTaskType.PayRent);
                break;
            default:
                return;
        }
    }
}
