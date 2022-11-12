using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainStageManager : MonoBehaviour
{
    public GameObject[] stages;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("0"))
        {
            PlayerPrefs.SetInt("0", 1);
            PlayerPrefs.SetInt("1", 0);
            PlayerPrefs.SetInt("2", 0);
            PlayerPrefs.SetInt("3", 0);
            PlayerPrefs.SetInt("4", 0);
            PlayerPrefs.SetInt("5", 0);
            PlayerPrefs.SetInt("6", 0);
            PlayerPrefs.SetInt("7", 0);
            PlayerPrefs.SetInt("8", 0);
        }

        for(int i = 0; i < 9; i++)
        {
            if(PlayerPrefs.GetInt(i.ToString()) == 0)
            {
                stages[i].GetComponent<Image>().color = Color.grey;
                stages[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("TitleStage");
    }

    public void OnStageButton()
    {
        string clickButtonName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(clickButtonName);
    }
}
