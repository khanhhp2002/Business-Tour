using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class EventHandle : PhotonSingleton<EventHandle>, IOnEventCallback
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log($"Event code: {photonEvent.Code}");
        if (photonEvent.Code >= 200) return;
        EventCodes eventCode = (EventCodes)photonEvent.Code;
        object[] data = (object[])photonEvent.CustomData;

        switch (eventCode)
        {
            case EventCodes.ChatMessage:
                ChatMessageReceive(data);
                break;
        }
    }

    public void ChatMessageReceive(object[] data)
    {
        ChatManager.Instance.ReceiveMessage(data);
    }

    public void ChatMessageSend(string message)
    {
        object[] data = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, message };
        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.ChatMessage,
            data,
            new RaiseEventOptions { Receivers = ReceiverGroup.Others },
            new SendOptions { Reliability = true });
    }
}
