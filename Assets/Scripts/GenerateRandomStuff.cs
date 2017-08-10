using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomStuff : MonoBehaviour
{
    public GameObject prefabRandom;
    private List<Transform> allRandomPlaces;

    private List<GameObject> _allSpawnedObjects;

    private Transform _transform;

    void Start ()
    {
        _transform = GetComponent<Transform>();
        _allSpawnedObjects = new List<GameObject>();

        allRandomPlaces = new List<Transform>();
        for(int i = 0; i < _transform.childCount; i++)
        {
            if(_transform.GetChild(i).tag == "RandomPlace")
            {
                allRandomPlaces.Add(_transform.GetChild(i));
            }
        }

        GenerateStuff();
    }

    public void GenerateStuff()
    {
        CleanStuff();

        for (int i = 0; i < allRandomPlaces.Count; i++)
        {
            GameObject go = Instantiate(prefabRandom, allRandomPlaces[i].position, allRandomPlaces[i].rotation);
            go.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f);
            _allSpawnedObjects.Add(go);
            go.transform.parent = transform;
        }
    }

    public void CleanStuff()
    {
        for (int i = 0; i < _allSpawnedObjects.Count; i++)
        {
            Destroy(_allSpawnedObjects[i]);
        }
    }

}
