using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuVisibility : MonoBehaviour
{
    public Transform palmTransform;
    public Camera mainCamera;
    public GameObject menu;
    public float visibilityThreshold = 45f;

    // Update is called once per frame
    void Update()
    {
        Vector3 palmToCameraVector = (mainCamera.transform.position - palmTransform.position).normalized;
        float dotProduct = Vector3.Dot(palmTransform.up, palmToCameraVector.normalized);

        float angleToCamera = Vector3.Angle(palmTransform.up, palmToCameraVector);

        bool isPalmFacingCamera = dotProduct > 0;

        var menuState = menu.GetComponent<MenuState>();

        if (menuState != null && menuState.isActive)
        {
            if (isPalmFacingCamera && angleToCamera < visibilityThreshold)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            } 
        }

    }
}
