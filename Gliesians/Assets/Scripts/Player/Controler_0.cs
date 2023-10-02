using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler_0 : MonoBehaviour {

	[Header("Move Managing")]

	public float speed ;
	public float runing ;
	public float jump;
    public float speedWhileJump;

	public bool stab ;

	[Header("Look Managing")]

	public Transform cam;
	public float maxY ;
	public float minY ;

    [Header("Ingame Values")]

    public bool canLook;
    public bool canMove;
    public bool canJump;

    public Vector3 moveDirection;
	public float usedSpeed;
    public float speedCoef = 1;

    public Vector2 lookDirection;

	public bool isRunning;
	public bool isWalking ;
    public bool isJumping;

	public float velocityZ;
	public float velocityY;
	public float velocityX;
	public Vector3 velocity;

	// Use this for initialization
	void Start ()
    {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }

	void FixedUpdate ()
    {
		//Run test
		if (Input.GetKey (PlayerPrefs.GetString ("Input.Run")))
        {
			isRunning = true;
		} 
		else {
			isRunning = false;
		}

        //Jump Testing
        RaycastHit hit;
        Ray Rayon = new Ray(transform.position - new Vector3(0, 0.75f, 0), -transform.up);
        if (Physics.Raycast(Rayon, out hit, 0.26f))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }

        //Set variables
        isWalking = false;
		velocityZ = this.GetComponent<Rigidbody> ().velocity.z;
		velocityY = this.GetComponent<Rigidbody> ().velocity.y;
		velocityX = this.GetComponent<Rigidbody> ().velocity.x;
		velocity = this.GetComponent<Rigidbody> ().velocity;

        //Setting move speed
		usedSpeed = speed * speedCoef;
		if (isRunning) {
			usedSpeed *= runing;
		}
        if (isJumping)
        {
            usedSpeed *= speedWhileJump;
        }

        //Look managing
        if (canLook) {
			lookDirection = new Vector2(Input.GetAxis ("Mouse X"), -Input.GetAxis("Mouse Y"));
            Vector2 joy = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (joy.magnitude > 0)
            {
                lookDirection = joy;
            }

            if (lookDirection.magnitude > 0)
            {
				//Look Up/Down
				cam.transform.Rotate (lookDirection.y * PlayerPrefs.GetFloat ("Input.Mouse.SensivityY") * Time.deltaTime, 0, 0);	

				//Look Right/Left
				transform.Rotate (0, lookDirection.x * PlayerPrefs.GetFloat ("Input.Mouse.SensivityX") * Time.deltaTime, 0);	
            }
		}

        //Move managing
        if (canMove)
        {
            //Move Forward
            if (Input.GetKey(PlayerPrefs.GetString("Input.Forward"))) {
                isWalking = true;
                moveDirection = (moveDirection + transform.forward).normalized;
            }
            //Move Backward
            if (Input.GetKey(PlayerPrefs.GetString("Input.Backward"))) {
                isWalking = true;
                moveDirection = (moveDirection - transform.forward).normalized;
            }
            //Move Right
            if (Input.GetKey(PlayerPrefs.GetString("Input.Right"))) {
                isWalking = true;
                moveDirection = (moveDirection + transform.right).normalized;
            }
            //Move Left
            if (Input.GetKey(PlayerPrefs.GetString("Input.Left"))) {
                isWalking = true;
                moveDirection = (moveDirection - transform.right).normalized;
            }
            //Using Joystick
            Vector2 joy = new Vector2(Input.GetAxis("Forward"), Input.GetAxis("Left"));
            if (joy.magnitude > 0)
            {
                isWalking = true;
                moveDirection = joy;
            }

            if (!isJumping)
            {
                GetComponent<Rigidbody>().AddForce(-velocity, ForceMode.VelocityChange);
            }
            if (isWalking)
            {
                if (!isJumping)
                {
                    GetComponent<Rigidbody>().AddForce((moveDirection * usedSpeed), ForceMode.VelocityChange);
                }
                else
                {
                    GetComponent<Rigidbody>().AddForce((moveDirection * usedSpeed)/velocity.magnitude, ForceMode.VelocityChange);
                }
            }
        }

        //Jump managing
        if (canJump)
        {
            if (Input.GetKey(PlayerPrefs.GetString("Input.Jump")) && !isJumping)
            {
                GetComponent<Rigidbody>().velocity += transform.up.normalized * jump;
                isJumping = true;
            }
        }

		//Stabilization
		if (stab) {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}

	}
}
