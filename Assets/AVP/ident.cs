using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ident : MonoBehaviour
{
    public float explosionForce;
    Rigidbody[] rig;
    Transform[] trans;
    List<Vector3> startPos = new List<Vector3>();
    List<Transform> transformList = new List<Transform>();
    void Awake()
    {
        rig = GetComponentsInChildren<Rigidbody>();
        trans = GetComponentsInChildren<Transform>();
        foreach (Transform pos in trans)
        {
            startPos.Add(pos.position);
            transformList.Add(pos);
        }
    }

    public void Reset()
    {
        foreach (Rigidbody rigger in rig)
        {
            rigger.isKinematic = true;
            rigger.angularVelocity = Vector3.zero;
            rigger.velocity = Vector3.zero;
        }
        for (int i = 0; i < startPos.Count; i++)
        {
            transformList[i].position = startPos[i];
            transformList[i].rotation = Quaternion.Euler(270, 0, 0);
        }
    }

    public void SetExplosionForce(float force)
    {
        explosionForce = force;
    }
    public void explode()
    {
        foreach (Rigidbody rigger in rig)
        {
            rigger.isKinematic = false;
            rigger.AddExplosionForce(explosionForce, Vector3.zero, 50);
        }
    }
}
