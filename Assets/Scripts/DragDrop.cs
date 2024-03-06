using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DragDrop : MonoBehaviour
{
    private Grabbable grabInteractable;
    private Rigidbody rb;

    [SerializeField] private Transform dropZoneTransform;

    [SerializeField] private float thresholdDistance = 0.5f;

    private void Start()
    {
        grabInteractable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();
        if (grabInteractable == null)
        {
            Debug.LogError("Could not find XRGrabInteractable");
            return;
        }

        grabInteractable.WhenGrabbableUpdated += HandleGrabbableUpdated;
    }

    private void OnDestroy()
    {
        grabInteractable.WhenGrabbableUpdated -= HandleGrabbableUpdated;
    }

    private void HandleGrabbableUpdated(Oculus.Interaction.GrabbableArgs args)
    {
        if (!grabInteractable.IsGrabbed)
        {
            if (IsInsideDropZone())
            {
                transform.position = dropZoneTransform.position;
                transform.rotation = dropZoneTransform.rotation;
                //rb.isKinematic = true;
            }

        } else {
            //rb.isKinematic = false;
        }
    }


    private bool IsInsideDropZone()
    {
        if (dropZoneTransform == null) return false;

        return Vector3.Distance(transform.position, dropZoneTransform.position) <= thresholdDistance;
    }
}