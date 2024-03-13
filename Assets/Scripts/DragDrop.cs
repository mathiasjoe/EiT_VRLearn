using UnityEngine;
using Oculus.Interaction;
using TMPro;

public class DragDrop : MonoBehaviour
{
    private Grabbable grabInteractable;
    private GameObject[] dropZones;

    [SerializeField] private Transform dropZoneTransform;

    [SerializeField] private float thresholdDistance = 0.5f;

    [SerializeField] private GameObject goal;

    private string goalBaseText;

    private void Start()
    {
        grabInteractable = GetComponent<Grabbable>();
        dropZones = GameObject.FindGameObjectsWithTag("DropZone");
        if (grabInteractable == null)
        {
            Debug.LogError("Could not find XRGrabInteractable");
            return;
        }

        if (goal != null)
        {
            goalBaseText = goal.GetComponent<TextMeshProUGUI>().text;
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
        }
        else
        {
            OnRelease();
        }
    }

    private void OnGrab()
    {
        // EnableDropzonesOutline();

        SetGoalTextColor(Color.white);
        SetStrikethrought(false);

        SetGravity(false);

    }

    private void OnRelease()
    {
        // DisableDropzonesOutline();
        if (IsInsideDropZone())
        {
            transform.position = dropZoneTransform.position;
            transform.rotation = dropZoneTransform.rotation;

            SetGoalTextColor(Color.green);
            SetStrikethrought(true);

            SetGravity(false);
        } else
        {
            SetGravity(true);
        }
    }

    private void SetGravity(bool enable)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (enable)
        {
            rb.isKinematic = false;
        } else
        {
            rb.isKinematic = true;
        }
    }

    private void SetStrikethrought(bool enable)
    {
        if (goal != null) 
        {
            TextMeshProUGUI textComponent = goal.GetComponent<TextMeshProUGUI>();

            if  (textComponent != null )
            {
                string text = textComponent.text;
                if (enable)
                {
                    textComponent.text = $"<s>{goalBaseText}</s>";
                } else
                {
                    textComponent.text = $"{goalBaseText}";
                }
            }
        }
    }

    private void SetGoalTextColor(Color color)
    {
        if (goal != null)
        {
            TextMeshProUGUI textComponent = goal.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.color = color;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on goal GameObject.");
            }
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