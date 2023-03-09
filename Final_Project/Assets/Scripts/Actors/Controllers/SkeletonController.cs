using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum SkeletonBehaviour
{
    Chasing,
    Attacking,
    Approaching,
    Idle
}

public class SkeletonController : MonoBehaviour
{
    [SerializeField] private SkeletonBehaviour behaviour;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float approachDistance;
    [SerializeField] private float idleDistance;
    [SerializeField] private float distanceToTarget;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask floor;


    void Update()
    {
        






        var vectorToTarget = target.transform.position - transform.position;
        distanceToTarget = vectorToTarget.magnitude;

        RaycastHit viewing;

        if (distanceToTarget <= attackDistance)
        {
            behaviour = SkeletonBehaviour.Attacking;
        }
        else if ((distanceToTarget > attackDistance) && (distanceToTarget <= chaseDistance))
        {
            behaviour = SkeletonBehaviour.Chasing;
        }
        else if ((distanceToTarget > approachDistance) && (distanceToTarget < idleDistance))
        {
            behaviour = SkeletonBehaviour.Approaching;
        }
        else if(distanceToTarget >= idleDistance)
        {
            behaviour = SkeletonBehaviour.Idle;
        }
        switch (behaviour)
        {
            case SkeletonBehaviour.Chasing:
                Aim();
                if (Physics.Raycast(transform.position, vectorToTarget.normalized, out viewing, chaseDistance, player))
                {
                        MoveEneny();
                }
                break;
            case SkeletonBehaviour.Approaching:
                Aim();
                if (Physics.Raycast(transform.position, vectorToTarget.normalized, out viewing, approachDistance, player))
                {
                        MoveEneny();
                }
                break;
            case SkeletonBehaviour.Attacking:
                Attack();
                Aim();
                break;
            case SkeletonBehaviour.Idle:
                Idle();
                break;

        }

    }

    void Aim()
    {
        var stalkerPos = new Vector3(transform.position.x, 0, transform.position.z);
        var targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        var vectorToObjective = targetPos - stalkerPos;
        var aiming = Quaternion.LookRotation(vectorToObjective);
        transform.rotation = Quaternion.Lerp(transform.rotation, aiming, Time.deltaTime * rotationSpeed);
    }
    void MoveEneny()
    {
        var forward = transform.TransformDirection(Vector3.forward);
        Vector3 moveDirection = (forward * speed) + new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity = moveDirection;

    }
    void Attack()
    {

    }
    void Idle()
    {

    }
}
