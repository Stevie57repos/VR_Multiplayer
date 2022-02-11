using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _connectOptionsPanelGO;
    [SerializeField]
    private GameObject _connectWithNamePanelGO;

    void Start()
    {
        _connectOptionsPanelGO.SetActive(true);
        _connectWithNamePanelGO.SetActive(false); 
    }
}
