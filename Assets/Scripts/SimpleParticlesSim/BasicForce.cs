using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicForce : MonoBehaviour {

	[Header("Object Type")]

	[Tooltip("Object that is acting in the scene.")]
	public bool isEntity;

	[Tooltip("Not in scene, that manage the general force properties")]
	public bool isManager;

	[Header("----- Entity Options")]
	[Header("Physic Properties")]

	public float mass;
	public float drag;
	public float angularDrag;

	public float bouciness;
	public float friction;

    [Header("Force Properties")]

	public float generatedForce;

	public float receivedForce;

	public float distanceFallof;

	public GameObject[] influenced;

	public string tags ;

	public Vector3 direction;

	[Header("Initials Conditions")]

	public bool useInitialVelocity;

	public Vector3 initialVelocity;
	public float initialVelocityMagnitude;

    public float initialAngularVelocityMagnitude;

    [Header("Manager Getting")]

	public GameObject manager ;

	[Header("----- Manager Options")]

	public float generalForceStrength;
	public float generalDistanceFallof;

    [Tooltip("Will the entities in the scene never dispawn ?")]
    public bool permanentEntities;

    // Use this for initialization
    void Start () {

        if (isEntity)
        {

            //Initial Conditions applaying

            if (useInitialVelocity)
            {
                this.GetComponent<Rigidbody2D>().velocity = initialVelocity.normalized * initialVelocityMagnitude;
                this.GetComponent<Rigidbody2D>().angularVelocity = initialAngularVelocityMagnitude;
            }

            //Physic applying

            this.GetComponent<Rigidbody2D>().mass = mass;
            this.GetComponent<Rigidbody2D>().drag = drag;
            this.GetComponent<Rigidbody2D>().angularDrag = angularDrag;

            this.GetComponent<CircleCollider2D>().sharedMaterial = new PhysicsMaterial2D();
            this.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = bouciness;
            this.GetComponent<CircleCollider2D>().sharedMaterial.friction = friction;

            //Manager getting

            manager = GameObject.Find("ForceManager");
        }

        //Getting other entities

        influenced = GameObject.FindGameObjectsWithTag(tags);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isEntity) {

            //Getting other entities

            if (!manager.GetComponent<BasicForce>().permanentEntities)
            {
                influenced = manager.GetComponent<BasicForce>().influenced;
            }

            //Force direction calculation

            direction = new Vector3 (0,0,0);
            foreach (GameObject a in influenced)
            {
                if (a != gameObject)
                {
                    direction += (((a.transform.position - transform.position).normalized * a.GetComponent<BasicForce>().generatedForce * Mathf.Sqrt(a.GetComponent<Rigidbody2D>().mass)) * (1 / Mathf.Pow((a.transform.position - transform.position).magnitude, (manager.GetComponent<BasicForce>().generalDistanceFallof))));
                }
            }

			//Force applying

			GetComponent<Rigidbody2D> ().AddForce ( (direction.normalized * receivedForce * manager.GetComponent<BasicForce>().generalForceStrength), ForceMode2D.Impulse);
		}

        if (isManager)
        {
            if (!permanentEntities || influenced.Length < 1)
            {
                influenced = GameObject.FindGameObjectsWithTag(tags);
            }
        }

    }
}
