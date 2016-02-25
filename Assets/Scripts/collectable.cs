using UnityEngine;
using System.Collections;

public class collectable : MonoBehaviour {

    public Color colour;
    public Renderer partA, partB;
    public ParticleSystem ps;
    [Range (0,3)]
    public int ID;
    Transform follow;
    bool dropOff;
    Vector3 goHere;
    void Start()
    {
        follow = transform;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Actor" && follow==transform)
        {
            if (!col.GetComponent<Actor>().carrying)
            {
                follow = col.transform;
                col.GetComponent<Actor>().carrying = true;
            }
        }

        if (col.gameObject.name== "DropOffZone")
        {
            GameObject.FindGameObjectWithTag("Base").GetComponent<temple>().collectedbles++;
            dropOff = true;
            switch (ID)
            {
                case 0:
                    goHere = new Vector3(1.5f, 8.25f, 1.5f);
                    break;
                case 1:
                    goHere = new Vector3(-1.5f, 8.25f, 1.5f);
                    break;
                case 2:
                    goHere = new Vector3(-1.5f, 9.5f, -1.5f);
                    break;
                case 3:
                    goHere = new Vector3(1.5f, 9.5f, -1.5f);
                    break;
            }
        }
    }

    void Update ()
    {
        if (follow != transform && !dropOff)
        {
            goHere=follow.position + Vector3.up;
            transform.position = Vector3.Lerp(transform.position, goHere, Time.deltaTime*5);
        }
        else if (dropOff)
        {
            transform.position = Vector3.Lerp(transform.position, goHere, Time.deltaTime * 2.5f);
        }
        partA.material.color = Color.Lerp(colour,Color.white,0.2f);
        partB.material.color = Color.Lerp(colour, Color.white, 0.5f);
        ps.startColor= colour;
    }
}
