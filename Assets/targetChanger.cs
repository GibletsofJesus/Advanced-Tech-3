using UnityEngine;
using System.Collections;

public class targetChanger : MonoBehaviour {
    public Vector3 offset;
    public float newMinDist;
    public void OnMouseDown()
    {
        Camera.main.GetComponent<cameraOrbitControls>().newTarget = transform;
        Camera.main.GetComponent<cameraOrbitControls>().newTargetOffset = offset;
        Camera.main.GetComponent<cameraOrbitControls>().minDistance = newMinDist;
    }
}
