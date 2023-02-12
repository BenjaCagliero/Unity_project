using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStates
{
    Observe,
    Stalk,
}

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private enemyStates currentState;
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float persuitDistance;
    [SerializeField] private Vector3 initialRotation;

    private void Start()
    {

    }

    private void lookPlayer()
    {
        transform.LookAt(player);
    }

    private void stalkLookPlayer()
    {
        var vectorToPlayer = player.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

    }
    public void SetCurrentState()
    {

        switch(currentState)
        {
            case enemyStates.Observe:
                ExecuteObserve();
                break;

            case enemyStates.Stalk:
                ExecuteStalk();
                break;

            default:
                Debug.LogError("State invalid");
                break;
        }

    }

    private void Update()
    {
        SetCurrentState();
    }
    private void ExecuteObserve()
    {
        stalkLookPlayer(); 
    }
    private void ExecuteStalk()
    {
        var vectorToPlayer = player.position - transform.position;
        var distance = vectorToPlayer.magnitude;
        lookPlayer();
        if (distance > persuitDistance)
        {
            transform.position += vectorToPlayer.normalized * (speed * Time.deltaTime);
        }
    }
}

