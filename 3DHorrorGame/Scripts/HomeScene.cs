using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    public GameObject[] canvases;
    public enum Mode {normal, infinite};
    public Mode mode;
    public Slider slider;

    // Start is called before the first frame update
    public void OnNormalStart()
    {
        mode = Mode.normal;
        canvases[0].SetActive(false);
        canvases[1].SetActive(true);
    }

    public void OnInfiniteStart()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        mode = Mode.infinite;
        canvases[0].SetActive(false);
        canvases[1].SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit(0);
    }

    public void OnBack()
    {
        canvases[1].SetActive(false);
        canvases[0].SetActive(true);
    }

    public void OnEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        StartGame();
    }

    public void OnNormal()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        StartGame();
    }

    public void OnHard()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        StartGame();
    }

    void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;

        canvases[1].SetActive(false);
        canvases[2].SetActive(true);

        if (mode == Mode.normal)
        {
            StartCoroutine(SceneLoading("1"));
        }
        else
        {
            StartCoroutine(SceneLoading("2"));
        }
    }

    IEnumerator SceneLoading(string num)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Scene_0" + num);
        operation.allowSceneActivation = false;

        float time = 0f;

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
            slider.value = time;

            if(time >= 10f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
