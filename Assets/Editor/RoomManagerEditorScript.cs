using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    override
    public  void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is resposnible for creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;

        if(GUILayout.Button("Join Random Room"))
        {
            roomManager.JoinRandomRoom();
        }

        if(GUILayout.Button("Join School Room"))
        {
            roomManager.EnterSchool();
        }

        if (GUILayout.Button("Join Outdoor Room"))
        {
            roomManager.EnterOutdoor();
        }
    }
}
