using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle1 : MonoBehaviour {

    [Header("Properties")]

    public bool useLifetime;

    public int lifeTime;

    public float speed;
    public float size;

	public string tags;

    public GameObject toSpawn;

    [Header("Ingame")]

    public Vector3 startSize;

    public int delay;
    public int delay2;

    public GameObject desintegrated1;
	public GameObject desintegrated2;

	public GameObject obj;

	public int self;

	// Use this for initialization
	void Start () {
		self = Random.Range (-1000000, 1000000);
        delay2 = 0;
		StartCoroutine (Clock ());
	}

	//Clock
	IEnumerator Clock() {
		delay++;
		yield return new WaitForSecondsRealtime (0.1f * Time.deltaTime);
		StartCoroutine (Clock ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.localScale = startSize * Mathf.Sqrt(GetComponent<Rigidbody2D>().mass);

		if (delay >= lifeTime && useLifetime) {
			int x=0; int y=0;
			while (x + y == 0) {
				x = Random.Range (-1, 1);
				y = Random.Range (-1, 1);
			}
			desintegrated1 = Instantiate (toSpawn, transform.position + new Vector3 (x*0.1f*transform.localScale.magnitude,y*0.1f*transform.localScale.magnitude, 0), new Quaternion (0, 0, 0, 0));
			desintegrated2 = Instantiate (toSpawn, transform.position + new Vector3 (-x*0.1f* transform.localScale.magnitude, -y*0.1f* transform.localScale.magnitude, 0), new Quaternion (0, 0, 0, 0));
			desintegrated1.GetComponent<BasicForce> ().mass = GetComponent<Rigidbody2D> ().mass / 2f;
			desintegrated2.GetComponent<BasicForce> ().mass = GetComponent<Rigidbody2D> ().mass / 2f;
			desintegrated1.GetComponent<BasicForce> ().useInitialVelocity = false;
			desintegrated2.GetComponent<BasicForce> ().useInitialVelocity = false;
            Vector3 vel1 = (Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * (desintegrated1.transform.position - transform.position).normalized);
            desintegrated1.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (new Vector2(vel1.x,vel1.y)*speed) ;
            Vector3 vel2 = (Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * (desintegrated2.transform.position - transform.position).normalized);
            desintegrated2.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (new Vector2(vel2.x, vel2.y)*speed);
            desintegrated1.GetComponent<Rigidbody2D>().angularVelocity = GetComponent<Rigidbody2D>().angularVelocity / 2f;
            desintegrated2.GetComponent<Rigidbody2D>().angularVelocity = GetComponent<Rigidbody2D>().angularVelocity / 2f;
            desintegrated1.GetComponent<Particle1>().delay = 0;
            desintegrated2.GetComponent<Particle1> ().delay = 0;
			Destroy (gameObject);
		}
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == tags)
        {
            bool doo = false;
            if (GetComponent<Rigidbody2D>().mass > collider.gameObject.GetComponent<Rigidbody2D>().mass)
            {
                doo = true;
            }
            if (GetComponent<Rigidbody2D>().mass == collider.gameObject.GetComponent<Rigidbody2D>().mass && self > collider.gameObject.GetComponent<Particle1>().self)
            {
                doo = true;
            }
            if (doo)
            {
                GetComponent<Rigidbody2D>().mass += collider.gameObject.GetComponent<Rigidbody2D>().mass;
                GetComponent<Rigidbody2D>().velocity += collider.gameObject.GetComponent<Rigidbody2D>().velocity * (collider.gameObject.GetComponent<Rigidbody2D>().mass / GetComponent<Rigidbody2D>().mass);
                GetComponent<Rigidbody2D>().angularVelocity += collider.gameObject.GetComponent<Rigidbody2D>().angularVelocity * (collider.gameObject.GetComponent<Rigidbody2D>().mass / GetComponent<Rigidbody2D>().mass);
                Destroy(collider.gameObject);
            }
        }
    }
}
