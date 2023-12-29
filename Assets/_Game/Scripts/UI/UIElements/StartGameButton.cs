using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Sprite _masterClientView;
    [SerializeField] private Sprite _guestView;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _text;
    private void Start()
    {
        _startGameButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound((int)SFXType.ButtonClick);
            Launcher.Instance.StartGame();
        });
        Launcher.Instance.OnJoinRoom += OnJoinRoom;
        Launcher.Instance.OnLeaveRoom += OnLeaveRoom;
    }

    private void OnJoinRoom(string roomName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.enabled = true;
            _background.sprite = _masterClientView;
            _text.text = "Start Game";
        }
        else
        {
            _startGameButton.enabled = false;
            _background.sprite = _guestView;
            _text.text = "Start Game";
        }
    }

    private void OnLeaveRoom()
    {
        _startGameButton.enabled = false;
        _background.sprite = _guestView;
        _text.text = "Start Game";
    }
}
