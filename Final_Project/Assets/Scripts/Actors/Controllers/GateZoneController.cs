using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateZoneController : MonoBehaviour
{

    [SerializeField] private KeyController keyController;
    private bool _key;
    public Action<bool> onGateZone;
    void Start()
    {
        _key= false;
        keyController.onKeyPick += GotKey;
    }

    void OnTriggerEnter(Collider other)
    {
        var _name = other.transform.parent.tag;
        if (_name == "Player")
        {
            if (_key)
            {
                onGateZone?.Invoke(true);
                Destroy(gameObject);
            }
        }
            
    }
    void GotKey (bool key)
    {
        _key = key;
        keyController.onKeyPick -= GotKey;
    }
}
