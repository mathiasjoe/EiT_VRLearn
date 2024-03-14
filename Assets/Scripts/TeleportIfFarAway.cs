using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportIfFarAway : MonoBehaviour
{
    private Vector3 originalPosition;

    public float maxDistance = 2.5f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, originalPosition) > maxDistance)
        {
            transform.position = originalPosition;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
            }
        }
    }
}
