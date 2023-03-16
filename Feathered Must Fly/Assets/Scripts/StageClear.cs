using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public int stage;
    public GameObject pauseUI, clearUI, overUI;

    // Start is called before the first frame update
    void Start()
    {
        if(stage < 1) stage = 1;
        else if(stage > 8) stage = 8;

        if (pauseUI) pauseUI.SetActive(false);
        if (clearUI) clearUI.SetActive(false);
        if (overUI) overUI.SetActive(false);
    }

    private void Update()
    {
        if (!clearUI.activeSelf && !overUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseUI.activeSelf)
                {
                    BirdMove.Player.UseCursor(false);
                    pauseUI.SetActive(false);
                }
                else
                {
                    BirdMove.Player.UseCursor(true);
                    pauseUI.SetActive(true);
                }
            }
        }
    }

    public int GetStage()
    {
        return stage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (PlayerPrefs.GetInt("Stage") < stage) PlayerPrefs.SetInt("Stage", stage);
            BirdMove.Player.UseCursor(true);
            clearUI.SetActive(true);
        }
    }

    public void OnGameOverEnter()
    {
        BirdMove.Player.UseCursor(true);
        overUI.SetActive(true);
    }

    public void OnRetryButton()
    {
        BirdMove.Player.ShowWaitImage();
        BirdMove.Player.UseCursor(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnHomeButton()
    {
        BirdMove.Player.ShowWaitImage();
        BirdMove.Player.UseCursor(false);
        SceneManager.LoadScene("HomeScene");
    }

    public void OnNextButton()
    {
        BirdMove.Player.ShowWaitImage();
        BirdMove.Player.UseCursor(false);
        if (stage < 8) SceneManager.LoadScene("Stage" + stage);
        else
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
