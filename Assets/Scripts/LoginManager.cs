using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _playerName;

    [SerializeField]
    private string _sceneName;

    override 
    public void OnConnected()
    {
        print($"I am connected now");
    }

    override
    public void OnConnectedToMaster()
    {
        print($"I'm connected to master server with player name {PhotonNetwork.NickName}");
        PhotonNetwork.LoadLevel(_sceneName);
    }

    public void ConnectAnnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (_playerName != null)
        {
            PhotonNetwork.NickName = _playerName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
