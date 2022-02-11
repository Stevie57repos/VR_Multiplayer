using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvatarSelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _avatarSelectionPlatformGO;

    [SerializeField]
    private AvatarSelectionSO _avatarListSO;
    [SerializeField]
    private List<GameObject> _selectAvatarList;
    [SerializeField]
    private Transform _avatarHolder;
    [SerializeField]
    private Transform _playerAvatarHolder;
    [SerializeField]
    private List<GameObject> _playerAvatarList;

    [SerializeField]
    private int _avatarSelectionNumber = 0;

    [SerializeField]
    private AvatarInputConverter avatarInputConverter;

    private void Awake()
    {
        SpawnAvatars();
    }

    private void Start()
    {
        object storedAvatarSelectionNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out storedAvatarSelectionNumber))
        {
            Debug.Log("Stored avatar selection number: "+ (int)storedAvatarSelectionNumber);
            _avatarSelectionNumber = (int)storedAvatarSelectionNumber;
            ActivateAvatarModelAt(_avatarSelectionNumber);
            LoadAvatarModelAt(_avatarSelectionNumber);
        }
        else
        {
            _avatarSelectionNumber = 0;
            ActivateAvatarModelAt(_avatarSelectionNumber);
            LoadAvatarModelAt(_avatarSelectionNumber);
        }     
    }
    private void SpawnAvatars()
    {
        for(int i = 0; i < _avatarListSO.avatarPrefabList.Count; i++)
        {
            GameObject avatarPlatform = Instantiate(_avatarListSO.avatarPrefabList[i], _avatarHolder);

            AvatarHolder avatarHolder = avatarPlatform.GetComponent<AvatarHolder>();
            
            SetLayerRecursively(avatarHolder.HeadTransform.gameObject, 0);
            SetLayerRecursively(avatarHolder.BodyTransform.gameObject, 0);
            
            _selectAvatarList.Add(avatarPlatform);
        }
        
        for(int i = 0; i < _avatarListSO.avatarPrefabList.Count; i++)
        {
            // player is using default avatar1 at start of game
            GameObject avatarPlayer = Instantiate(_avatarListSO.avatarPrefabList[i], _playerAvatarHolder);
            _playerAvatarList.Add(avatarPlayer);
        }

        //_playerAvatarList.Add()
    }

    private void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public void ActivateAvatarSelectionPlatform()
    {
        _avatarSelectionPlatformGO.SetActive(true);
    }

    public void DeactivateAvatarSelectionPlatform()
    {
        _avatarSelectionPlatformGO.SetActive(false);
    }

    public void NextAvatar()
    {
        _avatarSelectionNumber += 1;
        if (_avatarSelectionNumber >= _selectAvatarList.Count)
        {
            _avatarSelectionNumber = 0;
        }
        ActivateAvatarModelAt(_avatarSelectionNumber);
    }

    public void PreviousAvatar()
    {
        _avatarSelectionNumber -= 1;

        if (_avatarSelectionNumber < 0)
        {
            _avatarSelectionNumber = _selectAvatarList.Count - 1;
        }
        ActivateAvatarModelAt(_avatarSelectionNumber);  
    }

    private void ActivateAvatarModelAt(int avatarIndex)
    {
        foreach (GameObject selectableAvatarModel in _selectAvatarList)
        {
            selectableAvatarModel.SetActive(false);
        }

        _selectAvatarList[avatarIndex].SetActive(true);
        LoadAvatarModelAt(_avatarSelectionNumber);
    }

    private void LoadAvatarModelAt(int avatarIndex)
    {
        foreach (GameObject avatar in _playerAvatarList)
        {
            avatar.SetActive(false);
        }

        _playerAvatarList[avatarIndex].SetActive(true);

        avatarInputConverter.MainAvatarTransform = _playerAvatarList[avatarIndex].GetComponent<AvatarHolder>().MainAvatarTransform;

        avatarInputConverter.AvatarBody = _playerAvatarList[avatarIndex].GetComponent<AvatarHolder>().BodyTransform;
        avatarInputConverter.AvatarHead = _playerAvatarList[avatarIndex].GetComponent<AvatarHolder>().HeadTransform;
        avatarInputConverter.AvatarHand_Left = _playerAvatarList[avatarIndex].GetComponent<AvatarHolder>().HandLeftTransform;
        avatarInputConverter.AvatarHand_Right = _playerAvatarList[avatarIndex].GetComponent<AvatarHolder>().HandRightTransform;

        ExitGames.Client.Photon.Hashtable playerAvatarProp = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, _avatarSelectionNumber } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerAvatarProp);
    }
}