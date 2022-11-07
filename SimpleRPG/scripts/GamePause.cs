using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject PauseCanvas;
    public Player_Controller player;
    private bool paused;

    // Start is called before the first frame update
    private void Start()
    {
        PauseCanvas.SetActive(false);
        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    private void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void OnPlayButton()
    {
        Resume();
    }

    public void OnHomeButton()
    {
        Resume();
        player.SaveData();
        SceneManager.LoadScene("00_StartScene");
    }
}
