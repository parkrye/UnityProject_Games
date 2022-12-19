using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBrush : MonoBehaviour
{
    SpriteRenderer ink;

    private void Start()
    {
        ink = gameObject.GetComponent<SpriteRenderer>();
        Selected(1);
    }

    public void Selected(int c)
    {
        switch (c)
        {
            default:
            case 0:
                ink.color = Color.white;
                break;
            case 1:
                ink.color = Color.black;
                break;
            case 2:
                ink.color = Color.red;
                break;
            case 3:
                ink.color = Color.blue;
                break;
            case 4:
                ink.color = Color.yellow;
                break;
            case 5:
                ink.color = Color.green;
                break;
        }
    }
}
