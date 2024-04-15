using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuHandler : MonoBehaviour
{

    public GameObject menu;
    public GameObject aboutCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneTransitionManager.singleton.GoToScene(1);
    }

    public void StartLearn()
    {
        SceneTransitionManager.singleton.GoToScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowAbout()
    {
        menu.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    public void HideAbout()
    {
        aboutCanvas.SetActive(false);
        menu.SetActive(true);
    }
}
