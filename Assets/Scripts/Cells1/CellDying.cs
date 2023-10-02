using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDying : MonoBehaviour {

	public float minSize;

	public GameObject counter;

	// Use this for initialization
	void Start () {
		counter = GameObject.Find ("Counter");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (this.transform.localScale.magnitude < minSize) {
			counter.GetComponent<CounterCells> ().deadCells++;
			Destroy (gameObject);
		}

	}
}
