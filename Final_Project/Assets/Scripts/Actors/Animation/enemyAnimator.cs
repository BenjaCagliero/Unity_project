using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimator : MonoBehaviour
{
    public bool isMovingForward = false;
    Animator animator;
    int isMovingForwardHash;
    Rigidbody Rigidbody;

    void Start()
    {
        animator = GetComponent<Animator>();
        isMovingForwardHash = Animator.StringToHash("isMovingForward");


    }
    void Update()
    {
        bool isMovingForward = animator.GetBool(isMovingForwardHash);
        bool isMoving = GetComponent<Rigidbody>().velocity.magnitude> 0.2;
        //condicion para que el enemy se mueva en base a la relacion del velocidad del Rigidbody y del bool
        if (!isMovingForward && isMoving)
        {
            animator.SetBool(isMovingForwardHash, true);
        }
        if (isMovingForward && !isMoving)
        {
            animator.SetBool(isMovingForwardHash, false);
        }

    }





}
