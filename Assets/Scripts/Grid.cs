using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab;
    private List<GameObject> allTiles;

	void Start ()
    {
        allTiles = new List<GameObject>();

        for (int i = -4; i <= 4; i += 2)
        {
            for (int j = -4; j <= 4; j += 2)
            {
                GameObject go = Instantiate(tilePrefab);
                go.transform.parent = transform;
                go.transform.position = new Vector3(i, 0.05f, j);
                go.GetComponent<Tile>().ID = i * 4 + j;
                allTiles.Add(go);
            }
        }
	}

    public List<GameObject> GetAllTiles()
    {
        return allTiles;
    }
}
