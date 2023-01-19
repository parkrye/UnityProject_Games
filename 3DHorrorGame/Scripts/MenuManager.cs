using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject inGameCanvas;
    public ObjectManager objectManager;

    public void OnResumeButton()
    {
        objectManager.QuitMenu();
    }

    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene_00");
    }
}
