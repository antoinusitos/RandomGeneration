using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{


    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static PlayerManager _instance;

    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
