using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : BaseManager
{

    private bool _isShaking = false;
    private float _timeToShake = 0.0f;
    private float _currentTimeShaking = 0.0f;

    private float _magnitude = 0.1f;
    private float _speed = 0.05f;
    private float _currentSpeed = 0.0f;

    private Vector3 _beginPosition = Vector3.zero;
    private GameObject _camera = null;


    public void ShakeForSeconds(float time)
    {
        _timeToShake = time;
        _currentTimeShaking = 0.0f;
        _isShaking = true;
        Shake();
    }

    private void Shake()
    {
        float randomX = Random.Range(-_magnitude, _magnitude);
        float randomY = Random.Range(-_magnitude, _magnitude);
        _camera.transform.position += new Vector3(randomX, randomY, 0.0f);
    }

    private void Update()
    {
        if (_isShaking && _currentTimeShaking < _timeToShake)
        {
            _currentTimeShaking += Time.deltaTime;
            if(_currentTimeShaking >= _timeToShake)
            {
                _isShaking = false;
                _currentTimeShaking = 0.0f;
                _camera.transform.position = _beginPosition;
            }
        }

        if (_isShaking)
        {
            if (_currentSpeed >= _speed)
            {
                _currentSpeed = 0.0f;
                Shake();
            }
            else
            {
                _currentSpeed += Time.deltaTime;
            }
        }
    }

    public bool GetIsShaking()
    {
        return _isShaking;
    }


    private void Awake()
    {
        InitSingleton();
        _camera = Camera.main.gameObject;
        _beginPosition = _camera.transform.position;
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }


    //
    // Singleton Stuff
    // 

    private static ScreenShakeManager _instance;

    public static ScreenShakeManager GetInstance()
    {
        return _instance;
    }

    private void InitSingleton()
    {
        _instance = this;
    }
}
