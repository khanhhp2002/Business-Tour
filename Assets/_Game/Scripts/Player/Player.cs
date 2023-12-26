using DG.Tweening;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private TileBase _occupiedTile;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] private SpriteRenderer _dice1Sprite;
    [SerializeField] private SpriteRenderer _dice2Sprite;
    [SerializeField] private DiceSO _diceSO;
    [SerializeField] private DiceData _currentDice;

    private float _rollTimer;
    private Stack<PlayerTaskType> playerTasks = new Stack<PlayerTaskType>();
    private int _money;
    public PlayerMovement PlayerMovement { get => _playerMovement; }
    public PlayerInput PlayerInput => _playerInput;
    public PlayerColor PlayerColor { get => _playerColor; set => _playerColor = value; }
    public int Money { get => _money; set => _money = value; }

    private void Start()
    {
        OnInit();

    }
    private void OnInit()
    {
        Money = 1000000;
        _currentDice = _diceSO.GetDiceByIndex(0);
    }
    public void OnStartTurn()
    {
        Debug.Log("OnStartTurn");
        playerTasks.Push(PlayerTaskType.RollDice);
        if (_occupiedTile.IsTileType(TileType.AirportTile))
            playerTasks.Push(PlayerTaskType.FlyToTile); //ToDo: need to change into FlyToTile in future
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
            case PlayerTaskType.FlyToTile:
                _playerInput.SetState(new FlyToTileState());
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
        SetDiceImage(dice1Value, dice2Value);
        Debug.Log($"Dice1: {dice1Value}, Dice2: {dice2Value}");
        int value = dice1Value + dice2Value;
        bool isPair = dice1Value == dice2Value;
        if (isPair) playerTasks.Push(PlayerTaskType.RollDice);
        PlayerMovement.MoveWithDice(value, _occupiedTile.TileIndex, isPair, SetNewOccuiedTile);
    }
    public void BuyProperty(int level)
    {
        (_occupiedTile as CityTile).CreateProperty(level, PlayerColor);
        Invoke(nameof(OnEndTask), 1f);
    }
    public void UpgradeProperty()
    {
        (_occupiedTile as CityTile).UpgradeProperty();
        Invoke(nameof(OnEndTask), 1f);
    }
    public void SetNewOccuiedTile(TileBase tile)
    {
        CheckTileStatus(tile);
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
                else if (temp.OwnerColor == this.PlayerColor)
                    playerTasks.Push(PlayerTaskType.UpgradeProperty);
                else
                    playerTasks.Push(PlayerTaskType.PayRent);
                break;
            default:
                return;
        }
    }
    private void SetDiceImage(int value1, int value2)
    {
        _dice1Sprite.sprite = _currentDice.GetDiceImageByValue(value1);
        _dice2Sprite.sprite = _currentDice.GetDiceImageByValue(value2);
    }
    public void RollAndMove()
    {
        _rollTimer = 3f;
        StartCoroutine(nameof(SpineDice));
    }
    IEnumerator SpineDice()
    {
        int value1;
        int value2;
        _dice1Sprite.gameObject.transform.DOShakeRotation(_rollTimer, 100, 50, 90, true, ShakeRandomnessMode.Harmonic);
        _dice2Sprite.gameObject.transform.DOShakeRotation(_rollTimer, 100, 50, 90, true, ShakeRandomnessMode.Harmonic);
        while (_rollTimer >= 0)
        {
            value1 = Random.Range(1, 7);
            value2 = Random.Range(1, 7);
            SetDiceImage(value1, value2);
            yield return new WaitForSeconds(0.1f);
            _rollTimer -= 0.1f;
        }
        MoveWithDice();
    }
}
