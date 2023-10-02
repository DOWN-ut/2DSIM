using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2 : MonoBehaviour {

    [Header("Properties")]

    public float power;

    public float distanceFallof;

    [Header("Ingame")]

    public GameObject[] influenced;

    Quaternion torque;

    public Vector3 direction;

    public GameObject manager;

    // Use this for initialization
    void Start () {

        manager = GameObject.Find("ForceManager");

        influenced = manager.GetComponent<BasicForce>().influenced;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!manager.GetComponent<BasicForce>().permanentEntities)
        {
            influenced = manager.GetComponent<BasicForce>().influenced;
        }

        torque = Quaternion.AngleAxis( (GetComponent<Rigidbody2D>().angularVelocity/Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity)) * 90, new Vector3(0, 0, 1));

        direction = new Vector3(0, 0,0);
        foreach (GameObject a in influenced)
        {
            if (a != gameObject)
            {
                Vector2 dist = (a.transform.position - transform.position);
                direction += ( (a.GetComponent<Particle2>().torque * dist.normalized) * a.GetComponent<Rigidbody2D>().mass * a.GetComponent<Rigidbody2D>().angularVelocity * a.GetComponent<Particle2>().power) / Mathf.Pow(dist.magnitude, distanceFallof);                
            }
        }

        GetComponent<Rigidbody2D>().AddForce((direction), ForceMode2D.Force);

    }
}
