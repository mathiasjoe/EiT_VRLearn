using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsHandler : MonoBehaviour
{

    private GameObject[] organs;
    private GameObject[] goals;

    private Dictionary<GameObject, string> goalsBaseText = new Dictionary<GameObject, string>();

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

        goals = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var goal in goals)
        {
            string text = goal.GetComponent<TextMeshProUGUI>().text;
            goalsBaseText[goal] = text;
        }
    }

    public void ResetGame()
    {
        // Reset position of organs
        foreach (var organ in organs)
        {
            if (organ != null && originalPositions.ContainsKey(organ) && originalRotations.ContainsKey(organ)) 
            { 
                Rigidbody rb = organ.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;

                organ.transform.position = originalPositions[organ];
                organ.transform.rotation = originalRotations[organ];
            }
        }

        // Reset checklist
        foreach (var goal in goals)
        {
            TextMeshProUGUI textComponent = goal.GetComponent<TextMeshProUGUI>();
            if (textComponent != null && goalsBaseText.ContainsKey(goal))
            {
                textComponent.color = Color.white;
                textComponent.text = $"{goalsBaseText[goal]}";
            }
        }
    }
}
