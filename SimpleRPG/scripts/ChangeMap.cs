using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMap : MonoBehaviour
{
    public GameObject player;
    private string nowMap;
    private string index;
    private string objectName;

    private void Start()
    {
        nowMap = SceneManager.GetActiveScene().name;
        index = (nowMap[12]).ToString() + (nowMap[13]).ToString();
        objectName = gameObject.name;
        if (index != "00")
        {

            if (objectName == "Prev")
            {
                try
                {
                    if (PlayerPrefs.GetInt("location") == 0)
                    {
                        player.GetComponent<SpriteRenderer>().flipX = false;
                        player.GetComponent<Transform>().position = new Vector3(2f, 0.2f, 1f);
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().flipX = true;
                        player.GetComponent<Transform>().position = new Vector3(88f, 0.2f, 1f);
                    }
                }
                catch
                {
                    player.GetComponent<Transform>().position = new Vector3(2f, 0.2f, 1f);
                }
            }
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            player.GetComponent<Transform>().position = new Vector3(0f, 2f, 1f);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (objectName == "Prev")
        {
            PlayerPrefs.SetInt("location", 1);
            if (index == "01")
            {
                player.GetComponent<Player_Controller>().Heal(-1);
                player.GetComponent<Player_Controller>().SaveData();
                SceneManager.LoadScene("01_MainScene00");
            }
            else
            {
                index = (int.Parse(index)-1).ToString();
                if(index.Length == 1)
                {
                    index = "0" + index;
                }
                player.GetComponent<Player_Controller>().SaveData();
                SceneManager.LoadScene("01_MainScene" + index);
            }
        }
        else if(objectName == "Next")
        {
            PlayerPrefs.SetInt("location", 0);
            index = (int.Parse(index) + 1).ToString();
            if (index.Length == 1)
            {
                index = "0" + index;
            }
            if (SceneManager.GetSceneByName("01_MainScene" + index) != null)
            {
                player.GetComponent<Player_Controller>().SaveData();
                SceneManager.LoadScene("01_MainScene" + index);
            }
        }
        else
        {
            player.GetComponent<Player_Controller>().Heal(-1);
            player.GetComponent<Player_Controller>().SaveData();
            PlayerPrefs.SetInt("location", 0);
            SceneManager.LoadScene("01_MainScene01");
        }
    }
}
