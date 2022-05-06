using System.Collections;
using System.Collections.Generic;
using KartGame.KartSystems;
using UnityEngine;

public class AIInput : KartGame.KartSystems.BaseInput
{
    public List<GameObject> targets = new List<GameObject>();
    public bool Accelerate { get; set; }
    public bool Brake { get; set; }
    public float Steer { get; set; }

    public override InputData GenerateInput()
    {
        return new InputData
        {
            Accelerate = Accelerate,
            Brake = Brake,
            TurnInput = Steer
        };
    }

    int currentIndex = 0;

    Vector3 _targetDelta;

    void Update()
    {
        Vector3 targetDelta = (targets[currentIndex].transform.position - transform.position).normalized;
        _targetDelta = targetDelta;
        targetDelta.y = 0;
        Vector3 forwards = transform.forward;
        forwards.y = 0;

        if (Vector3.Distance(targets[currentIndex].transform.position, transform.position) < 5f)
        {
            currentIndex++;
            if (currentIndex >= targets.Count)
            {
                currentIndex = 0;
            }
        }
        float angle = Vector3.SignedAngle(forwards, targetDelta, Vector3.up);
        Steer = angle / 30f;
        Steer = Mathf.Clamp(Steer, -1f, 1f);
        print(Steer);
        Accelerate = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targets[currentIndex].transform.position, 2f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _targetDelta * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);

        Vector3 minTurnAngle = Quaternion.AngleAxis(-30, Vector3.up) * transform.forward;
        Vector3 maxTurnAngle = Quaternion.AngleAxis(30, Vector3.up) * transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + minTurnAngle * 2);
        Gizmos.DrawLine(transform.position, transform.position + maxTurnAngle * 2);

        Gizmos.color = Color.cyan;
        Vector3 steerAngle = Quaternion.AngleAxis(30 * Steer, Vector3.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + steerAngle);
    }
}
