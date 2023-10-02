using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Langton_Manager : MonoBehaviour {

	[Header("Fourmi's actions")]
	public float[] fourmi_step;
	public float[] fourmi_rotation;

	[Header("Time")]
	public int framesTime;
	public int delay;

	[Header("Cases management")]
	public Color[] color;
	public GameObject[] cases;

	[Header("Fourmi")]
	public List<GameObject> fourmis;

	[Header("Show things")]
	public string counterTitle;
	public int number = 0;
	public TextMesh counter;

	// Use this for initialization
	void Start () {
		cases = GameObject.FindGameObjectsWithTag ("Langton_Case");
		foreach (GameObject a in cases) {
			a.GetComponent<Langton_Case> ().visual.color = color [0];
			a.GetComponent<Langton_Case> ().state = 0;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		delay++;
		if (delay >= framesTime) {
            foreach (GameObject a in fourmis)
            {
                a.GetComponent<Langton_Fourmi>().move = true;
            }
			number++;
			counter.text = counterTitle + "\n" + number;
			delay = 0;
		}
	}
}
