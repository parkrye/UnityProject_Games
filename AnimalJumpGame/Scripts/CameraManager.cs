using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] players = new GameObject[2];
    GameObject[] backgrounds;
    GameObject underLine;

    float players_x;

    // Start is called before the first frame update
    void Start()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        underLine = GameObject.FindWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        players_x = (players[0].transform.position.x + players[1].transform.position.x) / 2f;
        if (players_x > transform.position.x)
        {
            transform.position = new Vector3(players_x, 0f, -10f);
            underLine.transform.position = new Vector3(players_x, -7f, 0f);
        }

        if (transform.position.x >= backgrounds[1].transform.position.x)
        {
            backgrounds[0].transform.Translate(16f, 0f, 0f);
            GameObject temp = backgrounds[0];
            backgrounds[0] = backgrounds[1];
            backgrounds[1] = temp;

        }
    }

    public void GoToPlayers()
    {
        players_x = (players[0].transform.position.x + players[1].transform.position.x) / 2f;
        transform.position = new Vector3(players_x, 0f, -10f);
        underLine.transform.position = new Vector3(players_x, -7f, 0f);

        backgrounds[0].transform.position = new Vector3(players_x, -1f, 10f);
        backgrounds[1].transform.position = new Vector3(players_x + 16f, -1f, 10f);
    }
}
