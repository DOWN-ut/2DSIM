using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarSpawner : MonoBehaviour {

	[Header("Objectd to spawn")]

	public GameObject thing;

	public int number;

	[Header("Coordonates limits")]

	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	public float maxZ;
	public float minZ;

	[Header("Ingame Values")]

	public float X;
	public float Y;
	public float Z;
	public float Xr;
	public float Yr;

	public int n;

	public GameObject[] obj;

	// Use this for initialization
	void Start () {
		obj = new GameObject[number+1];

		while (n <= number-1) {
			X = Random.Range (minX, maxX);
			Y = Random.Range (minY, maxY);
			Z = Random.Range (minZ, maxZ);
			Xr = Random.Range (-100, 100);
			Yr = Random.Range (-100, 100);
			obj[n] = Instantiate (thing, new Vector3 (X, Y, Z), new Quaternion (Xr,Yr,0,0));
			n++;
		}

		n = 0;
	}
}
