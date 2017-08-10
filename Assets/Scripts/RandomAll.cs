using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAll : MonoBehaviour
{
    public Grid grid;
    public CameraCreation creation;
    public int height = 4;
    public Transform PlaneParent;
    public float randomRate = 0.5f;
    public bool randomizeRate = false;

    private List<GameObject> allTiles;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (randomizeRate)
                randomRate = Random.Range(0.1f, 0.5f);
            DestroyAll();
            RandomizeAll();
        }
    }

    void DestroyAll()
    {
        for(int i = 1; i < PlaneParent.childCount; i++)
        {
            Transform temp = PlaneParent.GetChild(i);
            if (temp.GetComponent<BuildingPart>())
            {
                temp.GetComponent<BuildingPart>().DestroyInstantiated();
            }
            Destroy(temp.gameObject);
        }
    }

    void RandomizeAll()
    {
        allTiles = grid.GetAllTiles();
        for (int h = 0; h < height; h++) // height
        {
            for (int i = 0; i < allTiles.Count; i++) // width * length
            {
                float rand = Random.Range(0f, 1f);
                if(rand <= randomRate)
                {
                    Vector3 pos = new Vector3(allTiles[i].transform.position.x, allTiles[i].transform.position.y + 1, allTiles[i].transform.position.z);
                    pos.y += h * 2;
                    Quaternion rot = allTiles[i].transform.rotation;
                    if (height == 0)
                    {
                        creation.CreatePart(pos, rot, h, allTiles[i].GetComponent<Tile>().ID);
                    }
                    else
                    {
                        creation.CreatePart(pos, rot, h);
                    }
                }
            }
        }
    }

}
