using Assets.Scripts.Actors.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class FPSController : Player
{
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private bool haveMap = false;
    [SerializeField] private float evadeTimer;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private float sprintTimer = Mathf.Clamp(0f, -3f, 5f);
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool evadeNow;
    [SerializeField] private bool canEvade = true;
    public bool canDash;
    [SerializeField] private float evadeDuration = 0.2f;
    [SerializeField] private int evadePoints;
    [SerializeField] private LayerMask floor;
    [SerializeField] private Dictionary<int,string> attacks = new Dictionary<int, string>();
    private bool jumping;

    public event Action PFront;
    public event Action PFrontR;
    public event Action PFrontL;
    public event Action PBack;
    public event Action PBackR;
    public event Action PBackL;
    public event Action PRight;
    public event Action PLeft;
    public event Action PFJump;
    public event Action PBJump; 

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        attacks.Add(1,"Handed Combat") ;
        attacks.Add(2, "Using Bow");
        attacks.Add(3, "Using Sword");
        attacks.Add(4, "Using Shield");
        attacks.Add(5, "Avada Kevadra");
        attacks.Add(6, "Avocado Acabadooo ... c va");
    }

    void Update()
    {
        CheckHealth();
        MapHandling();
        WeaponAndAbility();
        MovementControl();
        JumpControl();
        RotationControl();
        if (GetHealth() <= 0)
        {
            KillEntity();
        }
    }


    #region Handle Map
    void MapHandling()
    {

        bool seeMap = Input.GetKey(KeyCode.Tab);
        if (seeMap && haveMap)
        {
            Debug.Log("Opening Map.");
        }



    }
    #endregion

    #region Handles Movement
    void MovementControl()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            if (sprintTimer >= 0)
            {
                sprintTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (sprintTimer < 5)
            {
                sprintTimer += Time.deltaTime;
            }
        }

        //Cooldown Sprint
        if (sprintTimer <= 0)
        {
            canSprint = false;
        }
        else
        {
            canSprint = true;
        }


        float curSpeedZ = canMove ? (!evadeNow ? ((isRunning && canSprint) ? runSpeed : walkSpeed) : runSpeed * 5) * Input.GetAxis("Vertical") : 0;
        float curSpeedX = canMove ? ((isRunning && canSprint) ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedZ) + (right * curSpeedX) + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        if (curSpeedZ != 0 || curSpeedX != 0)
        {
            GetComponent<Rigidbody>().velocity = moveDirection;
        }
        if (curSpeedZ > 0 && curSpeedX == 0)
        {
            if (!jumping)
            {
                PFront.Invoke();
            }
            else
            {
                PFJump.Invoke();
            }
        }
        else if (curSpeedZ > 0 && curSpeedX < 0)
        {
            PFrontL.Invoke();
        }
        else if (curSpeedZ > 0 && curSpeedX > 0)
        {
            PFrontR.Invoke();
        }
        else if (curSpeedZ < 0 && curSpeedX == 0)
        {
            if (!jumping)
            {
                PBack.Invoke();
            }
            else
            {
                PFJump.Invoke();
            }
        }
        else if (curSpeedZ < 0 && curSpeedX < 0)
        {
            PBackL.Invoke();
        }
        else if (curSpeedZ < 0 && curSpeedX > 0)
        {
            PBackR.Invoke();
        }
        else if (curSpeedZ == 0 && curSpeedX > 0)
        {
            PLeft.Invoke();
        }
        else if (curSpeedZ == 0 && curSpeedX < 0)
        {
            PRight.Invoke();
        }


    }
    #endregion

    #region Handles Jumping
    void JumpControl()
    {
        RaycastHit hit;

        if (Input.GetButtonDown("Jump") && canMove && true && Physics.Raycast(transform.position,-transform.up, out hit, 0.2f, floor))
        {
            AddJumpForce(jumpPower);
            jumping= true;
        }
        else
        {
            jumping= false;
        }
    }
    void AddJumpForce(float force)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);

    }
    #endregion

    #region Handles Rotation
    void RotationControl()
    {
        //characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
           
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    #endregion

    #region Handle Abilities and weapons
    void WeaponAndAbility()
    {
        bool noWeapon = Input.GetKey(KeyCode.F1);
        bool useBow = Input.GetKey(KeyCode.F2);
        bool useSword = Input.GetKey(KeyCode.F3);
        bool useShield = Input.GetKey(KeyCode.F4);
        bool useSpell1 = Input.GetKey(KeyCode.Q);
        bool evade = Input.GetKeyDown(KeyCode.E);
        bool heal = Input.GetKeyDown(KeyCode.H);
        bool damage = Input.GetKeyDown(KeyCode.J);

        string text = "";

        if(heal)
        {
            Debug.Log("healing");
            Heal(GetHealAmount());
        }
        if(damage)
        {
            Debug.Log("Damaging");
            Damage(GetDamageAmount());
        }

        if (noWeapon)
        {
            text = attacks[1].ToUpper();
            Debug.Log(text);
        }
        if (useBow)
        {
            text = attacks[2].ToUpper();
            Debug.Log(text);
        }
        if (useSword)
        {
            text = attacks[3].ToUpper();
            Debug.Log(text);
        }
        if (useShield)
        {
            text = attacks[4].ToUpper();
            Debug.Log(text);
        }
        if (useSpell1)
        {
            text = attacks[5].ToUpper();
            Debug.Log(text);
        }
        if (evade)
        {
            if (canEvade)
            {
                text = attacks[6].ToUpper();
                Debug.Log(text);
                GameManager.instance.addDash(evadePoints);
                evadeNow = true;
            }
            
        }
        if (evadeNow)
        {
            canEvade = false;
            if (evadeDuration > 0)
            {
                evadeDuration -= Time.deltaTime;

            }
            else
            {
                evadeDuration = 0.2f;
                evadeNow = false;
            }
        }
        if (!canEvade)
        {
            if (evadeTimer > 0)
            {
                evadeTimer -= Time.deltaTime;
            }
            else
            {
                evadeTimer = 3;
                canEvade = true;
            }
        }
        canDash = canEvade;
    }
    #endregion
}