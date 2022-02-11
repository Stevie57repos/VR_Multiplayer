using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _genericPlayer;
    [SerializeField]
    private Transform _spawn;
    [SerializeField]
    private bool _isDebug;

    void Start()
    {
        if (_isDebug) return;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(_genericPlayer.name, _spawn.position, Quaternion.identity);
        }
    }
}
