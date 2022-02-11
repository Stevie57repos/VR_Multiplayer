using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VRRoomManager : MonoBehaviourPunCallbacks
{
    private string _mapType;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void JoinRoom()
    {
        _mapType = MultiplayerVRConstants.MAP_TYPE_LEVEL;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomPoperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY, _mapType }
        };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomPoperties, 0);
    }

    private void CreateAndJoinRoom()
    {
        string randomRoom = $"room_{Random.Range(0, 10000)}";
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY, _mapType }
        };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }

    override
    public void OnJoinedLobby()
    {
        print($"Joined the lobby");
    }

    override
    public void OnJoinRandomFailed(short returnCode, string message)
    {
        print(message);
        CreateAndJoinRoom();
    }

    override
    public void OnConnectedToMaster()
    {
        print($"connected to servers again");
        PhotonNetwork.JoinLobby();
    }
    
    override
    public void OnCreatedRoom()
    {
        print($"A room with the name : {PhotonNetwork.CurrentRoom.Name}");
    }

    override
    public void OnJoinedRoom()
    {
        print($"The local player {PhotonNetwork.NickName} has joined " +
            $"{PhotonNetwork.CurrentRoom.Name} which has player count : {PhotonNetwork.CurrentRoom.PlayerCount}");

        PhotonNetwork.LoadLevel("Level");
    }

    override
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        print($"{newPlayer.NickName} has joined the room.");
    }

    // for updating room occupancy count

    //override
    //public void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    if (roomList.Count == 0)
    //    {
    //        // there is no room at all
    //        _occupancySchool.text = $"{0}/{20}";
    //        _occupancyOutdoor.text = $"{0}/{20}";
    //    }

    //    foreach (RoomInfo room in roomList)
    //    {
    //        if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
    //        {
    //            _occupancyOutdoor.text = $"{room.PlayerCount}/20";
    //        }
    //        else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
    //        {
    //            _occupancySchool.text = $"{room.PlayerCount}/20";
    //        }
    //    }
    //}
}
