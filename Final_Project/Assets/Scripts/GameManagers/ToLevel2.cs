using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevel2 : MonoBehaviour
{
    [SerializeField] ScenesManager scenesManager;
    void OnTriggerEnter(Collider other)
    {
        var _name = other.transform.parent.tag;
        if (_name == "Player")
        {
            scenesManager.LoadGameLevel2();
        }

    }
}
