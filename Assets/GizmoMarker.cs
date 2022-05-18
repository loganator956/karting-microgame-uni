using System;
using UnityEngine;

public class GizmoMarker : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}