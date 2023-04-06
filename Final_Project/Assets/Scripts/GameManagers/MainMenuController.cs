using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControlsPanel()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ShowMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Break();
    }
}
