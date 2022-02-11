using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    public static VirtualWorldManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

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
        PhotonNetwork.LoadLevel("HomeScene");
    }
}
