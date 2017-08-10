using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


// To add a button :
// - Add a Manager in Init
// - Create the Script
// - Create the Delegate at the end of the file


[ExecuteInEditMode]
class MasterManager : EditorWindow
{
    private float _buttonSize = 30.0f;

    private static GameObject _managersParent = null;

    private struct AManager
    {
        public string name;
        public System.Type scriptToAdd;

        public AManager(string newName, System.Type toAdd)
        {
            name = newName;
            scriptToAdd = toAdd;
        }
    }

    private static List<AManager> _allManagers;
    private static List<GameObject> _allManagersInstance;

    [MenuItem("Basic Manager/Master Manager")]

    static void Init()
    {
        if (_allManagers == null)
            _allManagers = new List<AManager>();
        if (_allManagersInstance == null)
            _allManagersInstance = new List<GameObject>();


        //************** ADD MANAGER HERE **************//

        // GAME MANAGER
        AManager gameManager = new AManager("Game Manager", typeof(GameManager));
        _allManagers.Add(gameManager);

        // SOUND MANAGER
        AManager soundManager = new AManager("Sound Manager", typeof(SoundManager));
        _allManagers.Add(soundManager);

        // INPUT MANAGER
        AManager inputManager = new AManager("Input Manager", typeof(InputManager));
        _allManagers.Add(inputManager);

        // SCORE MANAGER
        AManager scoreManager = new AManager("Score Manager", typeof(ScoreManager));
        _allManagers.Add(scoreManager);

        // UI MANAGER
        AManager uiManager = new AManager("UI Manager", typeof(UIManager));
        _allManagers.Add(uiManager);

        // SCREENSHAKE MANAGER
        AManager screenShakeManager = new AManager("ScreenShake Manager", typeof(ScreenShakeManager));
        _allManagers.Add(screenShakeManager);

        // PLAYER MANAGER
        AManager playerManager = new AManager("Player Manager", typeof(PlayerManager));
        _allManagers.Add(playerManager);

        // GAMEPAD MANAGER
        AManager gamepadManager = new AManager("Gamepad Manager", typeof(GamepadManager));
        _allManagers.Add(gamepadManager);

        // INFO MANAGER
        AManager infoManager = new AManager("Info Manager", typeof(InfoManager));
        _allManagers.Add(infoManager);

        // QUEST MANAGER
        AManager questManager = new AManager("Quest Manager", typeof(QuestManager));
        _allManagers.Add(questManager);

        // QUEST MANAGER
        AManager cinematicManager = new AManager("Cinematic Manager", typeof(CinematicManager));
        _allManagers.Add(cinematicManager);

        //************** ADD MANAGER HERE **************//


        // Get existing open window or if none, make a new one:
        MasterManager window = (MasterManager)EditorWindow.GetWindow(typeof(MasterManager));
        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        if(_managersParent == null)
        {
            _managersParent = GameObject.Find("Managers");

            if (_managersParent != null)
            {

                if (_allManagersInstance == null)
                {
                    _allManagersInstance = new List<GameObject>();
                }

                for (int i = 0; i < _managersParent.transform.childCount; i++)
                {
                    _allManagersInstance.Add(_managersParent.transform.GetChild(i).gameObject);
                }
            }
        }

        string f = _managersParent == null ? "not" : "";
        GUILayout.Label("Manager Parent : " + f + " found", EditorStyles.boldLabel);

        GUILayout.Label("CREATION / SELECTION", EditorStyles.boldLabel);

        if (_allManagers == null) return;

        for (int i = 0; i < _allManagers.Count; i++)
        {
            GUILayout.BeginHorizontal();

            GameObject found = null;

            for(int m = 0; m < _allManagersInstance.Count; m++)
            {
                if (_allManagersInstance != null && _allManagersInstance[m].name == _allManagers[i].name)
                {
                    found = _allManagersInstance[m];
                    break;
                }
            }

            if (GUILayout.Button(_allManagers[i].name, GUILayout.Height(_buttonSize)))
            {
                if (!found)
                {
                    CheckParentCreated();
                    CreateNewManager(_allManagers[i]);
                    Debug.Log(_allManagers[i].name + " created.");
                }
                else
                {
                    Selection.activeGameObject = GameObject.Find(_allManagers[i].name);
                }
            }

            if (GUILayout.Button("Remove " + _allManagers[i].name, GUILayout.Height(_buttonSize)))
            {
                if (found)
                {
                    _allManagersInstance.Remove(found);
                    DestroyImmediate(found);
                }
            }

            GUILayout.EndHorizontal();

            if (found)
            {
                GUILayout.TextArea(_allManagers[i].name + " already created. Click to select it...", EditorStyles.centeredGreyMiniLabel);
            }

            GUILayout.Space(10.0f);
        }
    }

    void CheckParentCreated()
    {
        if(_managersParent == null)
        {
            _managersParent = new GameObject("Managers");
        }
    }

    private static void AttachInstanceAndStock(GameObject go)
    {
        go.tag = "Manager";
        _allManagersInstance.Add(go);
        if (_managersParent)
            go.transform.parent = _managersParent.transform;
        else
            Debug.LogError("No Object Managers found as parent for new Manager !");
    }

    static void CreateNewManager(AManager theNewManager)
    {
        GameObject go = new GameObject(theNewManager.name);
        BaseManager m = (BaseManager)go.AddComponent(theNewManager.scriptToAdd);
        m.InitManagerForEditor();
        AttachInstanceAndStock(go);
    }
}