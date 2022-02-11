using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI _occupancySchool;
    [SerializeField]
    private TextMeshProUGUI _occupancyOutdoor;

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

    override
    public void OnJoinedLobby()
    {
        print($"Joined the Lobby");
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    override
    public void OnJoinRandomFailed(short returnCode, string message)
    {
        print(message);
        CreateAndJoinRoom();
    }

    private void CreateAndJoinRoom()
    {
        string randomRoom = $"room_{_mapType}_{Random.Range(0, 10000)}";
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        // We have 2 different maps
        // 1. Outdoor = "outdoor"
        // 2. School = "school"
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY, _mapType}
        };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }

    public void EnterOutdoor()
    {
        _mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomPoperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY, _mapType }
        };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomPoperties, 0);
    }

    public void EnterSchool()
    {
        _mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomPoperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY, _mapType }
        };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomPoperties, 0);
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
            $"{PhotonNetwork.CurrentRoom.Name} of maptype which has player count : {PhotonNetwork.CurrentRoom.PlayerCount}");     
      
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                print($"Joined room with the map type : {mapType}");

                if ((string)_mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
                {
                    // load school scene
                    PhotonNetwork.LoadLevel("World_School");
                }
                else if((string)_mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    // load outdoor scene
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
            }
        }
    }

    override
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        print($"{newPlayer.NickName} has joined the room.");
    }

    override
    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(roomList.Count == 0)
        {
            // there is no room at all
            _occupancySchool.text = $"{0}/{20}";
            _occupancyOutdoor.text = $"{0}/{20}";
        }

        foreach(RoomInfo room in roomList)
        {
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                _occupancyOutdoor.text = $"{room.PlayerCount}/20";
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
            {
                _occupancySchool.text = $"{room.PlayerCount}/20";
            }
        }
    }
}
