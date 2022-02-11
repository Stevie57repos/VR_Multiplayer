using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VRRoomManager))]
public class VRRoomManagerEditorScript : Editor
{
    override
    public void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is resposnible for creating and joining rooms", MessageType.Info);

        VRRoomManager roomManager = (VRRoomManager)target;

        if (GUILayout.Button("Join Room"))
        {
            roomManager.JoinRoom();
        }
    }
}
