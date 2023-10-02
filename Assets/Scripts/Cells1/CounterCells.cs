using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCells : MonoBehaviour {

	//Alive and dead cells

	public GameObject[] objs;

	public TextMesh textNum ;

	public int deadCells;

	//Eat speed

	public TextMesh textAvEat;
	int u;
	public float avEat;

	//Speed

	public TextMesh textAvSpeed;
	int v;
	public float avSpeed;
	public float minSpeed;
	public float maxSpeed;

	//Size

	public TextMesh textAvSize;
	public int s;
	public float avSize;

	//Size replication

	public TextMesh textSizeRep;
	public int sizeRep;

	//Time replication

	public TextMesh textTimeRep;
	public int timeRep;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Alive and dead cells

		objs = GameObject.FindGameObjectsWithTag ("Entity");

		textNum.text = "CELLS" + "\n" + (objs.Length).ToString () + " alive" + "\n" + deadCells.ToString() + " dead";

		//Eat speed

		for (int i = 0; i < objs.Length; i++) {
			avEat += objs [i].GetComponent<CellAbilities> ().eatSpeed;
			u = i;
		}
		avEat = avEat / u;

		textAvEat.text = "Eat Speed" + "\n" + avEat.ToString () + " av" ;

		//Speed

		minSpeed = avSpeed;
		maxSpeed = avSpeed;
		for (int i = 0; i < objs.Length; i++) {
			avSpeed += objs [i].GetComponent<CellAbilities> ().speed;
			if (objs [i].GetComponent<CellAbilities> ().speed < minSpeed) {
				minSpeed = objs [i].GetComponent<CellAbilities> ().speed;
			}
			if (objs [i].GetComponent<CellAbilities> ().speed > maxSpeed) {
				maxSpeed = objs [i].GetComponent<CellAbilities> ().speed;
			}
			v = i;
		}
		avSpeed = avSpeed / v;
		avSpeed = Mathf.Round (avSpeed * 100); avSpeed = avSpeed / 100;
		maxSpeed = Mathf.Round (maxSpeed * 100); maxSpeed = maxSpeed / 100;
		minSpeed = Mathf.Round (minSpeed * 100); minSpeed = minSpeed / 100;

		textAvSpeed.text = "SPEED" + "\n" + avSpeed.ToString () + " av" + "\n" + maxSpeed.ToString () + " max" + "\n" + minSpeed.ToString() + " min";

		//Size

		for (int i = 0; i < objs.Length; i++) {
			avSize += objs [i].transform.localScale.magnitude;
			s = i;
		}
		avSize = avSize / s;

		textAvSize.text = "SIZE" + "\n" + avSize.ToString () + " av";

		//Replications

		textSizeRep.text = "SIZE" + "\n" + sizeRep.ToString ();
		textTimeRep.text = "TIME" + "\n" + timeRep.ToString ();

	}
}
