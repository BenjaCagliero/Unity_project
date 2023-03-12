using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReposition : MonoBehaviour
{
    [SerializeField] private LayerMask player;

     
    private void OnTriggerEnter(Collider other)
    {
        
            other.gameObject.transform.position += new Vector3(-32f, 17f);
            Debug.Log("Me caigooooo");
        
    }
}
