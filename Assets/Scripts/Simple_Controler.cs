using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Controler : MonoBehaviour {

	public float speed;

	public float look;

	public Transform parent;

	public Transform target;

	public Vector3 offset;

	bool done;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null) {
			parent.parent = target;
			if (!done) {
				parent.localPosition = offset;
				done = true;
			}
		} 
		else {
			parent.parent = null;
			done = false;
		}

		if (Input.GetKey (KeyCode.Z)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + transform.forward,Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition - transform.forward,Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + transform.right,Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition - transform.right,Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.E)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + transform.up,Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.Space)) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition - transform.up,Time.deltaTime * speed);
		}

		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X") * look, 0));
		transform.Rotate (new Vector3 (-Input.GetAxis ("Mouse Y") * look, 0, 0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

	}
}
