using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ended : MonoBehaviour
{
    private bool ending = false;
    [SerializeField] private float m_timer;
    void Update()
    {
        if (ending)
        {

            if (0 < m_timer)
            {
                m_timer -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Main Menu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var _name = other.transform.parent.tag;
        if (_name == "Player")
        {
            ending = true;
        }
    }
}
