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

    void Update()
    {
        Vector3 targetDelta = targets[currentIndex].transform.position - transform.position;
        targetDelta.y = 0;
        Vector3 forwards = transform.forward;
        forwards.y = 0;

        if (Vector3.Dot(forwards, targetDelta) < 0)
        {
            currentIndex++;
            if (currentIndex >= targets.Count)
            {
                currentIndex = 0;
            }
        }
        float angle = Vector3.SignedAngle(transform.position, targets[currentIndex].transform.position, Vector3.up);
        Steer = Mathf.Clamp(angle, -1f, 1f);
        print(Steer);
        Accelerate = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targets[currentIndex].transform.position, 2f);
    }
}
