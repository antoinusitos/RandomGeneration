using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private bool _canMove = true;

    private int _indexPlayer = 0;

    public bool useGamepad = true;
    public bool useKeyboard = true;

    // Accessor
    private InputManager _inputManager;
    private GamepadManager _gamepadManager;

    private void Start()
    {
        _indexPlayer = GetComponent<PlayerIndexInfo>().playerIndex;
        _inputManager = InputManager.GetInstance();
        _gamepadManager = GamepadManager.GetInstance();
    }

    private void Update()
    {
        if (_canMove)
        {
            if(useGamepad)
            {
                //Debug.Log(_gamepadManager.GetStickPosX(_indexPlayer));
                //Debug.Log(_gamepadManager.GetStickPosY(_indexPlayer));
                if (_gamepadManager.AButtonPressed(_indexPlayer))
                {
                    Debug.Log("A Button Pressed");
                }
                if (_gamepadManager.BButtonPressed(_indexPlayer))
                {
                    Debug.Log("B Button Pressed");
                }
                if (_gamepadManager.XButtonPressed(_indexPlayer))
                {
                    Debug.Log("X Button Pressed");
                }
                if (_gamepadManager.YButtonPressed(_indexPlayer))
                {
                    Debug.Log("Y Button Pressed");
                }
                if (_gamepadManager.LeftTriggerPressed(_indexPlayer))
                {
                    Debug.Log("Left Trigger Pressed");
                }
                if (_gamepadManager.RightTriggerPressed(_indexPlayer))
                {
                    Debug.Log("Right Trigger Pressed");
                }
                if (_gamepadManager.LeftShoulderPressed(_indexPlayer))
                {
                    Debug.Log("Left Shoulder Pressed");
                }
                if (_gamepadManager.RightShoulderPressed(_indexPlayer))
                {
                    Debug.Log("Right Shoulder Pressed");
                }
            }

            if(useKeyboard)
            {
                if (_inputManager.GetSpaceBarPressed())
                {
                    Debug.Log("Space Bar Pressed");
                }
                if (_inputManager.GetSpaceBarReleased())
                {
                    Debug.Log("Space Bar Released");
                }
                if (_inputManager.GetSpaceBarState())
                {
                    Debug.Log("Space Bar Input");
                }
            }
        }
    }
}
