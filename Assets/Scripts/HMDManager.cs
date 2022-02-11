using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HMDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _Player;
    [SerializeField]
    private GameObject _hmd;
    [SerializeField]
    private bool _enableHMD;

    private void Awake()
    {
        _hmd.SetActive(false);
    }

    void Start()
    {
        //print($"xrsettings is {XRSettings.isDeviceActive }");
        //print($"device name is:{XRSettings.loadedDeviceName} ");

        if (_enableHMD)
        {
            _hmd.SetActive(true);
            _Player.transform.position = new Vector3(_Player.transform.position.x, _Player.transform.position.y + 2f, _Player.transform.position.z);
        }


        if (!XRSettings.isDeviceActive)
        {
            print($"No headset plugged");

        }
        else if (XRSettings.isDeviceActive && XRSettings.loadedDeviceName == "Mock HMD" 
            || XRSettings.loadedDeviceName == "MockHMD Display")
        {
            print($"Using Mock HMD");
            _hmd.SetActive(true);
            _Player.transform.position = new Vector3(_Player.transform.position.x, _Player.transform.position.y + 2f, _Player.transform.position.z);
        }
        else
        {
            print($"We have a headset : {XRSettings.loadedDeviceName}");
        }
    }

}
