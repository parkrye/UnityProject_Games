using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    bool pause;
    public Text text;
    public GameObject retry;
    public GameObject back;

    private void Start()
    {
        pause = false;
        retry.SetActive(false);
        back.SetActive(false);
    }
    public void OnPauseButton()
    {
        if (pause)
        {
            pause = false;
            retry.SetActive(false);
            back.SetActive(false);
            text.text = "=";
            Time.timeScale = 1f;
        }
        else
        {
            pause = true;
            retry.SetActive(true);
            back.SetActive(true);
            text.text = ">";
            Time.timeScale = 0f;
        }
    }

    public void OnRetryButton()
    {
        string stageName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(stageName);
        Time.timeScale = 1f;
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("MainStage");
        Time.timeScale = 1f;
    }
}
