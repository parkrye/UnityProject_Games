using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public TutorialManager tutorialManager;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnTutorialButton()
    {
        tutorialManager.StartTutorial();
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnResetButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
