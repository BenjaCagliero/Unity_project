using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isBackWardHash;
    void Start()
    {
        animator= GetComponent<Animator>();
        Debug.Log(animator);
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isBackWardHash = Animator.StringToHash("isBackwards");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackWardHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool isBackwardPressed = Input.GetKey(KeyCode.S);



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
