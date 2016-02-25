using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unitManager : MonoBehaviour
{
    public Texture2D selectionTexture = null;
    public Rect selection = new Rect(0, 0, 0, 0);
    private Vector3 startClick = -Vector3.one;

    public GameObject selectedUnit;
    public List<GameObject> selectedUnits;
    public GameObject sphereCursor;
    public List<GameObject> units = new List<GameObject>();

    public void unitSelection(GameObject unit)
    {
        for (int i=0;i < selectedUnits.Count;i++)
            selectedUnits[i].GetComponent<Actor>().selected = false;

        selectedUnits.Clear();
        if (selectedUnit)
            selectedUnit.GetComponent<Actor>().selected = false;
        selectedUnit = unit;
    }

    public void addUnitToSelection(GameObject unit)
    {
        selectedUnit = null;
        unit.GetComponent<Actor>().selected = true;
        selectedUnits.Add(unit);
    }

    void checkCamera()
    {
        if (Input.GetMouseButtonDown(0))
            startClick = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
        {
            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }
            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
            startClick = -Vector3.one;
        }

        if (Input.GetMouseButton(0))
            selection = new Rect(startClick.x, (Screen.height - startClick.y), Input.mousePosition.x - startClick.x, (Screen.height - Input.mousePosition.y) - (Screen.height - startClick.y));
        
    }

    void OnGUI()
    {
        if (startClick != -Vector3.one)
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(selection, selectionTexture);
        }
    }

    void Update()
    {
        checkCamera();

        //Move multiple units
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "PlayArea")
                {
                    //ok so we have where we want to go
                    //bad news to all you units, not all of you go go to the same point :(

                    //add an offset to each target, based of the distance between units at the moment
                    List<Vector3> offset = new List<Vector3>();

                    for (int i = 0; i < selectedUnits.Count;i++)
                    {
                        offset.Add(selectedUnits[i].transform.position - selectedUnits[0].transform.position);
                    }
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        GameObject curs = Instantiate(sphereCursor, new Vector3(hit.point.x+ offset[i].x, 0, hit.point.z+ offset[i].z), Quaternion.Euler(Vector3.zero)) as GameObject;
                        Destroy(curs, 1.98f);
                        sphereCursor.transform.position = new Vector3(hit.point.x + offset[i].x, 1.1875f, hit.point.z + offset[i].z);
                        selectedUnits[i].GetComponent<Actor>().target = new Vector3(hit.point.x + offset[i].x, 1.1875f, hit.point.z+offset[i].z);
                    }
                }
            }
        }
        //Move a single unit
        else if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "PlayArea")
                {
                    GameObject curs = Instantiate(sphereCursor, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.Euler(Vector3.zero)) as GameObject;
                    Destroy(curs, 1.98f);
                    sphereCursor.transform.position = new Vector3(hit.point.x, selectedUnit.transform.position.y, hit.point.z);
                    selectedUnit.GetComponent<Actor>().target = new Vector3(hit.point.x, selectedUnit.transform.position.y, hit.point.z);
                }
            }
        }
    }
}
