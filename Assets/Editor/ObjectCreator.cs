using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
class ObjectCreator : EditorWindow
{
    private float _buttonSize = 30.0f;

    private float totalSize = 0;

    private string tempString = "name";
    private int numberToAttach = 0;
    private string finalString = "0";

    public struct CreatedObject
    {
        public string name;
        public List<MonoScript> scriptsToAttach;

        public CreatedObject(string newName)
        {
            name = newName;
            scriptsToAttach = new List<MonoScript>();
        }
    }

    public static List<CreatedObject> allCreatedObjects;

    private List<MonoScript> currentscriptsToAttach = new List<MonoScript>();

    System.Type t;

    [MenuItem("Basic Manager/Object Creator")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ObjectCreator window = (ObjectCreator)EditorWindow.GetWindow(typeof(ObjectCreator));
        if (allCreatedObjects == null)
            allCreatedObjects = new List<CreatedObject>();

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
        GUILayout.Label("ScriptToAttach", EditorStyles.boldLabel);
        string s = GUILayout.TextField(finalString);
        if (s != "")
        {

            int tempNumber = int.Parse(s);

            if (tempNumber > numberToAttach)
            {
                for (int i = 0; i < tempNumber - numberToAttach; i++)
                {
                    currentscriptsToAttach.Add(new MonoScript());
                }
            }
            else if (tempNumber < numberToAttach)
            {
                for (int i = 0; i < tempNumber - numberToAttach; i++)
                {
                    currentscriptsToAttach.RemoveAt(currentscriptsToAttach.Count);
                }
            }

            numberToAttach = tempNumber;
            finalString = numberToAttach.ToString();
        }
        else
        {
            finalString = "";
        }

        GUILayout.EndHorizontal();

        for (int i = 0; i < numberToAttach; i++)
        {
            currentscriptsToAttach[i] = (MonoScript)EditorGUI.ObjectField(new Rect(0, 50 + i * 30, position.width - 6, 20), currentscriptsToAttach[i], typeof(MonoScript), false);
        }

        //t = (System.Type)EditorGUI.ObjectField(new Rect(0, 50 + 3 * 30, position.width - 6, 20), t, typeof(System.Type), false);

        totalSize = 50 + numberToAttach * 30;

        GUILayout.Space(totalSize);

        if (GUILayout.Button("Add Finder !", GUILayout.Height(_buttonSize)))
        {
            CreatedObject co = new CreatedObject(tempString);
            co.scriptsToAttach = new List<MonoScript>();
            for (int i = 0; i < numberToAttach; i++)
            {
               // MonoScript mb =  currentscriptsToAttach[i] as MonoBehaviour;
                co.scriptsToAttach.Add(currentscriptsToAttach[i]);
            }

            allCreatedObjects.Add(co);
            Debug.Log("Object Created !");
        }
    }
}