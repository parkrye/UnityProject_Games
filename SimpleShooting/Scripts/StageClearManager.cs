using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    public GameObject obj;
    static bool enb;

    private void Start()
    {
        enb = false;
        obj.SetActive(enb);
    }

    private void FixedUpdate()
    {
        if (enb)
        {
            ClearScreen();
            enb = false;
        }
    }

    public static void StageClear()
    {
        enb = true;
    }

    void ClearScreen()
    {
        obj.SetActive(enb);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name.Substring(5), 1);
        Time.timeScale = 0f;
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("MainStage");
        Time.timeScale = 1f;
    }
}
