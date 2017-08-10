using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager
{
    // Enter
    public bool GetEnterPressed()
    {
        return Input.GetKeyDown(KeyCode.KeypadEnter);
    }

    public bool GetEnterReleased()
    {
        return Input.GetKeyUp(KeyCode.KeypadEnter);
    }

    public bool GetEnterState()
    {
        return Input.GetKey(KeyCode.KeypadEnter);
    }

    // Space Bar
    public bool GetSpaceBarPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetSpaceBarReleased()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public bool GetSpaceBarState()
    {
        return Input.GetKey(KeyCode.Space);
    }

    // Escape
    public bool GetEscapePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static InputManager _instance;

    public static InputManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
