using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField]private bool delete = false;
    public Action<bool> onKeyPick;

    private void Update()
    {
        if (delete)
            DeleteKey(); 
    }
    void OnTriggerEnter(Collider other)
    {
        onKeyPick?.Invoke(true);
        var _name = other.transform.parent.tag;
        if (_name == "Player")
            delete = true;
    }
    private void DeleteKey()
    {
        Destroy(gameObject);
    }
}
