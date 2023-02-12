using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum Behaviour
{
    Chasing,
    Glaring,
    Attacking,
    Approaching
}
public class StalkerController : MonoBehaviour
{

    [SerializeField] private Behaviour behaviour;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float approachDistance;
    
    void Update()
    {
        var vectorToTarget = target.position - transform.position;
        float distanceToTarget = target.position.magnitude;
        if (distanceToTarget <= attackDistance)
        {
            behaviour = Behaviour.Attacking;
        }
        else if ((distanceToTarget >= attackDistance) && (distanceToTarget <= chaseDistance))
        {
           behaviour= Behaviour.Chasing;
        }
        else if ((distanceToTarget >= chaseDistance) && (distanceToTarget <= approachDistance))
        {
            behaviour= Behaviour.Glaring;
        }
        else if (distanceToTarget >= approachDistance)
        {
            behaviour= Behaviour.Approaching;
        }

        switch (behaviour)
        {
            case Behaviour.Chasing:
                Aim();
                MoveEneny();
                break;
            case Behaviour.Approaching: 
                Aim();
                MoveEneny();
                break;
            case Behaviour.Attacking:
                Attack();
                break;
            case Behaviour.Glaring:
                Aim();
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
        transform.position = Vector3.Lerp(transform.position, transform.forward, Time.deltaTime * speed);
    }
    void Attack()
    { 

    }
    
}
