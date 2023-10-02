using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour {

	[Header("Objectd to spawn")]

	public GameObject thing;

	public int number;

	[Header("Coordonates limits , x100")]

	public float maxX;
	public float minX;
	public float maxY;
	public float minY;

	[Header("Object Propeties")]

	public float maxMass;
	public float minMass;

	[Header("Object Initial Velocity")]

	public float maxInitialVelocityMagnitude;
	public float minInitialVelocityMagnitude;

    public float maxInitialAngularVelocityMagnitude;
    public float minInitialAngularVelocityMagnitude;

    [Header("Ingame Values")]

	public float X;
	public float Y;

	public int n;

	public GameObject[] obj;

	// Use this for initialization
	void Start () {

		obj = new GameObject[number+1];

		while (n < number) {
			X = Random.Range (minX, maxX);
			Y = Random.Range (minY, maxY);
			obj[n] = Instantiate (thing, new Vector3 (X/100, Y/100, 0), new Quaternion (0, 0, 0, 0));
			obj [n].GetComponent<BasicForce> ().initialVelocity = (new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0)).normalized;
			obj [n].GetComponent<BasicForce> ().useInitialVelocity = true;
			obj [n].GetComponent<BasicForce> ().initialVelocityMagnitude = Random.Range (minInitialVelocityMagnitude, maxInitialVelocityMagnitude);
            obj[n].GetComponent<BasicForce>().initialAngularVelocityMagnitude = Random.Range(minInitialAngularVelocityMagnitude, maxInitialAngularVelocityMagnitude);
            obj[n].GetComponent<BasicForce> ().mass = Random.Range (minMass, maxMass);
			n++;
		}

		n = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
