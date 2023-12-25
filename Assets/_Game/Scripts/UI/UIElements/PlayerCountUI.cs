using UnityEngine;
using UnityEngine.UI;

public class PlayerCountUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _countSprites;
    [SerializeField] private Image _countImage;

    private void Start()
    {
        Launcher.Instance.OnPlayerCountChanged += OnPlayerCountChanged;
        Launcher.Instance.OnLeaveRoom += OnLeaveRoom;
        Launcher.Instance.OnJoinRoom += OnJoinRoom;
    }

    private void OnPlayerCountChanged(int count)
    {
        if (count > _countSprites.Length)
        {
            Debug.LogError("Player count is too high");
            return;
        }

        _countImage.sprite = _countSprites[count - 1];
    }

    private void OnLeaveRoom()
    {
        _countImage.enabled = false;
    }

    private void OnJoinRoom(string roomName)
    {
        _countImage.enabled = true;
        OnPlayerCountChanged(1);
    }
}
