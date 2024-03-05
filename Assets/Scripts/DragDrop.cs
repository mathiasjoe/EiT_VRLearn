using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DragDrop : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [SerializeField] private Transform dropZoneTransform;

    [SerializeField] private float thresholdDistance = 0.5f;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null )
        {
            Debug.LogError("XRGrabInteractable component not found in the object");
            return;
        }

        grabInteractable.hoverEntered.AddListener(OnHover);
        grabInteractable.hoverExited.AddListener(OnHoverExit);
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        if ( grabInteractable != null )
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnHover(HoverEnterEventArgs args)
    {
        Debug.Log($"{args.interactor.gameObject.name} hovered {gameObject.name}");
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log($"{args.interactor.gameObject.name} left {gameObject.name}");
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{args.interactor.gameObject.name} started grabbing {gameObject.name}");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{args.interactor.gameObject.name} release {gameObject.name}");

        if (IsInsideDropZone())
        {
            // Snap object to the center of the drop zone
            transform.position = dropZoneTransform.position;
        }
    }

    private bool IsInsideDropZone()
    {
        if (dropZoneTransform == null) return false;

        return Vector3.Distance(transform.position, dropZoneTransform.position) <= thresholdDistance;
    }
}
