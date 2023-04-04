using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    public PickWeapon pickWeapon;
    public int weaponNumber;
    void Start()
    {
        pickWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PickWeapon>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickWeapon.UseWeapon(weaponNumber);
            Destroy(gameObject);
        }
    }
}
