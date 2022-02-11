using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _XRRig;

    [SerializeField]
    private GameObject _avatarHeadGO;

    [SerializeField]
    private GameObject _avatarBodyGO;

    [SerializeField]
    private AvatarSelectionSO _avatarSO;

    [SerializeField]
    private AvatarInputConverter _avatarInputConverter;

    [SerializeField]
    private GameObject _mainAvatarGO; 

    [SerializeField]
    private bool _isDebugSpawnPlayer;

    void Start()
    {
        if (photonView.IsMine)
        {
            SetUpLocalPlayer();
        }
        else
        {
            SetupNetworkPlayers();
        }
    }

    private void SetUpLocalPlayer()
    {
        _XRRig.SetActive(true);
        CreatePlayerAvatar();
        SetupTeleportAreas();
    }

    private void CreatePlayerAvatar()
    {
        object avatarSelectionNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
        {
            // spawn local player and enter player into buffered list to be spawn for other network players
            photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
        }

        SetLayerRecursively(_avatarHeadGO, 6);
        SetLayerRecursively(_avatarBodyGO, 7);

        _mainAvatarGO.AddComponent<AudioListener>();
    }

    private void SetupTeleportAreas()
    {
        TeleportationArea[] teleportAreas = GameObject.FindObjectsOfType<TeleportationArea>();
        foreach (TeleportationArea area in teleportAreas)
        {
            area.teleportationProvider = _XRRig.GetComponent<TeleportationProvider>();
        }
    }

    private void SetupNetworkPlayers()
    {
        // The player is remote. Disable XR rig so that there are no duplicates in scene
        _XRRig.SetActive(false);

        SetLayerRecursively(_avatarHeadGO, 0);
        SetLayerRecursively(_avatarBodyGO, 0);
    }

    private void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatar = Instantiate(_avatarSO.avatarPrefabList[avatarSelectionNumber], _XRRig.transform);      
        AvatarHolder avatarHolder = selectedAvatar.GetComponent<AvatarHolder>();
        SetUpAvatarGameobject(avatarHolder.HeadTransform, _avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform, _avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, _avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, _avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}
