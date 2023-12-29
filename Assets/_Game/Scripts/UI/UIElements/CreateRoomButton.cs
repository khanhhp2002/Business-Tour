using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : MonoBehaviour
{
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Sprite _blueBG;
    [SerializeField] private Sprite _grayBG;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _text;

    public Action OnClick;

    private void Start()
    {
        _createRoomButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound((int)SFXType.ButtonClick);
            Launcher.Instance.CreateRoom();
        });
        Launcher.Instance.OnJoinRoom += OnJoinRoom;
        Launcher.Instance.OnLeaveRoom += OnLeaveRoom;
    }

    private void OnLeavePage()
    {
        _background.sprite = _blueBG;
    }

    private void OnEnterPage()
    {
        _background.sprite = _grayBG;
    }

    private void OnJoinRoom(string roomID)
    {
        _createRoomButton.enabled = false;
        _background.sprite = _grayBG;
        _text.text = $"Room: {roomID}";
    }

    private void OnLeaveRoom()
    {
        _createRoomButton.enabled = true;
        _background.sprite = _blueBG;
        _text.text = "Create Room";
    }
}
