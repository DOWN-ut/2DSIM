using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerCells : MonoBehaviour {
	[Header("Objectd to spawn")]

	public GameObject thing;

	public int number;

	[Header("Coordonates limits")]

	public float maxX;
	public float minX;
	public float maxY;
	public float minY;

	[Header("Object Propeties")]

	public float maxMass;
	public float minMass;

	[Header("Ingame Values")]

	public float X;
	public float Y;
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
			Xr = Random.Range (-100, 100);
			Yr = Random.Range (-100, 100);
			obj[n] = Instantiate (thing, new Vector3 (X, Y, 0), new Quaternion (Xr,Yr,0,0));
            if (obj[n].GetComponent<CellGenetic>() != null)
            {
                obj[n].GetComponent<CellGenetic>().mass = Random.Range(minMass, maxMass);
            }
            if (obj[n].GetComponent<Cells2Genetics>() != null)
            {
                int len = Random.Range(10, 90);
                string gene = "" ;
                List<char> cha = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                cha.AddRange(thing.GetComponent<Cells2Genetics>().partNames);
                int i = 0;
                while (i < len)
                {
                    int ran = Random.Range(0, cha.Count - 1);
                    gene += cha[ran];
                    i++;
                }
                obj[n].GetComponent<Cells2Genetics>().genestring = gene;
            }
			n++;
		}

		n = 0;

	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
