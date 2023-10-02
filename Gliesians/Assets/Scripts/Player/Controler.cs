using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Input;

public class Controler : MonoBehaviour
{
    [Header("Move Managing")]

    public float speed;
    public float runing;
    public int timeBeforeRun;
    public float speedAim;
    public float jump;
    public float speedWhileJump;
    public float dodge;
    public float dodgeJump;

    public bool stab;

    public Transform foot;

    [Header("Look Managing")]

    public float lookSpeedTime = 1 / 1.7f;//A power use to manage look speed during slow motion
    public Transform cam;
    public Transform camMover;
    public float maxY;
    public float minY;
    public float cameraZoom;

    public Camera[] mainCameras;

    [Header("Ingame Values")] 

    public bool canLook;
    public bool canMove;
    public bool canJump;
    public bool canJump2;
    public bool canAim;
    public bool canReload;

    public Vector3 moveDirection;
    public float usedSpeed;
    public float speedCoef = 1;
    public float zoomCoef = 1;

    public Vector2 lookDirection;
    public float moveAngle;
    public float actZoom;

    public bool shooting;
    public bool aim;
    public int equipementIndex = 3;
    public bool reloading;
    public bool inventoring;
    public bool quickActioning;

    public bool timing;
    public bool ability1;
    public bool ability2;
    public bool ability3;
    public bool ability4;
    public bool jump2ing;

    public bool isRunning;
    public bool isWalking;
    public bool isJumping;
    public bool dashed;

    public bool aware = true; //The player is not aware when, for instance, he was stuned by an ennemy, or pushed by an explosion. When he is aware, on the ground and doesn't wants to move, the script keeps its speed to 0 to avoid slide on slopes. 

    public Vector3 horizontalVelocity;

    int runDelay;
    int dashPhase;
    int dashDelay;
    int dashCool;

    int abiD;
    bool abi1; //Temp variables to store abilities's keys state
    bool abi2;
    bool abi1dual;
    bool abi2dual;

    [Header("Others")]

    public Camera HUDcamera; 

    public Transform crosshair;

    public bool oldInputSystem;
    public PlayerControls controls;

    public LayerMask jumpLayer;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        cameraZoom = mainCameras[0].GetComponent<Camera>().fieldOfView;
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        if (isWalking)
        {
            runDelay++;
        }
        else
        {
            runDelay = 0;
        }

