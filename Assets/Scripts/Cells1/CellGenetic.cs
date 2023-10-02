using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenetic : MonoBehaviour {

	[Header("----- Cell's Physic Properties")]

	public float sizeX;
	public float sizeY;

	public float mass;

	public float drag ;
	public float angularDrag;

	public float speed;

	public float bouciness;
	public float friction;

	[Header("----- Cell's Abilities")]
	[Header("Targeting")]

	public float eyeSpeed;

	[Header("Analysing")]

	public float maxTargetSize;
	public float minTargetSize;

	[Header("Eating")]

	public float eatSpeed;

	[Header("Replicating")]

	public float sizeReplication;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (sizeX, sizeY, 1);
	}
	
	// Update is called once per frame
	void Update () {

		//Physic Properties

		this.GetComponent<Rigidbody2D> ().mass = mass;
		this.GetComponent<Rigidbody2D> ().drag = drag;
		this.GetComponent<Rigidbody2D> ().angularDrag = angularDrag;
		this.GetComponent<CircleCollider2D> ().sharedMaterial = new PhysicsMaterial2D ();
		this.GetComponent<CircleCollider2D> ().sharedMaterial.bounciness = bouciness;
		this.GetComponent<CircleCollider2D> ().sharedMaterial.friction = friction;

	}
}
