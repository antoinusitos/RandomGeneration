using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : BaseManager
{
    private bool _useVsync = false;

    // TO DO : USE DELEGUATES TO MINIMIZE THE WORK

    public void TryReadVSyncValue(string value, string name)
    {
        bool result = false;
        if (bool.TryParse(value, out result))
            SetUseVSync(result);
        else
            Debug.LogError("CANNOT CAST VALUE FOR " + name);
    }

    private void SetUseVSync(bool newState)
    {
        if (_useVsync != newState)
        {
            _useVsync = newState;
            print("Set VSync to " + _useVsync);
        }
    }


    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static InfoManager _instance;

    public static InfoManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
