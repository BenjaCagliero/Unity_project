using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Actors.Controllers
{
    public class Enemy : Entity
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float speed;
        public void MoveEneny()
        {
            var forward = transform.TransformDirection(Vector3.forward);
            Vector3 moveDirection = (forward * speed) + new Vector3(0, _rb.velocity.y, 0);
            _rb.velocity = moveDirection;

        }
    }
}