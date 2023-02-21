using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float crawlSpeed = 2.5f;
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
    [SerializeField] private float evadeDuration = 0.2f;
    [SerializeField] private int evadePoints;
    [SerializeField] private LayerMask floor;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MapHandling();
        WeaponAndAbility();
        MovementControl();
        JumpControl();
        RotationControl();
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
        bool isCrawling = Input.GetKey(KeyCode.LeftControl);


        if (isCrawling)
        {
            Debug.Log("Agachado");
        }
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

        // se pueden cambiar por swich case para contemplar que este agachado (crawl)
        float curSpeedX = canMove ? (!evadeNow ? ((isRunning && canSprint) ? runSpeed : walkSpeed) : runSpeed * 5) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? ((isRunning && canSprint) ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY) +new Vector3 (0 , _rb.velocity.y , 0);
        _rb.velocity = moveDirection;
    }
    #endregion

    #region Handles Jumping
    void JumpControl()
    {
        RaycastHit hit;
        Ray ray;

        if (Input.GetButtonDown("Jump") && canMove && true && Physics.Raycast(transform.position,-transform.up, out hit, 0.1f, floor))
        {
            AddJumpForce(jumpPower);
        }
    }
    void AddJumpForce(float force)
    {
        _rb.AddForce(Vector3.up * force, ForceMode.Impulse);

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

        if (noWeapon)
        {
            Debug.Log("Handed Combat");
        }
        if (useBow)
        {
            Debug.Log("Using Bow");
        }
        if (useSword)
        {
            Debug.Log("Using Sword");
        }
        if (useShield)
        {
            Debug.Log("Using Shield");
        }
        if (useSpell1)
        {
            Debug.Log("Avada Kevadra");
        }
        if (evade)
        {
            if (canEvade)
            {
                Debug.Log("Avocado Acabadooo ... c va");
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

    }
    #endregion
}