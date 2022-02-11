using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ AvatarSelectionSO")]
public class AvatarSelectionSO : ScriptableObject
{
   public List<GameObject> avatarPrefabList = new List<GameObject> ();
}
