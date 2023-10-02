using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physicbody : MonoBehaviour {

	[Header("Physic Properties")]

	public float mass ;
	public bool useRigidbodyMass;

    public float drag;
    public float angularDrag = 0.05f;

	public float inertia;

	[Header("Physics")]

	public Vector3 startGravity;
	public float startGravityMagnitude;

	public bool useWorldGravity;

    public Rigidbody[] rigidbods = new Rigidbody[1]; //In the case of a multi rigibody object, like a ragdoll

	[Header("Ingame Properties")]

	public bool useGravity;

	public Vector3 gravity;
	public float gravityMagnitude;

	[Header("Ingame Values")]

	public bool onGravityZone ;
	public Vector3 gravityZoneVector;
    public float gravityCoef = 1;

	// Use this for initialization
	void Start () {

        if (rigidbods.Length > 0)
        {
            if (rigidbods[0] == null)
            {
                rigidbods[0] = GetComponent<Rigidbody>();
            }

            foreach (Rigidbody a in rigidbods)
            {

                if (useRigidbodyMass)
                {
                    mass = a.GetComponent<Rigidbody>().mass;
                }
                a.GetComponent<Rigidbody>().mass = mass / rigidbods.Length;
                a.GetComponent<Rigidbody>().drag = drag;
                a.GetComponent<Rigidbody>().angularDrag = angularDrag;
            }
        }

        gravity = startGravity;
		gravityMagnitude = startGravityMagnitude;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float gravityEffect = 1;
        if (GetComponent<EffectManager>() != null)
        {
            gravityEffect *= GetComponent<EffectManager>().gravity;
        }

		//Differents gravity 
		if (useWorldGravity) {
			gravity = Physics.gravity;
            gravityMagnitude = Physics.gravity.magnitude * gravityCoef * gravityEffect;
        }
		if (onGravityZone) {
			gravity = gravityZoneVector;
            gravityMagnitude = gravityZoneVector.magnitude * gravityCoef * gravityEffect;

        }
		if (!useWorldGravity && !onGravityZone) {
			gravity = startGravity;
            gravityMagnitude = startGravityMagnitude * gravityCoef * gravityEffect;
        }

        float g = gravityMagnitude;
        if(g <= 0.1f * startGravityMagnitude)
        {
            g = 0;
        }

        foreach (Rigidbody a in rigidbods)
        {
            //Disable rigidbody's gravity manager
            a.useGravity = false;

            //Apply gravity
            if (useGravity)
            {
                a.AddForce(gravity.normalized * g, ForceMode.Acceleration);
            }
        }

    }
}
