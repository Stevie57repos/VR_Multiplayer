using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private void Start()
    {
        Instantiate(_prefab); 
    }

}
