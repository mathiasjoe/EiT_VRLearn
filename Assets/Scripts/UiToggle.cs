using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using Oculus.Interaction;

public class UiToggle : MonoBehaviour
{
    private Grabbable grabInteractable;

    public GameObject uiObject;
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
        uiObject.SetActive(grabInteractable.IsGrabbed);
    }
}
