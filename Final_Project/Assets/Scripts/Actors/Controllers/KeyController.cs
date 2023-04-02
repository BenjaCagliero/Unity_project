using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField]private bool delete = false;

    private void Update()
    {
        if (delete)
            DeleteKey(); 
    }
    void OnTriggerEnter(Collider other)
    {
        var _name = other.transform.parent.tag;
        if (_name == "Player")
            delete = true;
    }
    private void DeleteKey()
    {
        Destroy(gameObject);
    }
}
