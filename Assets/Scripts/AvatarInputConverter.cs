using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{  
    [Header("Avatar Transforms")]
    public Transform MainAvatarTransform;
    public Transform AvatarHead;
    public Transform AvatarBody;
    public Transform AvatarHand_Left;
    public Transform AvatarHand_Right;

    [Header("XR Rig Transforms")]
    [SerializeField]
    private Transform XRHead;
    [SerializeField]
    private Transform XRHand_Left;
    [SerializeField]
    private Transform XRHand_Right;
    
    [Header("Offset Values")]
    [SerializeField]
    private Vector3 headPositionOffset;
    [SerializeField]
    private Vector3 handRotationOffset; 

    void Update()
    {
        //Head and Body synch
        MainAvatarTransform.position = Vector3.Lerp(MainAvatarTransform.position, XRHead.position + headPositionOffset, 0.5f);
        AvatarHead.rotation = Quaternion.Lerp(AvatarHead.rotation, XRHead.rotation, 0.5f);
        AvatarBody.rotation = Quaternion.Lerp(AvatarBody.rotation, Quaternion.Euler(new Vector3(0, AvatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        //Hands synch
        AvatarHand_Right.position = Vector3.Lerp(AvatarHand_Right.position,XRHand_Right.position,0.5f);
        AvatarHand_Right.rotation = Quaternion.Lerp(AvatarHand_Right.rotation,XRHand_Right.rotation,0.5f)*Quaternion.Euler(handRotationOffset);

        AvatarHand_Left.position = Vector3.Lerp(AvatarHand_Left.position,XRHand_Left.position,0.5f);
        AvatarHand_Left.rotation = Quaternion.Lerp(AvatarHand_Left.rotation,XRHand_Left.rotation,0.5f)*Quaternion.Euler(handRotationOffset);
    }
}
