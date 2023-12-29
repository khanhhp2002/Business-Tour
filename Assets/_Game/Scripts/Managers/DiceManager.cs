using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    [SerializeField] private SpriteRenderer _dice1Sprite;
    [SerializeField] private SpriteRenderer _dice2Sprite;
    [SerializeField] private DiceSO _diceSO;

    private float _rollTimer;
    private DiceData _currentDice;
    private Dictionary<Player, DiceData> _diceDictionary = new Dictionary<Player, DiceData>();

    private void Start()
    {
        OnOffDice(false);
    }
    public void OnOffDice(bool status)
    {
        _dice1Sprite.gameObject.SetActive(status);
        _dice2Sprite.gameObject.SetActive(status);
    }
    public void SetDiceData(int index, Player _player)
    {
        _diceDictionary.Add(_player, _diceSO.GetDiceByIndex(index));
    }
    public void SetValueForDice(int value1, int value2)
    {
        _dice1Sprite.sprite = _currentDice.GetDiceImageByValue(value1);
        _dice2Sprite.sprite = _currentDice.GetDiceImageByValue(value2);

    }
    public void RollAndMove(Player player, Action<bool, int> callback)
    {
        DiceData temp;
        if (_diceDictionary.TryGetValue(player, out temp)) this._currentDice = temp;
        OnOffDice(true);
        _rollTimer = 3f;
        StartCoroutine(SpineDice(callback));
    }
    IEnumerator SpineDice(Action<bool, int> callback)
    {
        int value1 = 0;
        int value2 = 0;
        _dice1Sprite.gameObject.transform.DOShakeRotation(_rollTimer, 100, 50, 90, true, ShakeRandomnessMode.Harmonic);
        _dice2Sprite.gameObject.transform.DOShakeRotation(_rollTimer, 100, 50, 90, true, ShakeRandomnessMode.Harmonic);
        while (_rollTimer >= 0)
        {
            value1 = UnityEngine.Random.Range(1, 7);
            value2 = UnityEngine.Random.Range(1, 7);
            SetValueForDice(value1, value2);
            yield return new WaitForSeconds(0.1f);
            _rollTimer -= 0.1f;
        }
        callback?.Invoke(value1 == value2, value1 + value2);
    }
}
