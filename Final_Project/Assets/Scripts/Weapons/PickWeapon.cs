using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    public GameObject[] weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StoreWeapon();
        }
    }

    public void UseWeapon(int number)
    {
        for (int i =0 ; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[number].SetActive(true);
    }

    public void StoreWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

}
