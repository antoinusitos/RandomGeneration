using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFocus;

    private float _speedVertical = 1.0f;
    private float _deceleration = 4.0f;
    private float _speedHorizontal = 0.2f;
    private Transform _transform;
    private Vector2 _lastPos;
    private bool _move = false;

    private float _currentHeight = 0.5f;
    private Vector3 _startPos;
    private Vector3 _targetPos;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startPos = _transform.position;
        _targetPos = _startPos;
    }

    private void Update()
    {
        _transform.LookAt(objectToFocus.position);

        if(Input.GetMouseButtonDown(1))
        {
            _lastPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _move = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _move = false;
        }

        if (_move)
        {
            if (Input.mousePosition.x > _lastPos.x)
            {
                objectToFocus.Rotate(Vector3.up, Time.deltaTime * -_speedHorizontal * (Input.mousePosition.x - _lastPos.x));
            }
            else if (Input.mousePosition.x < _lastPos.x)
            {
                objectToFocus.Rotate(Vector3.up, Time.deltaTime * -_speedHorizontal * (Input.mousePosition.x - _lastPos.x));
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _targetPos = _startPos + new Vector3(0, 10, 0);
            _currentHeight = Mathf.Lerp(0, 2.0f, Time.deltaTime * _speedVertical);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _targetPos = _startPos - new Vector3(0, 10, 0);
            _currentHeight = Mathf.Lerp(0, 2.0f, Time.deltaTime * _speedVertical);
        }
        else
        {
            _targetPos = Vector3.Lerp(_targetPos, _transform.position, Time.deltaTime * _deceleration);
        }

        _transform.position = Vector3.Lerp(_transform.position, _targetPos, _speedVertical * Time.deltaTime);
    }
}
