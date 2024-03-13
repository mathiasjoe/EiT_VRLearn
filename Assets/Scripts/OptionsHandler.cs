using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHandler : MonoBehaviour
{

    private GameObject[] organs;

    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> originalRotations = new Dictionary<GameObject, Quaternion>();

    void Start()
    {
        organs = GameObject.FindGameObjectsWithTag("Organ");

        foreach (var organ in organs)
        {
            if (organ != null)
            {
                originalPositions[organ] = organ.transform.position;
                originalRotations[organ] = organ.transform.rotation;
            }
        }
    }

    public void ResetOrganPosition()
    {
        foreach (var organ in organs)
        {
            if (organ != null && originalPositions.ContainsKey(organ) && originalRotations.ContainsKey(organ)) 
            { 
                organ.transform.position = originalPositions[organ];
                organ.transform.rotation = originalRotations[organ];
            }
        }
    }
}
