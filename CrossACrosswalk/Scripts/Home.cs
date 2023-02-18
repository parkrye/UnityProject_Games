using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }

    public void OnStartButton()
    {
        if(PlayerPrefs.GetInt("Level") < 5)
        {
            SceneManager.LoadScene("Stage" + (PlayerPrefs.GetInt("Level") + 1));
        }
        else
        {
            SceneManager.LoadScene("Stage1");
        }
    }

    public void OnQuitButton()
    {
        Application.Quit(0);
    }
}
