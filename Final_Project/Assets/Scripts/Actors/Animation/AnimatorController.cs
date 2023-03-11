using Assets.Scripts.Actors.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isBackWardHash;
    int isStrafeLHash;
    int isStrafeRHash;
    int isJumpIHash;
    int isJumpRHash;
    public UIController uiController;


    


    void Start()
    {
        animator= GetComponent<Animator>();
        //Debug.Log(animator);
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isBackWardHash = Animator.StringToHash("isBackwards");
        isStrafeLHash = Animator.StringToHash("isStrafeL");
        isStrafeRHash = Animator.StringToHash("isStrafeR");
        isJumpIHash = Animator.StringToHash("isJumpIdle");
        isJumpRHash = Animator.StringToHash("isJumpRun");
    }


    void Update()
    {
        //booleanos para animator
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackWardHash);
        bool isStrafeL = animator.GetBool(isStrafeLHash);
        bool isStrafeR = animator.GetBool(isStrafeRHash);
        bool isJumpI = animator.GetBool(isJumpIHash);
        bool isJumpR = animator.GetBool(isJumpRHash);




        //booleanos para teclas
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool isBackwardPressed = Input.GetKey(KeyCode.S);
        bool strafeL = Input.GetKey(KeyCode.A);
        bool strafeR = Input.GetKey(KeyCode.D);
        bool jumping = Input.GetKey(KeyCode.Space);
        bool dash = Input.GetKey(KeyCode.E);

        
        
        
       

        //Condicion para saltar coriendo y en Idle
        if (!isJumpI && jumping)
        {
            animator.SetBool(isJumpIHash, true);
        }
        if (isJumpI && !jumping)
        {
            animator.SetBool(isJumpIHash, false);
        }

        if (!isJumpR && jumping && forwardPressed)
        {
            animator.SetBool(isJumpRHash, true);
        }
        if (isJumpR && !jumping)
        {
            animator.SetBool(isJumpRHash, false);
        }

        //condicion de strafe en L y R respectivamente
        if (!isStrafeL && strafeL)
        {
            animator.SetBool(isStrafeLHash, true);
        }
        if (isStrafeL && !strafeL)
        {
            animator.SetBool(isStrafeLHash, false);
        }

        if (!isStrafeR && strafeR)
        {

            animator.SetBool(isStrafeRHash, true);
        }
        if (isStrafeR && !strafeR)
        {
            animator.SetBool(isStrafeRHash, false);
        }

        //condicion de caminar si se apreta W
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        
        //condicion de correr si esta el shift izq. y se esta moviendo
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        //condicion movimiento hacia atras si esta apretado S
        if (!isBackward && isBackwardPressed)
        {
            animator.SetBool(isBackWardHash, true);
        }
        if (isBackward && !isBackwardPressed)
        {
            animator.SetBool(isBackWardHash, false);
        }
    }
}
