using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DragDrop : MonoBehaviour
{
    private Grabbable grabInteractable;

    [SerializeField] private Transform dropZoneTransform;

    [SerializeField] private float thresholdDistance = 0.5f;

    private void Start()
    {
        grabInteractable = GetComponent<Grabbable>();
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
        Debug.Log("HandleGrabbableUpdated executed");
    }


    private void OnGrab()
    {
        Debug.Log("Object grabbed!");
    }

    private void OnRelease()
    {
        Debug.Log("Object released!");

        //if (IsInsideDropZone())
        //{
            // Snap object to the center of the drop zone
        //    transform.position = dropZoneTransform.position;
        //}
    }

    private bool IsInsideDropZone()
    {
        if (dropZoneTransform == null) return false;

        return Vector3.Distance(transform.position, dropZoneTransform.position) <= thresholdDistance;
    }
}
