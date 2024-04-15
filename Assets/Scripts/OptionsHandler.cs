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

    private GameObject[] menus;

    void Start()
    {
        // Get organs original position
        organs = GameObject.FindGameObjectsWithTag("Organ");
        foreach (var organ in organs)
        {
            if (organ != null)
            {
                originalPositions[organ] = organ.transform.position;
                originalRotations[organ] = organ.transform.rotation;
            }
        }

        // Get original goal texts
        goals = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var goal in goals)
        {
            string text = goal.GetComponent<TextMeshProUGUI>().text;
            goalsBaseText[goal] = text;
        }

        // Fetch all menus
        menus = GameObject.FindGameObjectsWithTag("Menu");
        Debug.Log($"Menus found: {menus.Length}");
        // Hide one of them
        var secondMenu = menus[1];
        if (secondMenu != null)
        {
            var menuState = secondMenu.GetComponent<MenuState>();
            if (menuState != null)
            {
                secondMenu.SetActive(false);
                menuState.isActive = false;
            }
        }
    }

    public void ResetGame(bool gravity)
    {
        // Reset position of organs
        foreach (var organ in organs)
        {
            if (organ != null && originalPositions.ContainsKey(organ) && originalRotations.ContainsKey(organ)) 
            { 
                Rigidbody rb = organ.GetComponent<Rigidbody>();
                rb.isKinematic = !gravity;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

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

    public void ToggleMenuType()
    {
        StartCoroutine(ToggleMenuTypeRoutine());
    }

    IEnumerator ToggleMenuTypeRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (var menu in menus)
        {
            var menuState = menu.GetComponent<MenuState>();
            if (menuState != null)
            {
                menu.SetActive(!menuState.isActive);
                menuState.isActive = !menuState.isActive;
            }
        }
    }

    public void QuitToMainMenu()
    {
        SceneTransitionManager.singleton.GoToScene(0);
    }
}
