using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ident : MonoBehaviour
{
    public float explosionForce;
    Rigidbody[] rig;
    Transform[] trans;
	ParticleSystem[] ps;
    List<Vector3> startPos = new List<Vector3>();
    List<Transform> transformList = new List<Transform>();
	public Transform explosionPoint;
    void Awake()
    {
		rig = GetComponentsInChildren<Rigidbody>();
		trans = GetComponentsInChildren<Transform>();
		ps = GetComponentsInChildren<ParticleSystem>();
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
			if (transformList[i].gameObject.name != "ident_b" && transformList[i].gameObject.name != "ident_new" && transformList[i].gameObject.name != "ANIMATE ME" )
            transformList[i].rotation = Quaternion.Euler(270, 0, 0);
			else
				transformList[i].rotation = Quaternion.Euler(0, 0, 0);
		}
    }

	public void stopParticles()
	{
		foreach (ParticleSystem patricles in ps)
		{
			patricles.enableEmission = false;
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
			//rigger.AddExplosionForce(explosionForce, Vector3.zero, 50);
			//rigger.AddExplosionForce(explosionForce, Camera.main.transform.position, 50);
			rigger.AddExplosionForce(explosionForce, explosionPoint.position, 70);
		}
		foreach (ParticleSystem patricles in ps)
		{
			patricles.enableEmission = true;
		}
    }
}
