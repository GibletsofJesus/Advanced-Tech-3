using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unitManager : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject sphereCursor;
    public List<GameObject> units = new List<GameObject>();
    public void unitSelection(GameObject unit)
    {
        selectedUnit = unit;
        var UI = GameObject.FindGameObjectWithTag("UIchanger").GetComponent<UIscripting>();
        UI.newCam = unit.GetComponentInChildren<Camera>();
        UI.currentUser = unit.GetComponent<Actor>().thisUser;
        UI.UpdateUI();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
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
