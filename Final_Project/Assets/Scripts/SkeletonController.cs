using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float approachDistance;
    [SerializeField] private float idleDistance;

    void Update()
    {
        var vectorToTarget = target.position - transform.position;
        float distanceToTarget = target.position.magnitude;
        if (distanceToTarget <= attackDistance)
        {
            behaviour = SkeletonBehaviour.Attacking;
        }
        else if ((distanceToTarget >= attackDistance) && (distanceToTarget <= chaseDistance))
        {
            behaviour = SkeletonBehaviour.Chasing;
        }
        else if ((distanceToTarget >= approachDistance) && (distanceToTarget <= idleDistance))
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
                MoveEneny();
                break;
            case SkeletonBehaviour.Approaching:
                Aim();
                MoveEneny();
                break;
            case SkeletonBehaviour.Attacking:
                Attack();
                break;
            case SkeletonBehaviour.Idle:
                Idle();
                break;
        }
    }
    void Aim()
    {
        var stalkerPos = new Vector3(transform.position.x, 0, transform.position.z);
        var targetPos = new Vector3(target.position.x, 0, target.position.z);

        var vectorToObjective = targetPos - stalkerPos;
        var aiming = Quaternion.LookRotation(vectorToObjective);
        transform.rotation = Quaternion.Lerp(transform.rotation, aiming, Time.deltaTime * rotationSpeed);
    }
    void MoveEneny()
    {
        _rb.velocity =  transform.forward * speed;
    }
    void Attack()
    {

    }
    void Idle()
    {

    }
}
