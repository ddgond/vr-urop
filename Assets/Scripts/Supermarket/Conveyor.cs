using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector3 velocity;
    public float maxAcceleration = 0.5f;

    private Rigidbody rb;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider collider)
    {
        ItemLabel itemLabel = collider.GetComponentInParent<ItemLabel>();
        if (itemLabel != null)
        {
            Vector3 deltaV = velocity - collider.attachedRigidbody.velocity;
            Vector3 acceleration = deltaV / Time.fixedDeltaTime;
            if (acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
            {
                acceleration = acceleration.normalized * maxAcceleration;
            }
            collider.attachedRigidbody.AddForce(acceleration, ForceMode.Acceleration);
        }
    }
}
