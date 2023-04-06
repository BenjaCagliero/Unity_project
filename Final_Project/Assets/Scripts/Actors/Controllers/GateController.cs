using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    [SerializeField]private KeyController keyController;
    [SerializeField] private GateZoneController gateZoneController;
    private bool m_key = false;
    // Start is called before the first frame update
    void Start()
    {
        var scene = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByBuildIndex(3) != scene)
        {
            keyController.onKeyPick += GotKey;
        }
        gateZoneController.onGateZone += OnZone;
    }

    // Update is called once per frame
    private void GotKey(bool key)
    {
        var scene = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByBuildIndex(3) != scene)
        {
            m_key = key;
        }
        else
        {
            m_key = true;
        }
    }
    private void OnZone(bool zone)
    {
        if (m_key)
        {
            Destroy(gameObject);
        }
    }
}
