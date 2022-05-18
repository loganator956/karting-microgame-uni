using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        Debug.Log("Trigger enter");
        if (rb) // check if rigidbody exists (not null)
        {
            Debug.Log("Rigidbody");
            WeaponFire weapon = rb.GetComponentInChildren<WeaponFire>(); // attempt to get WeaponFire object
            if (weapon)
            {
                Debug.Log("Has weapon");
                weapon.EnableWeapon(); // enable weapon if exists
            }
        }
    }
}