using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Actors.Controllers
{
    public class Player : Entity
    {
        [SerializeField] private float healAmount;
        [SerializeField] private float damageAmount;

        public float GetHealAmount() { return healAmount;}
        public float GetDamageAmount() { return damageAmount;}

    }
    
}