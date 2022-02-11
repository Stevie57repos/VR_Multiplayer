using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NerfGunController : MonoBehaviour
{
    [SerializeField]
    private PhotonView _view;
    [SerializeField]
    private Transform _shotOrigin;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _shotAudioFX;
    [SerializeField]
    private float _gunRange = 100f;
    [Range(0.2f,1f)]
    [SerializeField]
    private float _shotRadius = 0.3f;
    public void FireGun()
    {
        _view.RPC("GunFireSFX", RpcTarget.All);
        Ray shotDirection = new Ray(_shotOrigin.position, (_shotOrigin.forward * _gunRange - _shotOrigin.position));
        if (Physics.SphereCast(shotDirection, 0.3f, out var hit))
        {
            if (hit.transform.GetComponent<TargetController>())
            {
                hit.transform.GetComponent<TargetController>().Destroyed();
            }
        }
    }

    [PunRPC]
    private void GunFireSFX()
    {
        _audioSource.PlayOneShot(_shotAudioFX);
    }
}
