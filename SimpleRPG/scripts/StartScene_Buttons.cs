using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene_Buttons : MonoBehaviour
{
    public Text load;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("map"))
        {
            load.color = Color.grey;
        }
    }

    // 새 게임
    public void OnClickNewStart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("01_MainScene00");
    }
    
    // 데이터 불러오기
    public void OnClickLoadSave()
    {
        if (PlayerPrefs.HasKey("map"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("map"));
        }
    }

    // 게임 종료
    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}
