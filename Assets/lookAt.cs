using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
        target.Rotate(new Vector3(0, 180, 0));
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.LookAt(target);
        }
	}
}
