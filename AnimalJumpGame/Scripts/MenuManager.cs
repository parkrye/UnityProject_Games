using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text[] menus = new Text[4];  // 취소, 키 확인, 처음부터, 뒤로 가기 4개 기능
    public GameObject keyPrefab;        // 키 배치 UI
    int cursor;                         // 커서
    bool keyCanvasOn;                   // 키 배치 UI가 생성되어 있는지 여부
    public AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        cursor = 0;
        for(int i = 0; i < 4; i++)
        {
            if(i == cursor)
            {
                menus[i].color = Color.white;
            }
            else
            {
                menus[i].color = Color.grey;
            }
        }
        keyCanvasOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyCanvasOn)
        {
            CursorMove();
            CursorClick();
        }
        else
        {
            KeyCanvasOut();
        }
    }

    // 커서 이동. 커서가 위치한 텍스트는 하얗게. 상하 범위 벗어날 시 반댓편으로 이동
    void CursorMove()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            audios[0].Play();
            cursor++;
            if(cursor > 3)
            {
                cursor = 0;
            }
        }
        else if(Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            audios[0].Play();
            cursor--;
            if (cursor < 0)
            {
                cursor = 3;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == cursor)
            {
                menus[i].color = Color.white;
            }
            else
            {
                menus[i].color = Color.grey;
            }
        }
    }

    // 커서가 위치한 메뉴 클릭시 이벤트 실행
    void CursorClick()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            audios[1].Play();
            if (cursor == 0)                    // 게임 진행
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                Destroy(gameObject);
            }
            else if(cursor == 1)                // 맵 초기화
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Destroy(gameObject);
            }
            else if (cursor == 2)                // 키 확인
            {
                Instantiate(keyPrefab);
                keyCanvasOn = true;
            }
            else                                // 미니맵으로
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                SceneManager.LoadScene("01_Map");
                Destroy(gameObject);
            }
        }
        else if (Input.GetKeyDown("escape"))    // 게임 진행
        {
            audios[1].Play();
            Time.timeScale = 1f;
            DataManager.Manager.SetMenu(false);
            Destroy(gameObject);
        }
    }

    // 키 확인 UI 사라짐
    void KeyCanvasOut()
    {
        if (Input.anyKeyDown)
        {
            keyCanvasOn = false;
        }
    }
}
