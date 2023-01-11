using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Text[] menus = new Text[2];
    public AudioSource[] audios;
    int cursor;

    void Start()
    {
        cursor = 0;
        menus[cursor].color = Color.white;
        menus[1 - cursor].color = Color.grey;
    }

    void Update()
    {
        CursorMove();
        CursorClick();
    }

    void CursorMove()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s") || Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            cursor = 1 - cursor;
            audios[0].Play();
        }
        menus[cursor].color = Color.white;
        menus[1 - cursor].color = Color.grey;
    }

    void CursorClick()
    {
        if (Input.GetKeyDown("return") ||Input.GetKeyDown("space"))
        {
            audios[1].Play();
            if (cursor == 0)
            {
                SceneManager.LoadScene("01_Map");
            }
            else
            {
                Application.Quit(0);
            }
        }
    }
}
