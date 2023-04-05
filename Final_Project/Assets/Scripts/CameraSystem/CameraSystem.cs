using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    public GameObject Camera1;
    public GameObject Camera2;

    private void Start()
    {
        CameraOne();
    }


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            CameraOne();
        }

        if (Input.GetKeyDown("2"))
        {
            CameraTwo();
        }
    }

    void CameraOne()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);

    }

    void CameraTwo()
    {
        Camera2.SetActive(true);
        Camera1.SetActive(false);
    }
}
