using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    GameObject _optionsPanel;
    GameObject _helpPanel;

    public void OnStartButton()
    {

    }

    public void OnOptionsButton()
    {
        _helpPanel.SetActive(false);
        _optionsPanel.SetActive(!_optionsPanel.activeSelf);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnHelpButton()
    {
        _optionsPanel.SetActive(false);
        _helpPanel.SetActive(!_helpPanel.activeSelf);
    }
}
