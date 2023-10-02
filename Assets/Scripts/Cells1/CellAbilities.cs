using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAbilities : MonoBehaviour {

	[Header("Targeting")]

	public GameObject eye;

	public Vector2 lookDirection;

	public float lookSpeed;

	public GameObject target;

	bool lookMoving;

	[Header("Analysing")]

	public float maxTargetSize;
	public float minTargetSize;

	public bool good;

	[Header("Moving")]

	public float speed;

	[Header("Eating")]

	public bool touchingTarget;

	public float eatSpeed;

	[Header("Others")]

	public Vector2 eyePosition;
	public Vector2 position;

	public float targetDistance;
	public float targetSize;

	public Vector3 size;
	public float sizeMagnitude;

	// Use this for initialization
	void Start () {
		lookSpeed = this.GetComponent<CellGenetic> ().eyeSpeed;

		maxTargetSize = this.GetComponent<CellGenetic> ().maxTargetSize;
		minTargetSize = this.GetComponent<CellGenetic> ().minTargetSize;

		speed = this.GetComponent<CellGenetic> ().speed;

		eatSpeed = this.GetComponent<CellGenetic> ().eatSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Infos

		position = new Vector2 (this.transform.position.x, this.transform.position.y);

		eyePosition = new Vector2 (eye.transform.position.x, eye.transform.position.y);

		lookDirection = new Vector2 ((eyePosition - position).x, (eyePosition - position).y);

		size = this.transform.localScale;
		sizeMagnitude = size.magnitude;

		//----- Targeting

		//Raycasting

		target = null;

		RaycastHit2D hit = Physics2D.Raycast (eye.transform.position, lookDirection);
//		Debug.DrawRay (eye.transform.position, (lookDirection)*100, new Color(1,0,0,1));
		if (hit.collider != null) {
			if (hit.collider.tag == "Entity") {
				target = hit.collider.gameObject;
			}
		}

		//Infos again

		if (target != null) {

			targetDistance = (hit.point - eyePosition).magnitude;

			targetSize = target.transform.localScale.magnitude / this.transform.localScale.magnitude;

		}

		//----- Analysing

		if (target != null) {
			if (target.tag != "Entity" || (target.transform.localScale.magnitude/size.magnitude > maxTargetSize || target.transform.localScale.magnitude/size.magnitude < minTargetSize)) {
				this.transform.Rotate (new Vector3 (0, 0, 1) * lookSpeed);
			}
		} else {
			this.transform.Rotate (new Vector3 (0, 0, 1) * lookSpeed);
		}

		if (target != null) {
			if (target.tag == "Entity") {
				if (targetSize < maxTargetSize && targetSize > minTargetSize) {
					good = true;
				}
				else {
					good = false;
				}
			}
			else {
				good = false;
			}
		} else {
			good = false;
		}

		//----- Moving

		if (good) {
			this.GetComponent<Rigidbody2D> ().AddForce ( (lookDirection.normalized * speed) - this.GetComponent<Rigidbody2D>().velocity ,ForceMode2D.Impulse);
		}

		//----- Eating

		if (target == null) {
			touchingTarget = false;
		}

		if (touchingTarget && target != null && transform.localScale.magnitude < GetComponent<CellReproduction>().maxSize) {
			this.transform.localScale += (target.transform.localScale * eatSpeed);
			if (target.transform.localScale.magnitude > 0) {
				target.transform.localScale -= (target.transform.localScale * eatSpeed);
			}
//			this.GetComponent<CellGenetic>().mass = this.GetComponent<CellGenetic>().mass + (target.GetComponent<CellGenetic>().mass / (1 / eatSpeed)) ;
//			target.GetComponent<CellGenetic>().mass = target.GetComponent<CellGenetic>().mass - (target.GetComponent<CellGenetic>().mass / (1 / eatSpeed)) ;

		}

		if (target != null) {
			if ( targetDistance <= 1) {
				touchingTarget = true;
			} else {
				touchingTarget = false;
			}
		}

	}

//	void OnCollisionStay2D (Collision2D collider) {
//		if (collider.gameObject == target) {
//			touchingTarget = true;
//		} else {
//			touchingTarget = false;
//		}
//	}
//	void OnCollisionExit2D (Collision2D collider) {
//		if (collider.gameObject == target) {
//			touchingTarget = false;
//		}
//	}
}
