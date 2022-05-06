using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Target[] NextTargets;
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
