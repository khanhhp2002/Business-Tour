using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class DiceData
{
    [SerializeField] private Sprite[] _diceImageArray = new Sprite[6];

    public Sprite GetDiceImageByValue(int value)
    {
        return _diceImageArray[value - 1];
    }
}
