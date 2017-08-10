using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
class AddFinder : EditorWindow
{
    private float _buttonSize = 30.0f;

    private string tempString = "name";

    GameObject toFind;

    [MenuItem("Basic Manager/Object Finder")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        AddFinder window = (AddFinder)EditorWindow.GetWindow(typeof(AddFinder));
        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        GUILayout.BeginHorizontal();
        GUILayout.Label("NAME", EditorStyles.boldLabel);
        tempString = GUILayout.TextField(tempString);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("INSTANCE", EditorStyles.boldLabel);
        toFind = (GameObject)EditorGUI.ObjectField(new Rect(0, 50, position.width - 6, 20), toFind, typeof(GameObject), true);

        GUILayout.EndHorizontal();

        GUILayout.Space(60);

        if (GUILayout.Button("Add Finder !", GUILayout.Height(_buttonSize)))
        {
            ObjectFinder._allFinders.Add(new ObjectFinder.AFinder(tempString, toFind));
            Close();
        }
    }
}