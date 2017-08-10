using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPart : MonoBehaviour
{
    public int iDHeight = 0;
    public int ID = -1;

    public enum PartType
    {
        CornerA, // 2 sides Front/Right
        CornerB, // 2 sides Front/Left
        CornerC, // 2 sides Back/Right
        CornerD, // 2 sides Back/Left
        CorridorA, // 2 sides Front/Back
        CorridorB, // 2 sides Right/Left
        Single, // 0 side
        Wall, // up and down
        CrossA, // 3 sides Forward Middle
        CrossB, // 3 sides Right Middle
        CrossC, // 3 sides Back Middle
        CrossD, // 3 sides Left Middle
        Open, // 4 sides
        deadEndA, // 1 sides Front
        deadEndB, // 1 sides Right
        deadEndC, // 1 sides Back
        deadEndD, // 1 sides Left
        none,
    }

    public PartType _currentPartType = PartType.none;
    public PartType _lastPartType = PartType.none;

    private Transform _transform;

    private bool mustPlay = true;

    private bool _objectForward;
    private bool _objectRight;
    private bool _objectBack;
    private bool _objectLeft;

    private bool _objectTop;
    private bool _objectBottom;

    private int neighboursCount = 0;

    private float _raycastSize = 1.5f;

    private GameObject _currentPrefab;

    [System.Serializable]
    public struct allBuildingPart
    {
        public PartType _partType;
        public GameObject _partPrefab;
    }

    public List<allBuildingPart> allPart;

    private bool _isIsland = false;
    private float _offsetIsland = 0f;
    private float _speedOffsetIsland = 0.1f;
    private bool _direction = true;
    private float _offsetMax = 0.2f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void CleanStuff()
    {
        if(_currentPrefab != null && _currentPrefab.GetComponent<GenerateRandomStuff>())
            _currentPrefab.GetComponent<GenerateRandomStuff>().CleanStuff();
    }

    void Update()
    {
        CheckNeighbours();
        if(_currentPartType == PartType.Single && _isIsland)
        {
            if(_direction)
            {
                _offsetIsland += Time.deltaTime * _speedOffsetIsland;
                if(_offsetIsland >= _offsetMax)
                {
                    _direction = !_direction;
                }
            }
            else
            {
                _offsetIsland -= Time.deltaTime * _speedOffsetIsland;
                if (_offsetIsland <= -_offsetMax )
                {
                    _direction = !_direction;
                }
            }
            _currentPrefab.transform.position = _transform.position + new Vector3(0, _offsetIsland, 0);
        }
        else
        {
            if(_currentPrefab != null)
                _currentPrefab.transform.position = _transform.position;
        }
    }

    private void CalculateType()
    {
        _lastPartType = _currentPartType;
        bool needChange = false;

        if (_objectTop)
        {
            _currentPartType = PartType.Wall;
            _isIsland = false;
        }
        else if (!_objectBottom && iDHeight != 0 && !_objectTop && !_objectForward && !_objectRight && !_objectLeft && !_objectBack)
        {
            _isIsland = true;
            _currentPartType = PartType.Single;
        }
        else
        {
            if (neighboursCount == 4)
            {
                _isIsland = false;
                _currentPartType = PartType.Open;
            }
            else if (neighboursCount == 3)
            {
                if (_objectForward)
                {
                    if(_objectRight)
                    {
                        if (_objectLeft)
                        {
                            _isIsland = false;
                            _currentPartType = PartType.CrossA;
                        }
                        else if(_objectBack)
                        {
                            _isIsland = false;
                            _currentPartType = PartType.CrossB;
                        }
                    }
                    else if(_objectLeft)
                    {
                        if (_objectBack)
                        {
                            _isIsland = false;
                            _currentPartType = PartType.CrossD;
                        }
                    }
                }
                else
                {
                    _isIsland = false;
                    _currentPartType = PartType.CrossC;
                }
            }
            else if (neighboursCount == 2)
            {
                if (_objectForward && _objectBack)
                {
                    _isIsland = false;
                    _currentPartType = PartType.CorridorA;
                }
                else if (_objectLeft && _objectRight)
                {
                    _isIsland = false;
                    _currentPartType = PartType.CorridorB;
                }
                else
                {
                    if (_objectForward)
                    {
                        if(_objectRight)
                        {
                            _currentPartType = PartType.CornerA;
                            _isIsland = false;
                        }
                        else
                        {
                            _currentPartType = PartType.CornerB;
                            _isIsland = false;
                        }

                    }
                    else
                    {
                        if (_objectRight)
                        {
                            _currentPartType = PartType.CornerC;
                            _isIsland = false;
                        }
                        else
                        {
                            _currentPartType = PartType.CornerD;
                            _isIsland = false;
                        }
                    }
                }
            }
            else if (neighboursCount == 1)
            {
                if (_objectForward)
                {
                    _isIsland = false;
                    _currentPartType = PartType.deadEndA;
                }
                else if(_objectRight)
                {
                    _isIsland = false;
                    _currentPartType = PartType.deadEndB;
                }
                else if(_objectLeft)
                {
                    _isIsland = false;
                    _currentPartType = PartType.deadEndD;
                }
                else if (_objectBack)
                {
                    _isIsland = false;
                    _currentPartType = PartType.deadEndC;
                }
            }
            else
            {
                _isIsland = false;
                _currentPartType = PartType.Single;
            }
        }

        if (_lastPartType != _currentPartType || needChange)
        {
            CleanStuff();
            if (_currentPrefab != null)
            {
                Destroy(_currentPrefab);
            }

            for (int i = 0; i < allPart.Count; i++)
            {
                if (allPart[i]._partType == _currentPartType)
                {
                    _currentPrefab = Instantiate(allPart[i]._partPrefab, _transform.position, _transform.rotation);
                    _currentPrefab.transform.parent = _transform.parent;

                    if (mustPlay)
                    {
                        mustPlay = false;
                        _currentPrefab.GetComponent<Animator>().SetTrigger("Play");
                    }
                    return;
                }
            }
        }
    }

    public void DestroyInstantiated()
    {
        if (_currentPrefab != null)
        {
            Destroy(_currentPrefab);
        }
    }

    public void CheckNeighbours()
    {
        neighboursCount = 0;
        _objectForward = false;
        _objectRight = false;
        _objectBack = false;
        _objectLeft = false;
        _objectBottom = false;
        _objectTop = false;

        if (Physics.Raycast(_transform.position, _transform.forward, _raycastSize))
        {
            _objectForward = true;
            neighboursCount++;
        }

        if (Physics.Raycast(_transform.position, _transform.right, _raycastSize))
        {
            _objectRight = true;
            neighboursCount++;
        }
        if (Physics.Raycast(_transform.position, -_transform.forward, _raycastSize))
        {
            _objectBack = true;
            neighboursCount++;
        }
        if (Physics.Raycast(_transform.position, -_transform.right, _raycastSize))
        {
            _objectLeft = true;
            neighboursCount++;
        }

        if (Physics.Raycast(_transform.position, _transform.up, _raycastSize))
        {
            _objectTop = true;
        }

        if (iDHeight != 0)
        {
            if (Physics.Raycast(_transform.position, -_transform.up, _raycastSize))
            {
                _objectBottom = true;
            }
        }

        CalculateType();
    }
}
