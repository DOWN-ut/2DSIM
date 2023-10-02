using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

	public float speed;

	public Transform center;

	public Vector3 axis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		this.transform.RotateAround (center.position, axis, Time.deltaTime * speed);

	}
}
