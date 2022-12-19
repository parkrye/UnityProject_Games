using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public int color = 1;
    SpriteRenderer ink;
    Vector3 mousePos, transPos;
    public GameObject[] brushType = new GameObject[2];

    private void Start()
    {
        ink = gameObject.GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        ColorChange(1);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(transPos.x, transPos.y, 0);
    }

    public void ChangeBrushType(int type)
    {
        if(type == 0)
        {
            brushType[0].SetActive(true);
            brushType[1].SetActive(false);
        }
        else
        {
            brushType[0].SetActive(false);
            brushType[1].SetActive(true);
        }
    }

    public void ColorChange(int c)
    {
        color = c;
        switch (color)
        {
            default:
            case 0:
                ink.color = Color.white;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 1:
                ink.color = Color.black;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case 2:
                ink.color = Color.red;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                ink.color = Color.blue;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 4:
                ink.color = Color.yellow;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 5:
                ink.color = Color.green;
                brushType[1].GetComponent<SpriteRenderer>().color = Color.green;
                break;
        }
    }

    public int GetInk()
    {
        return color;
    }
}
