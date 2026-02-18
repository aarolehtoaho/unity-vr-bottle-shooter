using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    private Rigidbody rb;
    private float WindStrength = 0.5f;
    private float DistanceToMiddle;
    private float BounceStrength = 0.5f;
    private float GroundLevel = 0.364f;
    private float MaxSpeed = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DistanceToMiddle = Vector3.Distance(transform.position, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        AddWindForce();
        AddBounceForce();
        LimitSpeed();
    }

    private void AddWindForce()
    {
        Vector3 DirectionToMiddle = Vector3.zero - transform.position;
        Vector3 PerpendicularDirection = Vector3.Cross(DirectionToMiddle, Vector3.up).normalized;
        Vector3 WindForce = PerpendicularDirection * WindStrength;
        if (DirectionToMiddle.magnitude > DistanceToMiddle)
        {
            Vector3 CorrectionForce = DirectionToMiddle.normalized * 0.5f;
            WindForce += CorrectionForce;
        }

        rb.AddForce(WindForce, ForceMode.Force);
    }

    private void AddBounceForce()
    {
        if (transform.position.y <= GroundLevel)
        {
            Vector3 BounceForce = Vector3.up * BounceStrength;
            rb.AddForce(BounceForce, ForceMode.Impulse);
        }
    }

    private void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > MaxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * MaxSpeed;
        }
    }
}
