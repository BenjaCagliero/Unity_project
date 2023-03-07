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
    int isRollFHash;
    int isRollBHash;
    public UIController uiController;

    public bool CanRoll()
    {
        bool canRoll = uiController.canEvade;
        return canRoll;
    }
    


    void Start()
    {
        animator= GetComponent<Animator>();
        Debug.Log(animator);
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isBackWardHash = Animator.StringToHash("isBackwards");
        isStrafeLHash = Animator.StringToHash("isStrafeL");
        isStrafeRHash = Animator.StringToHash("isStrafeR");
        isRollFHash = Animator.StringToHash("isRollF");
        isRollBHash = Animator.StringToHash("isRollB");

    }


    void Update()
    {
        //booleanos para animator
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackWardHash);
        bool isStrafeL = animator.GetBool(isStrafeLHash);
        bool isStrafeR = animator.GetBool(isStrafeRHash); 
        bool isRollF = animator.GetBool(isRollFHash);
        bool isRollB = animator.GetBool(isRollBHash);
        


        //booleanos para teclas
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool isBackwardPressed = Input.GetKey(KeyCode.S);
        bool strafeL = Input.GetKey(KeyCode.A);
        bool strafeR = Input.GetKey(KeyCode.D);
        bool roll = Input.GetKey(KeyCode.E);


        //condicion de roll F y B respectivamente, Sí esta moviendose hacia adelante o hacia atras
        if (!isRollF && roll && forwardPressed && CanRoll())
        {
            animator.SetBool(isRollFHash, true);
        }
        if (isRollF && !roll&& forwardPressed)
        {
            animator.SetBool(isRollFHash, false);
        }
        if (!isRollB && roll && isBackwardPressed && CanRoll())
        {
            animator.SetBool(isRollBHash, true);
        }
        if (isRollB && !roll && isBackwardPressed)
        {
            animator.SetBool(isRollBHash, false);
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
