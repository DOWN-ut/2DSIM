using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed;

    public Vector3 gravity;

    public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        int r = 0;

        if (Input.GetKey(KeyCode.D))
        {
            r = 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            r = -1;
        }

        Vector3 velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y, 0);

        GetComponent<Rigidbody2D>().AddForce( ( (Quaternion.Euler(0,0,90) * gravity) * speed * r) - velocity, ForceMode2D.Impulse);

        GetComponent<Rigidbody2D>().AddForce(gravity*force, ForceMode2D.Force);

        //transform.rotation = Quaternion.LookRotation(gravity) * Quaternion.Euler(-90, 0, 0);
        //transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z);

    }
}
