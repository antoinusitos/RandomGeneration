using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager 
{

    // Unomment if you want to use Game Flow
    //private bool _inGame = true;

    private void Start()
    {
        // Uncomment if you want to use Game Flow
        //StartCoroutine(GameFlow());
    }

    /*private IEnumerator GameFlow()
    {
        while(_inGame)
        {

            yield return new WaitForEndOfFrame();
        }
    }*/

    public void Win()
    {

    }

    public void Loose()
    {

    }

    public void StartGame()
    {

    }

    public void StartMenu()
    {

    }

    public override void Reset()
    {

    }

    public void Restart()
    {
        StartCoroutine(TimerRestart(3.0f));
    }

    IEnumerator TimerRestart(float time)
    {
        yield return new WaitForSeconds(time);
        StartMenu();
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
