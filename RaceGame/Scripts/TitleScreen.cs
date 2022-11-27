using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Rank"))
        {
            PlayerPrefs.SetInt("Rank", 1);
        }
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Track01");
    }

    public void OnQuitButton()
    {
        Application.Quit(0);
    }
}
