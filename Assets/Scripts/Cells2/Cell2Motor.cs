using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2Motor : MonoBehaviour {

    public float power = 2;

    public Rigidbody2D cent;

	void FixedUpdate ()
    {
        cent.AddForceAtPosition(transform.up * power, transform.position, ForceMode2D.Force);
	}
}
