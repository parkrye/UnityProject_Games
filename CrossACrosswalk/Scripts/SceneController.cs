using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int level;
    TrafficLight traffic;

    public GameObject goalUI, retryUI;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        traffic = GetComponent<TrafficLight>();
        traffic.Setting(level);
    }

    public void OnGoal()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.GetInt("Level") < level)
        {
            PlayerPrefs.SetInt("Level", level);
        }
        goalUI.SetActive(true);
    }

    public void OnKick()
    {
        StartCoroutine(Delay(10f));
    }

    void RetryUI()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        retryUI.SetActive(true);
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RetryUI();
    }

    public void OnNextButton()
    {
        if(level < 5)
        {
            SceneManager.LoadScene("Stage" + (level + 1));
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("Stage" + level);
    }
}
