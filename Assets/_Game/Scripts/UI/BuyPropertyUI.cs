using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyPropertyUI : UIBase<BuyPropertyUI>
{
    [SerializeField] private Button _selectLevelOneBuilding;
    [SerializeField] private Button _selectLevelTwoBuilding;
    [SerializeField] private Button _selectLevelThreeBuilding;
    [SerializeField] private Button _selectLevelFourBuilding;
    [SerializeField] private Button _purchase;

    //Event
    public Action<int> onPurchase;
    private int _currentBuildingLevel;

    private void Awake()
    {
        _selectLevelOneBuilding.onClick.AddListener(() => _currentBuildingLevel = 1);
        _selectLevelTwoBuilding.onClick.AddListener(() => _currentBuildingLevel = 2);
        _selectLevelThreeBuilding.onClick.AddListener(() => _currentBuildingLevel = 3);
        _selectLevelFourBuilding.onClick.AddListener(() => _currentBuildingLevel = 4);
        _purchase.onClick.AddListener(() => Purchase());
    }

    private void OnEnable()
    {

    }

    private void Purchase()
    {
        onPurchase?.Invoke(_currentBuildingLevel);
    }
}
