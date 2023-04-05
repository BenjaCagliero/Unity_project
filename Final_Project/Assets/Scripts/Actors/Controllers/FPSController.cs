using Assets.Scripts.Actors.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class FPSController : Player
{
    [SerializeField]float groundDrag;
    private bool grounded;
    private Rigidbody _rb;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private bool haveMap = false;
    [SerializeField] private float evadeTimer;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private float sprintTimer = 5f;
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
    private bool reloadRun;
    //Animaciones
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isBackWardHash;
    int isStrafeLHash;
    int isStrafeRHash;
    int isJumpIHash;
    int isJumpRHash;
    int isAtackingHash;
    int isDamagedHash;
    int isDeadHash;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        //characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        attacks.Add(1,"Handed Combat") ;
        attacks.Add(2, "Using Bow");
        attacks.Add(3, "Using Sword");
        attacks.Add(4, "Using Shield");
        attacks.Add(5, "Avada Kevadra");
        attacks.Add(6, "Avocado Acabadooo ... c va");
        //Animaciones
        animator = GetComponent<Animator>();
        
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isBackWardHash = Animator.StringToHash("isBackwards");
        isStrafeLHash = Animator.StringToHash("isStrafeL");
        isStrafeRHash = Animator.StringToHash("isStrafeR");
        isJumpIHash = Animator.StringToHash("isJumpIdle");
        isJumpRHash = Animator.StringToHash("isJumpRun");
        isAtackingHash = Animator.StringToHash("isAtacking");
        //booleanos para animator
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackWardHash);
        bool isStrafeL = animator.GetBool(isStrafeLHash);
        bool isStrafeR = animator.GetBool(isStrafeRHash);
        bool isJumpI = animator.GetBool(isJumpIHash);
        bool isJumpR = animator.GetBool(isJumpRHash);
        bool isAtacking = animator.GetBool(isAtackingHash);
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, floor);
       
        RotationControl();
        CheckHealth();
        MapHandling();
        WeaponAndAbility();
        MovementControl();
        JumpControl();
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
                if (!reloadRun)
                {
                    sprintTimer -= Time.deltaTime;
                    Run();
                }
                else
                {
                    reloadRun = true;
                    canSprint = false;
                    SRun();
                }

            }
            else
            {
                reloadRun = true;
                canSprint= false;
                SRun();
            }
        }
        else
        {
            if (sprintTimer < 5)
            {
                sprintTimer += Time.deltaTime;
                SRun();
            }
            else if (sprintTimer >= 5) 
            {
                canSprint = true;
                reloadRun = false;
            }
        }

        float curSpeedZ = canMove ? ((isRunning && canSprint) ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedX = canMove ? ((isRunning && canSprint) ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedZ) + (right * curSpeedX) + new Vector3(0, _rb.velocity.y, 0);
        if (curSpeedZ != 0 || curSpeedX != 0)
        {
            _rb.velocity = moveDirection;
            if (evadeNow)
            {
                _rb.AddForce(transform.forward * 1000, ForceMode.Impulse);
                _rb.velocity = new Vector3(_rb.velocity.x, -math.abs(_rb.velocity.y), _rb.velocity.z);
            }
        }
        #endregion

        #region animation
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
        {
            if (!jumping)
            {
                //OnPFront?.Invoke(true);
                Walk();
            }
            else
            {
                SRun();
                //OnPFJump?.Invoke(true);
                JumpR();
                jumping= false;
                
            }
        }
        else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
        {
            FrontLeft();
            SRun();
        }
        else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
        {
            FrontRight();
            SRun();
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
        {
            if (!jumping)
            {
                Back();
                SRun();
            }
            else
            {
                //JumpBack();
                jumping= false;
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0)
        {
            BackLeft();
            SRun();
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0)
        {
            BackRight();
            SRun();
        }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") < 0)
        {
            StrafeL();
            SRun();

        }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") > 0)
        {
            StrafeR();
            SRun();
        }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            if (jumping)
            {
                JumpIdle();
                jumping= false;
            }
            else
            {
                Idle();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool(isAtackingHash, true);
        }
        else
        {
            animator.SetBool(isAtackingHash, false);
        }

    }
    private void Walk()
    {
        animator.SetBool(isWalkingHash, true);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isBackWardHash, false);
        animator.SetBool(isStrafeRHash, false);
        if (animator.GetBool(isJumpRHash))
        {
            animator.SetBool(isJumpRHash, false);
        }
    }
    private void JumpR()
    {
        animator.SetBool(isJumpRHash, true);
    }
    private void JumpIdle()
    {
        animator.SetBool(isJumpIHash, true);
    }
    private void Idle()
    {
        animator.SetBool(isRunningHash, false);
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isBackWardHash, false);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isStrafeRHash, false);
        if (animator.GetBool(isJumpIHash))
        {
            animator.SetBool(isJumpIHash, false);
        }
        animator.SetBool(isJumpRHash, false);
    }
    private void StrafeL()
    {
        animator.SetBool(isStrafeLHash, true);
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isStrafeRHash, false);
        animator.SetBool(isBackWardHash, false);
    }
    private void StrafeR()
    {
        animator.SetBool(isStrafeRHash, true);
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isBackWardHash, false);
    }
    private void Run()
    {
        animator.SetBool(isRunningHash, true);
    }
    private void SRun()
    {
        animator.SetBool(isRunningHash, false);
    }
    private void Back()
    {
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isBackWardHash, true);
        animator.SetBool(isStrafeRHash, false);
    }
    private void FrontLeft()
    {
        animator.SetBool(isWalkingHash, true);
        animator.SetBool(isStrafeLHash, true);
        animator.SetBool(isBackWardHash, false);
        animator.SetBool(isStrafeRHash, false);
    }
    private void FrontRight()
    {
        animator.SetBool(isWalkingHash, true);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isBackWardHash, false);
        animator.SetBool(isStrafeRHash, true);
    }
    private void BackLeft()
    {
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isStrafeLHash, true);
        animator.SetBool(isBackWardHash, true);
        animator.SetBool(isStrafeRHash, false);
    }
    private void BackRight()
    {
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isStrafeLHash, false);
        animator.SetBool(isBackWardHash, true);
        animator.SetBool(isStrafeRHash, true);
    }
    #endregion

    #region Handles Jumping
    void JumpControl()
    {
        if (Input.GetButtonDown("Jump") && canMove && grounded)
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
    public float GetStamina()
    {
        return sprintTimer;
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