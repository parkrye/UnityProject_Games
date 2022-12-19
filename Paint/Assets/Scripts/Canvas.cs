using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    SpriteRenderer paint;
    int color;
    int[] location;

    // Start is called before the first frame update
    void Awake()
    {
        paint = gameObject.GetComponent<SpriteRenderer>();
        color = 0;
        location = new int[2] { 0, 0 };
    }

    public void Paint(int c)
    {
        color = c;
        switch (c)
        {
            case 0:
                paint.color = Color.white;
                break;
            case 1:
                paint.color = Color.black;
                break;
            case 2:
                paint.color = Color.red;
                break;
            case 3:
                paint.color = Color.blue;
                break;
            case 4:
                paint.color = Color.yellow;
                break;
            case 5:
                paint.color = Color.green;
                break;
        }
    }

    public int GetColor()
    {
        return color;
    }

    public void SetLocation(int row, int col)
    {
        location[0] = row;
        location[1] = col;
    }

    public int[] GetLocation()
    {
        return location;
    }
}
