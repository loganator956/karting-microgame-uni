using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public int ammo = 0;
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 30f);
        TimelinePlayer closestKart = null;
        float closestKartDistance = 999f;

        foreach (Collider col in cols)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (!rb) { continue; };
            TimelinePlayer kart = rb.GetComponent<TimelinePlayer>();
            if (kart)
            {
                float distance = Vector3.Distance(col.transform.position, transform.position);
                if (distance < closestKartDistance)
                {
                    closestKartDistance = distance;
                    closestKart = kart;
                }
            }
        }
        try { transform.LookAt(closestKart.transform.position, Vector3.up); } catch (System.NullReferenceException) { };
        if (ammo > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position + transform.forward, transform.forward, out rayHit, 30f))
            {
                TimelinePlayer timeliner = rayHit.collider.attachedRigidbody.gameObject.GetComponent<TimelinePlayer>();
                if (timeliner)
                {
                    timeliner.SlowDownLengthCounter = 20;
                    ammo--;
                    if (ammo <= 0)
                    {
                        DisableWeapon();
                    }
                }
            }
        }
    }

    public void EnableWeapon()
    {
        ammo = 20;
        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = true;
        }
    }

    public void DisableWeapon()
    {
        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }
}
