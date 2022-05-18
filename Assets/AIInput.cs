using System.Collections;
using System.Collections.Generic;
using KartGame.KartSystems;
using UnityEngine;

public class AIInput : KartGame.KartSystems.BaseInput
{
    public List<Vector3> targets = new List<Vector3>();
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

    Vector3 lastPosition;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        TargetProvider targetProvider = FindObjectOfType<TargetProvider>();
        targets = targetProvider.RequestRandomPath();
    }

    void Update()
    {
        Vector3 targetDelta = (targets[currentIndex] - transform.position).normalized;
        _targetDelta = targetDelta;
        targetDelta.y = 0;
        Vector3 forwards = transform.forward;
        forwards.y = 0;

        if (Vector3.Distance(targets[currentIndex], transform.position) < 5f)
        {
            lastPosition = targets[currentIndex];
            currentIndex++;
            if (currentIndex >= targets.Count)
            {
                currentIndex = 0;
            }
        }
        float angle = Vector3.SignedAngle(forwards, targetDelta, Vector3.up);
        Steer = angle / 30f;
        Steer = Mathf.Clamp(Steer, -1f, 1f);
        Accelerate = true;
        if (rb.velocity.magnitude < 0.5f)
        {
            StuckTimer += Time.deltaTime;
            if (StuckTimer > 6f)
            {
                transform.position = lastPosition + Vector3.up;
                StuckTimer = 0f;
            }
        }
    }

    public float StuckTimer = 0f;

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {

    }

    void OnDrawGizmos()
    {
        try
        {
            Gizmos.DrawSphere(targets[currentIndex], 2f);
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
        catch (System.Exception)
        {

        }
    }
}