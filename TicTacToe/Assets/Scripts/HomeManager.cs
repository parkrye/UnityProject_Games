using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Mode", 0);
        PlayerPrefs.SetInt("Turn", 0);
    }

    public void OnPlayerButton()
    {
        GoToGame();
    }

    public void OnLv1Button()
    {
        PlayerPrefs.SetInt("Mode", 1);
        PlayerPrefs.SetInt("Turn", Random.Range(0, 2));
        GoToGame();
    }

    public void OnLv2Button()
    {
        PlayerPrefs.SetInt("Mode", 2);
        PlayerPrefs.SetInt("Turn", Random.Range(0, 2));
        GoToGame();
    }

    public void OnLv3Button()
    {
        PlayerPrefs.SetInt("Mode", 3);
        PlayerPrefs.SetInt("Turn", Random.Range(0, 2));
        GoToGame();
    }

    void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
