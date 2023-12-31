using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicForce3D : MonoBehaviour {

	[Header("Object Type")]

	[Tooltip("Object that is acting in the scene")]
	public bool isEntity;

	[Tooltip("Not in scene object, that manage the general force properties")]
	public bool isManager;

	[Header("----- Entity Options")]
	[Header("Physic Properties")]

	public float mass;
	public float drag;
	public float angularDrag;

	[Header("Others Properties")]

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

	[Header("Manager Getting")]

	public GameObject manager ;


	[Header("----- Manager Options")]

	public float generalForceStrength;
	public float generalDistanceFallof;

	// Use this for initialization
	void Start () {

		if (isEntity) {

			//Initial Conditions applaying

			if (useInitialVelocity) {
				this.GetComponent<Rigidbody> ().velocity = initialVelocity.normalized * initialVelocityMagnitude;
			}

			//Force parameters

			influenced = GameObject.FindGameObjectsWithTag (tags);

			//Physic applying

			this.GetComponent<Rigidbody> ().mass = mass;
			this.GetComponent<Rigidbody> ().drag = drag;
			this.GetComponent<Rigidbody> ().angularDrag = angularDrag;

	//		this.GetComponent<Collider> ().sharedMaterial = new PhysicMaterial ();
	//		this.GetComponent<Collider> ().sharedMaterial.bounciness = bouciness;
	//		this.GetComponent<Collider> ().sharedMaterial.dynamicFriction = friction;
			
			//Manager getting

			manager = GameObject.Find ("Manager");

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isEntity) {

			//Force direction calculation

			direction = new Vector3 (0,0,0);

			foreach (GameObject a in influenced) {
				Vector3 dir = a.transform.position - this.transform.position ;
				direction += ((dir.normalized * a.GetComponent<BasicForce3D> ().generatedForce) * (1/Mathf.Pow (Mathf.Abs(dir.magnitude + 0.00001f), (distanceFallof *  manager.GetComponent<BasicForce3D>().generalDistanceFallof))));
			}

			//Force applying

			this.GetComponent<Rigidbody> ().AddForce (direction.normalized * receivedForce * manager.GetComponent<BasicForce3D>().generalForceStrength, ForceMode.Force);
		}

	}
}
