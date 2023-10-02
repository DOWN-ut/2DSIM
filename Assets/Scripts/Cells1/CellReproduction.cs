using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellReproduction : MonoBehaviour {

	[Header("----- Reproduction Conditions")]

	public int waitDuration ;
	public int delay;
	[Tooltip("To avoid infinit replication")]
	public int minWait;
	public int minDelay;

	public float maxSize;

	public float bornInitialSpeed;

	public GameObject child;

    [Header("----- Cell's Size")]

    public float sizeX;
    public float sizeY;

    [Header("[][][] Every folowing parameters are used as 'marging of error' for the replication")]

	[Header("----- Cell's Physic Properties")]

	public float mass;

	public float drag ;
	public float angularDrag;

	public float speed;

	public float bouciness;
	public float friction;

	[Header("----- Cell's Abilities")]
	[Header("Targeting")]

	public float eyeSpeed;

	[Header("Analysing")]

	public float targetSize;

	[Header("Eating")]

	public float eatSpeed;

	[Header("Replicating")]

	public float sizeReplication;
	[Tooltip("To avoid infinit replication because of a too small size replication value")]
	public float minSizeReplication;

	[Header("----- Others")]

	public GameObject counter;

	// Use this for initialization
	void Start () {
		delay = 0;
		StartCoroutine (Clock ());
		counter = GameObject.Find ("Counter");
		maxSize = this.GetComponent<CellGenetic> ().sizeReplication;
	}

	//Clock
	IEnumerator Clock() {
		if (delay <= waitDuration) {
			delay++;
		}
		minDelay++;
		yield return new WaitForSecondsRealtime (0.1f);
		StartCoroutine (Clock ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
         
        //Size replication

        if ( Mathf.Abs(transform.localScale.magnitude) >= maxSize || (delay >= waitDuration && minDelay >= minWait) ) {
            if (delay >= waitDuration)
            {
                counter.GetComponent<CounterCells>().timeRep++;
            }
            if ( Mathf.Abs(transform.localScale.magnitude) >= maxSize)
            {
                counter.GetComponent<CounterCells>().sizeRep++;
            }

            child = Instantiate (gameObject, this.transform.position + new Vector3 ((this.transform.localScale*1.1f).x,(this.transform.localScale*1.1f).y,0), new Quaternion (0, 0, 0, 0));

			child.transform.localScale = new Vector3 (child.GetComponent<CellGenetic> ().sizeX, child.GetComponent<CellGenetic> ().sizeY, 1);

//			child.GetComponent<CellGenetic> ().sizeX = Random.Range (child.GetComponent<CellGenetic> ().sizeX - sizeX, child.GetComponent<CellGenetic> ().sizeX);
//			if (child.GetComponent<CellGenetic> ().sizeX <= 0) {
//				child.GetComponent<CellGenetic> ().sizeX = -child.GetComponent<CellGenetic> ().sizeX;
//			}
//			child.GetComponent<CellGenetic> ().sizeY = Random.Range (child.GetComponent<CellGenetic> ().sizeY - sizeY, child.GetComponent<CellGenetic> ().sizeY);
//			if (child.GetComponent<CellGenetic> ().sizeY <= 0) {
//				child.GetComponent<CellGenetic> ().sizeY = -child.GetComponent<CellGenetic> ().sizeY;
//			}

			this.transform.localScale = new Vector3 (this.GetComponent<CellGenetic> ().sizeX, this.GetComponent<CellGenetic> ().sizeY, 1);

			child.GetComponent<CellGenetic> ().mass = Random.Range (child.GetComponent<CellGenetic> ().mass - mass, child.GetComponent<CellGenetic> ().mass + mass);
			if (child.GetComponent<CellGenetic> ().mass <= 0) {
				child.GetComponent<CellGenetic> ().mass = this.GetComponent<CellGenetic> ().mass;
			}
			child.GetComponent<CellGenetic> ().drag = Random.Range (child.GetComponent<CellGenetic> ().drag - drag, child.GetComponent<CellGenetic> ().drag + drag);
			if (child.GetComponent<CellGenetic> ().drag <= 0) {
				child.GetComponent<CellGenetic> ().drag = this.GetComponent<CellGenetic> ().drag;
			}
			child.GetComponent<CellGenetic> ().angularDrag = Random.Range (child.GetComponent<CellGenetic> ().angularDrag - angularDrag, child.GetComponent<CellGenetic> ().angularDrag + angularDrag);
			if (child.GetComponent<CellGenetic> ().angularDrag <= 0) {
				child.GetComponent<CellGenetic> ().angularDrag = this.GetComponent<CellGenetic> ().angularDrag;
			}
			child.GetComponent<CellGenetic> ().speed = Random.Range (child.GetComponent<CellGenetic> ().speed - speed, child.GetComponent<CellGenetic> ().speed + speed);
			child.GetComponent<CellGenetic> ().bouciness = Random.Range (child.GetComponent<CellGenetic> ().bouciness - bouciness, child.GetComponent<CellGenetic> ().bouciness + bouciness);
			if (child.GetComponent<CellGenetic> ().bouciness <= 0) {
				child.GetComponent<CellGenetic> ().bouciness = this.GetComponent<CellGenetic> ().bouciness;
			}
			child.GetComponent<CellGenetic> ().friction = Random.Range (child.GetComponent<CellGenetic> ().friction - friction, child.GetComponent<CellGenetic> ().friction + friction);
			if (child.GetComponent<CellGenetic> ().friction <= 0) {
				child.GetComponent<CellGenetic> ().friction = this.GetComponent<CellGenetic> ().friction;
			}
			child.GetComponent<CellGenetic> ().eyeSpeed = Random.Range (child.GetComponent<CellGenetic> ().eyeSpeed - eyeSpeed, child.GetComponent<CellGenetic> ().eyeSpeed + eyeSpeed);
			child.GetComponent<CellGenetic> ().maxTargetSize = Random.Range (child.GetComponent<CellGenetic> ().maxTargetSize - targetSize, child.GetComponent<CellGenetic> ().maxTargetSize + targetSize);
			child.GetComponent<CellGenetic> ().minTargetSize = Random.Range (child.GetComponent<CellGenetic> ().minTargetSize - targetSize, child.GetComponent<CellGenetic> ().minTargetSize + targetSize);
			child.GetComponent<CellGenetic> ().eatSpeed = Random.Range (child.GetComponent<CellGenetic> ().eatSpeed - eatSpeed, child.GetComponent<CellGenetic> ().eatSpeed + eatSpeed);

//			child.GetComponent<CellGenetic> ().sizeReplication = Random.Range (child.GetComponent<CellGenetic> ().sizeReplication - sizeReplication, child.GetComponent<CellGenetic> ().sizeReplication + sizeReplication);
//			if (child.GetComponent<CellGenetic> ().sizeReplication < minSizeReplication) {
//				child.GetComponent<CellGenetic> ().sizeReplication = minSizeReplication;
//			}

			child.GetComponent<Rigidbody2D> ().velocity = (child.transform.position - this.transform.position).normalized * bornInitialSpeed;

            delay = 0;
			minDelay = 0;

		}

	/*	//Delayed replication

		if (delay >= waitDuration) {

			child = Instantiate (gameObject, this.transform.position + new Vector3 ((this.transform.localScale*1.1f).x,(this.transform.localScale*1.1f).y,0), new Quaternion (0, 0, 0, 0));

			child.transform.localScale = new Vector3 (child.GetComponent<CellGenetic> ().sizeX, child.GetComponent<CellGenetic> ().sizeY, 1);

//			child.GetComponent<CellGenetic> ().sizeX = Random.Range (child.GetComponent<CellGenetic> ().sizeX - sizeX, child.GetComponent<CellGenetic> ().sizeX + sizeX);
//			child.GetComponent<CellGenetic> ().sizeY = Random.Range (child.GetComponent<CellGenetic> ().sizeY - sizeY, child.GetComponent<CellGenetic> ().sizeY + sizeY);

			child.GetComponent<CellGenetic> ().mass = Random.Range (child.GetComponent<CellGenetic> ().mass - mass, child.GetComponent<CellGenetic> ().mass + mass);
			child.GetComponent<CellGenetic> ().drag = Random.Range (child.GetComponent<CellGenetic> ().drag - drag, child.GetComponent<CellGenetic> ().drag + drag);
			child.GetComponent<CellGenetic> ().angularDrag = Random.Range (child.GetComponent<CellGenetic> ().angularDrag - angularDrag, child.GetComponent<CellGenetic> ().angularDrag + angularDrag);
			child.GetComponent<CellGenetic> ().speed = Random.Range (child.GetComponent<CellGenetic> ().speed - speed, child.GetComponent<CellGenetic> ().speed + speed);
			child.GetComponent<CellGenetic> ().bouciness = Random.Range (child.GetComponent<CellGenetic> ().bouciness - bouciness, child.GetComponent<CellGenetic> ().bouciness + bouciness);
			child.GetComponent<CellGenetic> ().friction = Random.Range (child.GetComponent<CellGenetic> ().friction - friction, child.GetComponent<CellGenetic> ().friction + friction);
			child.GetComponent<CellGenetic> ().eyeSpeed = Random.Range (child.GetComponent<CellGenetic> ().eyeSpeed - eyeSpeed, child.GetComponent<CellGenetic> ().eyeSpeed + eyeSpeed);
			child.GetComponent<CellGenetic> ().maxTargetSize = Random.Range (child.GetComponent<CellGenetic> ().maxTargetSize - targetSize, child.GetComponent<CellGenetic> ().maxTargetSize + targetSize);
			child.GetComponent<CellGenetic> ().minTargetSize = Random.Range (child.GetComponent<CellGenetic> ().minTargetSize - targetSize, child.GetComponent<CellGenetic> ().minTargetSize + targetSize);
			child.GetComponent<CellGenetic> ().eatSpeed = Random.Range (child.GetComponent<CellGenetic> ().eatSpeed - eatSpeed, child.GetComponent<CellGenetic> ().eatSpeed + eatSpeed);
		//	child.GetComponent<CellGenetic> ().sizeReplication = Random.Range (child.GetComponent<CellGenetic> ().sizeReplication - sizeReplication, child.GetComponent<CellGenetic> ().sizeReplication + sizeReplication);

			child.GetComponent<Rigidbody2D> ().velocity = (child.transform.position - this.transform.position).normalized * bornInitialSpeed;

			delay = 0;

			counter.GetComponent<CounterCells> ().timeRep++;
		}*/

	}

	void OnTriggerStay2D (Collider2D collider) {
		if (collider.tag == "Entity") {
			delay = 0;
		}
	}

}
