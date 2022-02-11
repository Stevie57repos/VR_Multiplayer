using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VirtualWorldManager))]
public class VirtualWorldManagerEditor : Editor
{
    override
    public void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is resposnible for creating and joining rooms", MessageType.Info);

        VirtualWorldManager roomManager = (VirtualWorldManager)target;

        if (GUILayout.Button("Leave Room"))
        {
            roomManager.LeaveRoomAndLoadNameScene();   
        }
    }
}
