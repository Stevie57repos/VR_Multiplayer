using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    private PhotonView _photonView;
    [SerializeField]
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void TransferOwnership()
    {
        _photonView.RequestOwnership();
    }

    public void OnSelectEntered()
    {
        _photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);

        // if we are owner already do not initiate transfer
        if (_photonView.Owner == PhotonNetwork.LocalPlayer) return;
        TransferOwnership();
    }

    public void OnSelectExited()
    {
        _photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
        print($"released");
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != _photonView) return;

        print($"ownership has been requested for {targetView.name} from {requestingPlayer.NickName}");
        photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        print($"ownership has been transfered to {targetView.name} from {previousOwner.NickName}");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {

    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        _rigidBody.isKinematic = true;
        // changing the layer to InHand to prevent other players from taking this object
        var layer = GetComponent<XRGrabInteractable>().interactionLayers = 8;
        
        //print($"{GetComponent<XRGrabInteractable>().interactionLayers.ToString()}");
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        _rigidBody.isKinematic = false;
        // changing the layer back
        GetComponent<XRGrabInteractable>().interactionLayers = 7;
    }
}
