using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public int ammo = 0;
    public bool isPlayer = false;
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 30f);
        Enemy closestKart = null;
        float closestKartDistance = 999f;

        foreach (Collider col in cols)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (!rb) { continue; };
            Enemy kart = rb.GetComponent<Enemy>();
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
        if (isPlayer && ammo > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ammo--;
            if (ammo <= 0)
            {
                DisableWeapon();
            }
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position + transform.forward, transform.forward, out rayHit, 30f))
            {
                Instantiate(line, Vector3.zero, Quaternion.identity);
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                lineRenderer.SetPositions(new Vector3[] { transform.position, rayHit.point });
                if (!rayHit.collider.attachedRigidbody) { return; };
                Collider[] colliders = Physics.OverlapSphere(rayHit.point, 3f);
                foreach (Collider col in colliders)
                {
                    col.attachedRigidbody.AddExplosionForce(2000f, rayHit.point, 4f);
                }

                Debug.Log(rayHit.transform.name);
                // Enemy timeliner = rayHit.collider.attachedRigidbody.gameObject.GetComponent<Enemy>();
                // if (timeliner)
                // {
                //     // hit
                //     timeliner.AddDamage();
                // }
            }
        }
    }
    public GameObject line;

    public void EnableWeapon()
    {
        ammo = 20;
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = true;
        }
    }

    public void DisableWeapon()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }
}
