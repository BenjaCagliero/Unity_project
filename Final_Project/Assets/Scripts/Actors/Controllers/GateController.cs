using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]private KeyController keyController;
    [SerializeField] private GateZoneController gateZoneController;
    private bool m_key = false;
    // Start is called before the first frame update
    void Start()
    {
        keyController.onKeyPick += GotKey;
        gateZoneController.onGateZone += OnZone;
    }

    // Update is called once per frame
    private void GotKey(bool key)
    {
        m_key= key;
    }
    private void OnZone(bool zone)
    {
        if (m_key)
        {
            Destroy(gameObject);
        }
    }
}
