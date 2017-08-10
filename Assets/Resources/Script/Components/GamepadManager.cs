using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadManager : BaseManager
{

    private float _triggerDeadZone = 0.5f;

    public int playerNumber;

    private List<PlayerIndex> _playerIndex;
    private List<GamePadState> _state;
    private List<GamePadState> _prevState;

    private void Start()
    {
        _playerIndex = new List<PlayerIndex>();
        for(int i = 0; i < playerNumber; i++)
        {
            _playerIndex.Add((PlayerIndex)i);
        }

        _state = new List<GamePadState>();
        for(int i = 0; i < playerNumber; i++)
        {
            _state.Add(new GamePadState());
        }
        _prevState = new List<GamePadState>();
        for (int i = 0; i < playerNumber; i++)
        {
            _prevState.Add(new GamePadState());
        }
    }

    void Update()
    {
        for (int i = 0; i < playerNumber; i++)
        {
            _prevState[i] = _state[i];
        }

        for (int i = 0; i < playerNumber; i++)
        {
            _state[i] = GamePad.GetState(_playerIndex[i]);
        }
    }

    public void Vibration(int playerIndex)
    {
        GamePad.SetVibration(_playerIndex[playerIndex], 1.0f, 1.0f);
    }

    public float GetStickPosX(int playerIndex)
    {
        return _state[playerIndex].ThumbSticks.Left.X;
    }

    public float GetStickPosY(int playerIndex)
    {
        return _state[playerIndex].ThumbSticks.Left.Y;
    }

    public bool RightShoulderPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.RightShoulder == ButtonState.Released && _state[playerIndex].Buttons.RightShoulder == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool LeftShoulderPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.LeftShoulder == ButtonState.Released && _state[playerIndex].Buttons.LeftShoulder == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool RightTriggerPressed(int playerIndex)
    {
        return _state[playerIndex].Triggers.Right >= _triggerDeadZone ? true : false;
    }

    public bool LeftTriggerPressed(int playerIndex)
    {
        return _state[playerIndex].Triggers.Left >= _triggerDeadZone ? true : false;
    }

    public bool BButtonPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.B == ButtonState.Released && _state[playerIndex].Buttons.B == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool AButtonPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.A == ButtonState.Released && _state[playerIndex].Buttons.A == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool XButtonPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.X == ButtonState.Released && _state[playerIndex].Buttons.X == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool YButtonPressed(int playerIndex)
    {
        if (_prevState[playerIndex].Buttons.Y == ButtonState.Released && _state[playerIndex].Buttons.Y == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static GamepadManager _instance;

    public static GamepadManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
