using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
class Snap : EditorWindow
{
    private float _buttonSize = 30.0f;

    [MenuItem("Basic Manager/Snap Object")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        Snap window = (Snap)EditorWindow.GetWindow(typeof(Snap));
        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        GameObject selected = Selection.activeGameObject;
        if (GUILayout.Button("SNAP !", GUILayout.Height(_buttonSize)) && selected != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(selected.transform.position, -Vector3.up, out hit))
                selected.transform.position = hit.point;
        }
    }
}