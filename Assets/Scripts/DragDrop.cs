using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DragDrop : MonoBehaviour
{
    private Grabbable grabInteractable;
    private GameObject[] dropZones;

    [SerializeField] private Transform dropZoneTransform;

    [SerializeField] private float thresholdDistance = 0.5f;

    private void Start()
    {
        grabInteractable = GetComponent<Grabbable>();
        dropZones = GameObject.FindGameObjectsWithTag("DropZone");
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
        if (grabInteractable.IsGrabbed)
        {
            OnGrab();
        } else {
            OnRelease();
        }
    }

    private void OnGrab()
    {
        EnableDropzonesOutline();
    }

    private void OnRelease()
    {
        if (IsInsideDropZone())
        {
            DisableDropzonesOutline();
            transform.position = dropZoneTransform.position;
            transform.rotation = dropZoneTransform.rotation;
        }
    }

    private void EnableDropzonesOutline()
    {
        foreach (var dropZone in dropZones)
        {
            MeshRenderer mesh = dropZone.GetComponent<MeshRenderer>(); 
            if (mesh != null)
            {
                mesh.enabled = true;
            }
        }
    }

    private void DisableDropzonesOutline()
    {
        foreach (var dropZone in dropZones)
        {
            MeshRenderer mesh = dropZone.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.enabled = false;
            }
        }
    }

    private bool IsInsideDropZone()
    {
        if (dropZoneTransform == null) return false;

        return Vector3.Distance(transform.position, dropZoneTransform.position) <= thresholdDistance;
    }
}