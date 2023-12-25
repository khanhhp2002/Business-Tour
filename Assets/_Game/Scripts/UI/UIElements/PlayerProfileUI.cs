using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileUI : MonoBehaviour, IPoolable<PlayerProfileUI>
{
    [SerializeField] private Image _rankFrame;
    [SerializeField] private TMP_Text _playerName;
    public int PlayerId;

    public Image RankFrame => _rankFrame;
    public TMP_Text PlayerName => _playerName;

    private Action<PlayerProfileUI> _returnAction;
    public void Initialize(Action<PlayerProfileUI> returnAction)
    {
        _returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(this);
        _playerName.text = string.Empty;
    }
}
