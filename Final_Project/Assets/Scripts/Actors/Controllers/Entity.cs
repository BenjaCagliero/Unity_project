using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private string entityName;
    private string entityId;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public void CheckHealth()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
    public float GetHealth()
    {
        return health;
    }

    public string GetEntityName()
    {
        return entityName;
    }
    public string GetEntityId()
    {
        return entityId;
    }
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
    }
    public void Heal(float healAmount)
    {
        if (health < maxHealth)
        {
            health += healAmount;
        }
        
    }
    public void KillEntity()
    {
        GameObject.Destroy(gameObject);
    }
}
