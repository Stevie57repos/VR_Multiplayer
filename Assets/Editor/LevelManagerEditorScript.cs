using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditorScript : Editor
{
    override
    public void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is resposnible for creating and joining rooms", MessageType.Info);

        LevelManager levelManager = (LevelManager)target;

        if (GUILayout.Button("Leave Room"))
        {
            levelManager.LeaveRoomAndLoadNameScene();
        }
    }
}