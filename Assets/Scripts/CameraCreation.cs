using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCreation : MonoBehaviour
{
    public GameObject buildingPrefab;
    public GameObject TilePrefab;
    public Transform planeParent;

	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.tag == "Tile")
                {
                    Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 1, hit.collider.transform.position.z);
                    Quaternion rot = hit.collider.transform.rotation;
                    CreatePart(pos, rot, 0, hit.collider.GetComponent<Tile>().ID);
                    Destroy(hit.collider);
                }
                else if (hit.collider.tag == "Block")
                {
                    Vector3 pos = hit.collider.transform.position;
                    pos = pos + hit.normal * 2;
                    Quaternion rot = hit.collider.transform.rotation;
                    int idHeight = hit.transform.GetComponent<BuildingPart>().iDHeight + 1;
                    CreatePart(pos, rot, idHeight);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.tag == "Block")
                {
                    if(hit.collider.GetComponent<BuildingPart>().iDHeight == 0)
                    {
                        GameObject go = Instantiate(TilePrefab);
                        go.transform.parent = planeParent.GetChild(0);
                        go.transform.position = new Vector3(hit.transform.position.x, 0.05f, hit.transform.position.z);
                        go.transform.rotation = hit.transform.rotation;
                    }
                    hit.collider.GetComponent<BuildingPart>().CleanStuff();
                    hit.collider.GetComponent<BuildingPart>().DestroyInstantiated();
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void CreatePart(Vector3 pos, Quaternion rot, int idHeight, int ID = -1)
    {
        GameObject go = Instantiate(buildingPrefab);
        go.transform.parent = planeParent;
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.GetComponent<BuildingPart>().iDHeight = idHeight;
        go.GetComponent<BuildingPart>().ID = ID;
        go.GetComponent<ParticleSystem>().Play();
        go.GetComponent<AudioSource>().Play();

    }
}
