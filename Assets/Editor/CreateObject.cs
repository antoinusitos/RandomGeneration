using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
class CreateObject : EditorWindow
{
    private float _buttonSize = 30.0f;


    [MenuItem("Basic Manager/Create Object")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CreateObject window = (CreateObject)EditorWindow.GetWindow(typeof(CreateObject));

        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        GUILayout.Label("CREATE OBJECT", EditorStyles.boldLabel);

        if (ObjectCreator.allCreatedObjects != null)
        {
            for (int i = 0; i < ObjectCreator.allCreatedObjects.Count; i++)
            {

                GUILayout.BeginHorizontal();
                GUILayout.Label(ObjectCreator.allCreatedObjects[i].name);
                if (GUILayout.Button("Create !", GUILayout.Height(_buttonSize)))
                {
                    GameObject go = new GameObject(ObjectCreator.allCreatedObjects[i].name);
                    for(int script = 0; script < ObjectCreator.allCreatedObjects[i].scriptsToAttach.Count; script ++)
                    {
                        go.AddComponent(ObjectCreator.allCreatedObjects[i].scriptsToAttach[script].GetClass());
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}