using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : BaseManager
{
    public Transform cinematicCamera;

    public enum CinematicState
    {
        Playing,
        Stopped,
        Waiting,
        Pause,
    }

    private CinematicState _currentState = CinematicState.Stopped;
    private CinematicState _stateBeforePause;

    [System.Serializable]
    public struct CinematicPoint
    {
        public Transform pointTransform;
        public float waitingTime;
        public float speed;
        public bool moveRotation;
        public bool useTarget;
        public Transform targetCamera;
    }

    public List<CinematicPoint> allPoints;
    private CinematicPoint _lastPoint;
    private CinematicPoint _currentPoint;

    private float _waitingTime = 0f;

    private int _currentIndex = 0;
    private float _currentProgressionPosition = 0;
    private float _currentProgressionRotation = 0;

    private void Start()
    {
        cinematicCamera.gameObject.SetActive(false);
    }

    public void Play()
    {
        if (_currentState != CinematicState.Playing)
        {
            cinematicCamera.gameObject.SetActive(true);
            if (_currentIndex == 0)
            {
                Camera.main.gameObject.SetActive(false);
                _currentPoint = allPoints[_currentIndex];
                cinematicCamera.position = _currentPoint.pointTransform.position;
                cinematicCamera.rotation = _currentPoint.pointTransform.rotation;
                if (allPoints.Count > 0)
                {
                    _lastPoint = allPoints[_currentIndex];
                    _currentIndex = 1;
                    _currentPoint = allPoints[_currentIndex];
                }
            }

            if (_currentState == CinematicState.Waiting)
            {
                _lastPoint = allPoints[_currentIndex];
                if (allPoints.Count > _currentIndex)
                {
                    _currentIndex++;
                    _currentPoint = allPoints[_currentIndex];
                }
            }
            
           _currentState = CinematicState.Playing;
        }
    }

    public void Stop()
    {
        _currentState = CinematicState.Stopped;
        Camera.main.gameObject.SetActive(true);
    }

    public void Pause()
    {
        if(_currentState == CinematicState.Playing || _currentState == CinematicState.Waiting)
        {
            _stateBeforePause = _currentState;
            _currentState = CinematicState.Pause;
        }
        else if(_currentState == CinematicState.Pause)
        {
            _currentState = _stateBeforePause;
        }
    }

    private void Update()
    {
        if (_currentState == CinematicState.Playing)
        {
            _currentProgressionPosition += Time.deltaTime * _currentPoint.speed;
            _currentProgressionRotation += Time.deltaTime * _currentPoint.speed;
            cinematicCamera.position = Vector3.Lerp(_lastPoint.pointTransform.position, _currentPoint.pointTransform.position, _currentProgressionPosition);
            if (_currentPoint.moveRotation)
                cinematicCamera.rotation = Quaternion.Lerp(_lastPoint.pointTransform.rotation, _currentPoint.pointTransform.rotation, _currentProgressionRotation);
            else if (_currentPoint.useTarget && _currentPoint.targetCamera != null)
            {
                cinematicCamera.LookAt(_currentPoint.targetCamera.position);
            }

            if (_currentProgressionPosition >= 0.99f)
            {
                cinematicCamera.position = _currentPoint.pointTransform.position;
                if (_currentPoint.moveRotation)
                    cinematicCamera.rotation = _currentPoint.pointTransform.rotation;
                _currentProgressionPosition = 0.0f;
                _currentProgressionRotation = 0.0f;
                _currentState = CinematicState.Waiting;
            }
        }
        else if (_currentState == CinematicState.Waiting)
        {
            _waitingTime += Time.deltaTime;
            if(_waitingTime >= _currentPoint.waitingTime)
            {
                _waitingTime = 0;
                Play();
            }
        }
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static CinematicManager _instance;

    public static CinematicManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
