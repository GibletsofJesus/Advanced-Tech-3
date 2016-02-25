using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoWController : MonoBehaviour {

    public float sizeX, sizeY;
    public GameObject fogObject;
	void Start ()
    {
        float fogSize = 5f;
        Vector3 offset = new Vector3((fogSize * sizeX) / 2, 0, (fogSize * sizeY) / 2);
        List<GameObject> allTheFog = new List<GameObject>();
        for (int x=0; x < sizeX;x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject newFog = Instantiate(fogObject) as GameObject;
                newFog.transform.parent = transform;
                newFog.transform.position = new Vector3((fogSize * x)-offset.x, 5, (fogSize * y)-offset.z);
                allTheFog.Add(newFog);
            }
        }
    }
}
