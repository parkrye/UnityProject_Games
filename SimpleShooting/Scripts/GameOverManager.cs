using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject obj;
    static bool over;

    // Start is called before the first frame update
    void Start()
    {
        over = false;
        obj.SetActive(over);
    }

    private void FixedUpdate()
    {
        if (over)
        {
            GameOverScreen();
            over = false;
        }
    }

    public static void GameOver()
    {
        over = true;
    }

    void GameOverScreen()
    {
        obj.SetActive(over);
        Time.timeScale = 0f;
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("MainStage");
        Time.timeScale = 1f;
    }

}
