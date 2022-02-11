using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LevelManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string _homeScene;

    override
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        print($"{newPlayer.NickName} has joined the room. Player count is : {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public void LeaveRoomAndLoadNameScene()
    {
        PhotonNetwork.LeaveRoom();
    }

    override
    public void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    override
    public void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(_homeScene);
    }
}
