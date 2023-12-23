using UnityEngine;
using Photon.Pun;
using TMPro;
public class Launcher : PhotonSingleton<Launcher>
{
    [SerializeField] private TMP_Text _logStatus;
    // Start is called before the first frame update
    void Start()
    {
        _logStatus.text = "Attempting to connect to server...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _logStatus.text = "Attempting to join lobby...";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _logStatus.text = "Joined lobby.";
    }
}
