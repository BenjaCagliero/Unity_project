using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateZoneController : MonoBehaviour
{

    [SerializeField] private KeyController keyController;
    private bool _key;
    public Action<bool> onGateZone;
    void Start()
    {
        _key= false;
        var scene = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByBuildIndex(3) != scene)
        {
            keyController.onKeyPick += GotKey;
        }
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
