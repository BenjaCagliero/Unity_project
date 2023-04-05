using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private bool delete = false;
    public Action<bool> onGrabPick;

    private void Update()
    {
        if (delete && Input.GetKeyDown(KeyCode.F))
        {
            onGrabPick?.Invoke(true);
            DeleteGrab();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var _name = other.transform.parent.tag;
        if (_name == "Player")
        {
                delete = true;
        }
    }

    private void DeleteGrab()
    {
        Destroy(gameObject);
    }
}
