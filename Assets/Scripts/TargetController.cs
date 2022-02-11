using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _Renderer;
    [SerializeField]
    private PhotonView _view;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _targetHitSFX;
    
    public void Destroyed()
    {
        _view.RPC("DestroyedRPC", RpcTarget.All); 
    }

    [PunRPC]
    private void DestroyedRPC()
    {
        _audioSource.PlayOneShot(_targetHitSFX);
        StartCoroutine(DestroyedRoutine());
    }

    private IEnumerator DestroyedRoutine()
    {
        _Renderer.enabled = false;
        yield return new WaitForSeconds(2f);
        _Renderer.enabled = true;
    }
}
