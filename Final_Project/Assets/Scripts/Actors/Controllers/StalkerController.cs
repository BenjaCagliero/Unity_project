using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum StalkerBehaviour
{
    Chasing,
    Glaring,
    Attacking,
    Approaching,
    Idle

}
public class StalkerController : MonoBehaviour
{

    [SerializeField] private StalkerBehaviour behaviour;
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
            behaviour = StalkerBehaviour.Attacking;
        }
        else if ((distanceToTarget >= attackDistance) && (distanceToTarget <= chaseDistance))
        {
           behaviour= StalkerBehaviour.Chasing;
        }
        else if ((distanceToTarget >= chaseDistance) && (distanceToTarget <= approachDistance))
        {
            behaviour= StalkerBehaviour.Glaring;
        }
        else if ((distanceToTarget >= approachDistance) && (distanceToTarget <= idleDistance))
        {
            behaviour= StalkerBehaviour.Approaching;
        }
        else if (distanceToTarget >= idleDistance)
        {
            behaviour = StalkerBehaviour.Idle;
        }

        switch (behaviour)
        {
            case StalkerBehaviour.Chasing:
                Aim();
                MoveEneny();
                break;
            case StalkerBehaviour.Approaching: 
                Aim();
                MoveEneny();
                break;
            case StalkerBehaviour.Attacking:
                Aim();
                Attack();
                break;
            case StalkerBehaviour.Glaring:
                Aim();
                break;
            case StalkerBehaviour.Idle:
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
        transform.rotation =Quaternion.Lerp(transform.rotation, aiming, Time.deltaTime * rotationSpeed);
    }
    void MoveEneny()
    {
        _rb.velocity = transform.forward * speed;
    }
    void Attack()
    { 

    }
    void Idle()
    {

    }
    
}
