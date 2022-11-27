using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelect : MonoBehaviour
{
    public GameObject[] cars = new GameObject[8];
    public GameObject[] player = new GameObject[8];
    public GameObject[] lines = new GameObject[8];
    public WheelController wheelController;
    public PlayerContoller playerContoller;
    public GameObject[] walls = new GameObject[8];
    public RankSystem rankSystem;

    // Start is called before the first frame update
    void Start()
    {
        int rank = PlayerPrefs.GetInt("Rank");
        if (rank >= 8)
            walls[7].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 7)
            walls[6].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 6)
            walls[5].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 5)
            walls[4].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 4)
            walls[3].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 3)
            walls[2].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 2)
            walls[1].transform.Translate(0f, 1.63f, 0f);
        if (rank >= 1)
        {
            walls[0].transform.Translate(0f, 1.63f, 0f);
        }

        SetFlag(0);
    }

    // Update is called once per frame
    public void SetFlag(int flag)
    {
        for (int i = 0; i < 8; i++)
        {
            if (i != flag)
            {
                cars[i].SetActive(true);
                player[i].SetActive(false);
                lines[i].SetActive(true);
            }
            else
            {
                cars[i].SetActive(false);
                player[i].SetActive(true);
                lines[i].SetActive(false);
            }
        }
        wheelController.SetWheelCollider();
        playerContoller.SetCarStatus(flag);
        playerContoller.SetLocation();
        rankSystem.Reset();
    }
}