        /*
        dashDelay++;
        dashCooldown++;
        if ( (dashed && dashCool >= dashCooldown) || !isJumping || GetComponent<Physicbody>().useGravity)
        {
            dashCool = 0;
            dashed = false;
        }
        */

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Clock());
    }

    void Awake()
    {
        if (!oldInputSystem)
        {
            
            controls = new PlayerControls();

            controls.Player.Movement.performed += mov => M(mov.ReadValue<Vector2>());
            controls.Player.Movement.canceled += mov1 => M(Vector2.zero);

            controls.Player.Look.performed += lok => L(lok.ReadValue<Vector2>());
            controls.Player.Look.canceled += lok1 => L(Vector2.zero);

            controls.Player.Jump.performed += jup => Jump();

            controls.Player.Jump2.performed += gra => Jump2();

            controls.Player.Time.performed += tim => Trigger();

            controls.Player.Shot.performed += sho => Shoot();
            controls.Player.Aim.performed += aim => Aim();

            controls.Player.Switch.performed += sw => Switch((int)(sw.ReadValue<Vector2>().normalized.y));
            controls.Player.Next.performed += nx => Switch(1);
            controls.Player.Previous.performed += pr => Switch(-1);
            controls.Player.Reload.performed += re => Reload();

            controls.Player.Ability1.performed += ab1 => Ability1();
            controls.Player.Ability2.performed += ab2 => Ability2();
            controls.Player.Ability3.performed += ab3 => Ability3();
            controls.Player.Ability4.performed += ab4 => Ability4();

            controls.Player.Action.performed += act => Action();
            controls.Player.Inventory.performed += ivn => Inventory();
            
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Trigger()
    {
        timing = !timing;
    }

    private void OnEnable()
    {
        if (!oldInputSystem)
        {
            controls.Enable();
        }
    }
    private void OnDisable()
    {
        if (!oldInputSystem)
        {
            controls.Disable();
        }
    }

    void Jump()
    {
        if (canJump && !isJumping && GetComponent<Physicbody>().gravityMagnitude > 0.05f)
        {
            float angle = Vector3.Dot(moveDirection.normalized, transform.forward.normalized);
            if (angle > 0.65f || !isWalking)
            {
                GetComponent<Rigidbody>().velocity += transform.up.normalized * jump;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = moveDirection.normalized * dodge;
                GetComponent<Rigidbody>().velocity += transform.up.normalized * dodgeJump;
                GetComponent<Collider>().sharedMaterial.dynamicFriction = 0;
                GetComponent<Collider>().sharedMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
            }
            isJumping = true;
        }
    }

    void Jump2()
    {
        if (canJump2)
        {
            jump2ing = !jump2ing;
        }
        else
        {
            jump2ing = false;
        }
    }

    public void Look(Vector2 direction)
    {
        float lookSpeed = Time.deltaTime / Mathf.Pow(Time.timeScale, lookSpeedTime);
        if (GetComponent<PlayerClass>().actWeapon < GetComponent<PlayerClass>().weapons.Count && aim)
        {
            lookSpeed /= GetComponent<PlayerClass>().weapons[GetComponent<PlayerClass>().actWeapon].GetComponent<AbilityManager>().aimZoom;
        }

        camMover.transform.Rotate(0, -direction.y * PlayerPrefs.GetFloat("Input.Mouse.SensivityY") * lookSpeed, 0);

        transform.Rotate(0, direction.x * PlayerPrefs.GetFloat("Input.Mouse.SensivityX") * lookSpeed, 0);
    }

    public void Move(Vector3 direction)
    {
        Vector3 goal = direction * usedSpeed ;
        goal = new Vector3(goal.x, GetComponent<Rigidbody>().velocity.y, goal.z);

        if (GetComponent<Physicbody>().useGravity)
        {
            if (!isJumping)
            {
                GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity, goal, Time.deltaTime * Mathf.Abs((goal - GetComponent<Rigidbody>().velocity).magnitude) * 10);
            }
            else
            {
                //GetComponent<Rigidbody>().velocity += new Vector3(goal.x / (GetComponent<Rigidbody>().velocity.magnitude + 0.1f), 0, goal.z / (GetComponent<Rigidbody>().velocity.magnitude + 0.1f));
                GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity,new Vector3( goal.x, GetComponent<Rigidbody>().velocity.y,goal.z), Time.deltaTime * speedWhileJump * GetComponent<EffectManager>().midairspeedBoost * (1+Mathf.Abs( GetComponent<Rigidbody>().velocity.y)));
            }
        }
    }

    /*
    public void Dash()
    {
        if (moveDirection.magnitude > 0.5f && dashPhase == 0 && !GetComponent<Physicbody>().useGravity)
        {
            dashPhase = 1;
            dashDelay = 0;
        }
        if (moveDirection.magnitude <= 0.01f && dashPhase == 1 && !GetComponent<Physicbody>().useGravity)
        {
            dashPhase = 2;
            dashDelay = 0;
        }

        Vector3 d = moveDirection;
        float angle = Vector3.Dot(horizontalVelocity.normalized, d.normalized);
        if (!dashed || angle < 0 || horizontalVelocity.magnitude < 0.1f)
        {
            if (moveDirection.magnitude > 0.5f && dashPhase == 2 && !GetComponent<Physicbody>().useGravity)
            {
                dashPhase = 3;
                if (angle < 0 && horizontalVelocity.magnitude > 0.1f)
                {
                    d = Vector3.zero;
                }
                d = new Vector3(d.x * dash, GetComponent<Rigidbody>().velocity.y, d.z * dash);
                GetComponent<Rigidbody>().velocity = d;
                dashed = true;
                dashCool = 0;
            }
        }

        if ((moveDirection.magnitude <= 0.01f && dashPhase == 3) || GetComponent<Physicbody>().useGravity || dashDelay > 1)
        {
            dashPhase = 0;
            dashDelay = 0;
        }
    }
    */

    public void Inputs ()
    {
        moveDirection = Vector3.zero;
        if (Input.GetAxis("MoveLR") > 0.01f || Input.GetAxis("MoveFB") > 0.01f)
        {
            moveDirection = new Vector3(Input.GetAxis("MoveLR"), 0, -Input.GetAxis("MoveFB"));
            moveDirection = (transform.forward * moveDirection.z) + (transform.right * moveDirection.x);
            moveDirection.y = 0;
        }
        lookDirection = Vector2.zero;
        if (Input.GetAxis("LookLR") > 0.01f || Input.GetAxis("LookFB") > 0.01f)
        {
            lookDirection = new Vector2(Input.GetAxis("LookLR"), -Input.GetAxis("LookFB"));
        } 
    }

    private void Update()
    {
        //ability3 = false;

        if (oldInputSystem)
        {
            Inputs();
        }

        //Applying animations

        GetComponent<PlayerClass>().animator.SetBool("LegWalk", isWalking);

        Move(moveDirection);
        Look(lookDirection);
        //Getting some values
        horizontalVelocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
        moveAngle = Vector3.Dot(horizontalVelocity.normalized, transform.forward.normalized); //Scalar product betweem forward and moving direction. The player needs to run only if he moves quite forward

        //Avoid sliding 
        if (isWalking)
        {
            GetComponent<Collider>().sharedMaterial.dynamicFriction = 0;
            GetComponent<Collider>().sharedMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        }
        else
        {
            GetComponent<Collider>().sharedMaterial.dynamicFriction = 1;
            GetComponent<Collider>().sharedMaterial.frictionCombine = PhysicMaterialCombine.Maximum;
        }

        //Run test
        if (runDelay > timeBeforeRun && moveAngle > 0.75f && !aim)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //Jump Testing
        Vector3 feet = foot.position;
        Collider[] walkOn = Physics.OverlapBox(feet, new Vector3(0.1f, 0.1f, 0.1f),transform.rotation,jumpLayer);
        isJumping = true;
        foreach (Collider a in walkOn)
        {
            if (a.GetComponent<ObjectInfos>() != null)
            {
                if (a.GetComponent<ObjectInfos>().walkable)
                {
                    isJumping = false;
                }
            }
            if (a.tag == "Decor")
            {
                isJumping = false;               
            }
            //print(a.name);
        }
        /*
        RaycastHit hit;
        Ray Rayon = new Ray(feet, -transform.up);
        if (Physics.Raycast(Rayon, out hit, 0.26f))
        {
            isJumping = false;
            GetComponent<Physicbody>().useGravity = true; //Re activates gravity if the player is on the ground.
        }
        else
        {
            isJumping = true;
        }
        */

        //Setting move speed
        usedSpeed = speed * speedCoef * GetComponent<EffectManager>().speedBoost;
        if (isRunning)
        {
            usedSpeed *= runing;
        }
        if (aim && GetComponent<PlayerClass>().actWeapon < GetComponent<PlayerClass>().weapons.Count - 1)
        {
            usedSpeed *= GetComponent<PlayerClass>().weapons[GetComponent<PlayerClass>().actWeapon].GetComponent<AbilityManager>().aimSpeed;
        }
        /*
        if (isJumping)
        {
            usedSpeed *= speedWhileJump;
        }
        */

        //Setting camera zoom
        actZoom = cameraZoom / zoomCoef;
        float newF = mainCameras[0].fieldOfView;
        if (mainCameras[0].fieldOfView > actZoom)
        {
            newF /= (float)(1 + ((Time.deltaTime / (float)Time.timeScale) * Mathf.Abs(mainCameras[0].fieldOfView - actZoom) * 0.2f));
            if (newF >= actZoom)
            {
                foreach (Camera ca in mainCameras)
                {
                    ca.fieldOfView = newF;
                }
            }
            else
            {
                foreach (Camera ca in mainCameras)
                {
                    ca.fieldOfView = actZoom;
                }
            }
        }
        else if (mainCameras[0].fieldOfView < actZoom)
        {
            newF *= (float)(1 + ( (Time.deltaTime / (float)Time.timeScale) * Mathf.Abs(mainCameras[0].fieldOfView - actZoom) * 0.2f ));
            if (newF <= actZoom)
            {
                foreach (Camera ca in mainCameras)
                {
                    ca.fieldOfView = newF;
                }
            }
            else
            {
                foreach (Camera ca in mainCameras)
                {
                    ca.fieldOfView = actZoom;
                }
            }
        }

        //Stabilization
        if (stab)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        //Abilities
            /*
        if (abiD > 1)
        {
            if (abi1 && abi2)
            {
                ability3 = true;
                abi1dual = true;
                abi2dual = true;
            }
            else
            {
                if (abi1)
                {
                    ability1 = true;
                }
                if (abi2)
                {
                    ability2 = true;
                }
            }
            abi1 = false;
            abi2 = false;
        }
        abiD++;
            */
    }

    void M(Vector2 direction)
    {
        //print(direction);
        moveDirection = (transform.forward * direction.y) + (transform.right * direction.x); 
        if (direction.magnitude > 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void L(Vector2 direction)
    {
        //print(direction);
        lookDirection = new Vector2(Mathf.Pow(direction.x, 2f) * Mathf.Sign(direction.x), Mathf.Pow(direction.y, 2f) * Mathf.Sign(direction.y));
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void Shoot()
    {
        shooting = !shooting;
    }

    void Aim()
    {
        aim = !aim;
        if (canAim && GetComponent<PlayerClass>().actWeapon < GetComponent<PlayerClass>().weapons.Count )
        {
            if (aim)
            {
                zoomCoef *= GetComponent<PlayerClass>().weapons[GetComponent<PlayerClass>().actWeapon].GetComponent<AbilityManager>().aimZoom;
            }
            else
            {
                zoomCoef /= GetComponent<PlayerClass>().weapons[GetComponent<PlayerClass>().actWeapon].GetComponent<AbilityManager>().aimZoom;
            }
        }
    }

    void Reload()
    {
        reloading = true;
    }

    void Ability1()
    {
        ability1 = !ability1;
        /*
        if (ability1)
        {
            ability1 = false;
            abi1 = false;
        }
        else
        {
            if (!abi1dual)
            {
                abi1 = true;
                abiD = 0;
            }
            else
            {
                abi1dual = false;
            }
        }
        */
    }

    void Ability2()
    {
        ability2 = !ability2;
        /*
        if (ability2)
        {
            ability2 = false;
            abi2 = false;
        }
        else
        {
            if (!abi2dual)
            {
                abi2 = true;
                abiD = 0;
            }
            else
            {
                abi2dual = false;
            }
        }
           */
    }

    void Ability3()
    {
        ability3 = !ability3;
    }

    void Ability4()
    {
        ability4 = !ability4;
    }

    void Switch(int s)
    {
        if (!aim && GetComponent<PlayerClass>().animatorWaitOneFrame > 5) //Avoid the player to change to fast of weapon, taht would make this glitch (see PlayerClass.WeaponChange(), we need to wait 5 frames
        {
            equipementIndex += s;
            if(equipementIndex >= GetComponent<PlayerClass>().weapons.Count)
            {
                equipementIndex = 0;
            }
            if(equipementIndex < 0)
            {
                equipementIndex = GetComponent<PlayerClass>().weapons.Count - 1;
            }
        }
    }
     
    void Action()
    {
        quickActioning = !quickActioning;
    }

    void Inventory()
    {
        inventoring = !inventoring;
    }

}
