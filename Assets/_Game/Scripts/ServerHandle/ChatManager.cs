using Photon.Pun;
using TMPro;
using UnityEngine;

public class ChatManager : Singleton<ChatManager>
{
    [SerializeField] private TMP_InputField _inputChat;
    [SerializeField] private TMP_Text _chatContent;

    private void Start()
    {
        _inputChat.onSubmit.AddListener(SentMessage);
    }
    public void JoinChatBox()
    {
        _chatContent.text += "<color=yellow>You are connected to this client.</color>\n";
    }

    public void LeaveChatBox()
    {
        _chatContent.text = string.Empty;
    }

    public void ReceiveMessage(object[] data)
    {
        _chatContent.text += $"<color=yellow>Player #{(int)data[0]}:</color> {(string)data[1]}\n";
    }

    public void SentMessage(string message)
    {
        if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message)) return;
        if (PhotonNetwork.InRoom)
            EventHandle.Instance.ChatMessageSend(message);
        else
            _chatContent.text += "<color=red>You are discoonected from this client.</color>\n";
        _chatContent.text += $"<color=yellow>You:</color> {message}\n";
        _inputChat.text = string.Empty;
    }
}
