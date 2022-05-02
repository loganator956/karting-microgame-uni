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
        if (rb)
        {
            Debug.Log("Rigidbody");
            WeaponFire weapon = rb.GetComponentInChildren<WeaponFire>();
            if (weapon)
            {
                Debug.Log("Has weapon");
                weapon.EnableWeapon();
            }
        }
    }
}