using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScene_Button : MonoBehaviour
{
    public GameObject statusCanvas;

    public Text level;
    public Text exp;
    public Text str;
    public Text dex;
    public Text con;
    public Text point;

    public GameObject player;
    private int[] data;

    private void Start()
    {
        statusCanvas.SetActive(false);
        data = new int[] { PlayerPrefs.GetInt("level"), PlayerPrefs.GetInt("exp"), PlayerPrefs.GetInt("str"), PlayerPrefs.GetInt("dex"), PlayerPrefs.GetInt("con"), PlayerPrefs.GetInt("point") };
        UpdateCanvas();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateCanvas();
        statusCanvas.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        statusCanvas.SetActive(false);
        player.GetComponent<Player_Controller>().SetData(data);
    }

    public void OnSTRUp()
    {
        if(data[5] > 0)
        {
            data[5] -= 1;
            data[2] += 1;
            UpdateCanvas();
        }
    }

    public void OnDEXUp()
    {
        if (data[5] > 0)
        {
            data[5] -= 1;
            data[3] += 1;
            UpdateCanvas();
        }
    }

    public void OnCONUp()
    {
        if (data[5] > 0)
        {
            data[5] -= 1;
            data[4] += 1;
            UpdateCanvas();
        }
    }

    private void UpdateCanvas()
    {
        level.text = "LEVEL : " + data[0];
        exp.text = "EXP : " + data[1] + " / " + data[0] * 100;
        str.text = "STR : " + data[2];
        dex.text = "DEX : " + data[3];
        con.text = "CON : " + data[4];
        point.text = "pt : " + data[5];
    }
}
