using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Langton_Case : MonoBehaviour {

	[Tooltip("Actual state of this case (true for black , false for white)")]
	public int state;

	[Header("Visual")]
	public SpriteRenderer visual;

	[Header("Else")]
	public Langton_Manager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager").GetComponent<Langton_Manager>();
	}

}
