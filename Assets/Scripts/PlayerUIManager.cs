using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _goHomeButton;
    // Start is called before the first frame update
    void Start()
    {
        _goHomeButton.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadNameScene);
    }

}
