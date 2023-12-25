using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceSO")]
public class DiceSO : ScriptableObject
{
    [SerializeField] private List<DiceData> _diceList = new List<DiceData>();

    public DiceData GetDiceByIndex(int index)
    {
        return _diceList[index];
    }
}
