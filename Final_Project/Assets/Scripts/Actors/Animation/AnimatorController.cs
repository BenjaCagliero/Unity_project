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
    public FPSController m_fPSController;

    


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
        //booleanos para animator
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackWardHash);
        bool isStrafeL = animator.GetBool(isStrafeLHash);
        bool isStrafeR = animator.GetBool(isStrafeRHash);
        bool isJumpI = animator.GetBool(isJumpIHash);
        bool isJumpR = animator.GetBool(isJumpRHash);
        
        m_fPSController.OnIdle += Idle;
        m_fPSController.OnPFront += Walk;
        m_fPSController.OnPBack += Back;
        m_fPSController.OnPLeft += StrafeL;
        m_fPSController.OnPRight += StrafeR;
        m_fPSController.OnPFJump += JumpR;
        m_fPSController.OnPJump += JumpIdle;
        m_fPSController.OnSprintF += Run;
        m_fPSController.OnStopSprint += SRun;
    }


    private void JumpIdle(bool jump)
    {
        animator.SetBool(isJumpIHash, true);
    }


    private void JumpR(bool jumpR)
    {
        animator.SetBool(isJumpRHash, true);
    }


    //condicion de strafe en L y R respectivamente
    private void StrafeL(bool strafeL)
    {
        animator.SetBool(isStrafeLHash, true);
    }


    private void StrafeR(bool strafeR)
    {
        animator.SetBool(isStrafeRHash, true);
        
    }


    //condicion de caminar si se apreta W
    private void Walk(bool walk)
    {
        animator.SetBool(isWalkingHash, true);
        if(animator.GetBool(isJumpRHash))
        {
            animator.SetBool(isJumpRHash, false);
        }
    }
 

    //condicion de correr si esta el shift izq. y se esta moviendo
    private void Run(bool run)
    {
        animator.SetBool(isRunningHash, true);
    }
    private void SRun(bool sRun)
    {
        animator.SetBool(isRunningHash, false);
    }

    //condicion movimiento hacia atras si esta apretado S
    private void Back(bool back)
    {
        animator.SetBool(isBackWardHash, true);
    }

    private void Idle(bool idle)
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

}
