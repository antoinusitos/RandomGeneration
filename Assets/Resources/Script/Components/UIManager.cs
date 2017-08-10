using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager
{



    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static UIManager _instance;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
