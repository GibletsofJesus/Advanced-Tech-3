using UnityEngine;
using System.Collections;

public class FoW : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "fogKiller")
            Destroy(gameObject, 0);
    }
}
