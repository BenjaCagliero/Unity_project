using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float crawlSpeed = 2.5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private bool haveMap = false;

    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private bool canMove = true;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Handle Map
        bool seeMap = Input.GetKey(KeyCode.Tab);
        if (seeMap && haveMap)
        {
            Debug.Log("Opening Map.");
        }
        else (Input.GetKeyUp(KeyCode.Tab))
        {
            Debug.Log("You dont have a map.");
        }

        #endregion

        #region Handle Abilities and weapons
        bool noWeapon = Input.GetKey(KeyCode.F1);    
        bool useBow = Input.GetKey(KeyCode.F2);    
        bool useSword = Input.GetKey(KeyCode.F3);    
        bool useShield = Input.GetKey(KeyCode.F4);    
        bool useSpell1 = Input.GetKey(KeyCode.Q);    
        bool useSpell2 = Input.GetKey(KeyCode.E);    

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
        if (useSpell2)
        {
            Debug.Log("Avocado Acabadooo");
        }

        #endregion

        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrawling = Input.GetKey(KeyCode.LeftCtrl);
        if (isCrawling)
        {
            Debug.Log("Agachado");
        }
        
        // se pueden cambiar por swich case para contemplar que este agachado (crawl)
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }
}