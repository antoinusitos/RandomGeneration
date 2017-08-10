using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
class ObjectFinder : EditorWindow
{
    private float _buttonSize = 30.0f;

    public struct AFinder
    {
        public string name;
        public GameObject instance;

        public AFinder(string newName, GameObject theInstance)
        {
            name = newName;
            instance = theInstance;
        }
    }

    AddFinder addingWidow;
    GameObject go;
    public static List<AFinder> _allFinders;

    [MenuItem("Basic Manager/Object Finder")]

    static void Init()
    {
        if (_allFinders == null)
            _allFinders = new List<AFinder>();

        // Get existing open window or if none, make a new one:
        ObjectFinder window = (ObjectFinder)EditorWindow.GetWindow(typeof(ObjectFinder));
        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        if (GUILayout.Button("Add Finder...", GUILayout.Height(_buttonSize)))
        {
            if (addingWidow == null)
                addingWidow = ScriptableObject.CreateInstance<AddFinder>();
            addingWidow.Show();
        }

        if (_allFinders == null)
        {
            _allFinders = new List<AFinder>();
            LoadList();
        }

        for (int i = 0; i < _allFinders.Count; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(_allFinders[i].name, EditorStyles.boldLabel);

            if(_allFinders[i].instance == null)
            {
                GUILayout.Label("ERROR, INSTANCE NOT FOUND", EditorStyles.boldLabel);
            }
            else
            {
                if (GUILayout.Button("FIND !", GUILayout.Height(_buttonSize)))
                {
                    Selection.activeGameObject = _allFinders[i].instance;
                    SceneView view = SceneView.lastActiveSceneView;
                    if (view != null)
                    {
                        view.pivot = _allFinders[i].instance.transform.position;
                    }
                }
            }

            if (GUILayout.Button("X", GUILayout.Height(_buttonSize), GUILayout.Width(_buttonSize)))
            {
                _allFinders.RemoveAt(i);
            }

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Finders !", GUILayout.Height(_buttonSize)))
        {
            SaveList();
        }

        if (GUILayout.Button("Load Finders !", GUILayout.Height(_buttonSize)))
        {
            if (_allFinders.Count != 0)
                _allFinders.Clear();
            LoadList();
        }
    }

    void SaveList()
    {
        PlayerPrefs.SetInt("FinderNumber", _allFinders.Count);

        for(int i = 0; i < _allFinders.Count; i++)
        {
            PlayerPrefs.SetString("FinderName"+i, _allFinders[i].name);
            PlayerPrefs.SetInt("FinderID"+i, _allFinders[i].instance.GetInstanceID());
        }

        PlayerPrefs.Save();
    }

    void LoadList()
    {
        int index = PlayerPrefs.GetInt("FinderNumber");

        for (int i = 0; i < index; i++)
        {
            string name = PlayerPrefs.GetString("FinderName" + i);
            int id = PlayerPrefs.GetInt("FinderID" + i);
            GameObject instance = null;


            GameObject[] allGO = GameObject.FindObjectsOfType<GameObject>();
            for(int go = 0; go < allGO.Length; go++)
            {
                if(allGO[go].GetInstanceID() == id)
                {
                    instance = allGO[go];
                    break;
                }
            }

            AFinder F = new AFinder(name, instance);
            _allFinders.Add(F);
        }
    }
}