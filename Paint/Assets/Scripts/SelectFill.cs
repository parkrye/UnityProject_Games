using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFill : MonoBehaviour
{
    public CanvasSetting canvasSetting;
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

    public void Fill(Canvas target, Brush brush)
    {
        int origin = target.GetColor();
        int[] location = target.GetLocation();
        int color = brush.GetInk();

        if (origin != color)
        {
            Stack<int[]> dots = new Stack<int[]>();
            dots.Push(location);
            while (dots.Count > 0)
            {
                int[] i = dots.Pop();
                if (i[0] >= 0 && i[0] < 134 && i[1] >= 0 && i[1] < 90)
                {
                    if (canvasSetting.canvas[i[0], i[1]].GetColor() == origin)
                    {
                        canvasSetting.canvas[i[0], i[1]].Paint(color);
                        dots.Push(new int[] { i[0] - 1, i[1] });
                        dots.Push(new int[] { i[0] + 1, i[1] });
                        dots.Push(new int[] { i[0], i[1] - 1 });
                        dots.Push(new int[] { i[0], i[1] + 1 });
                    }
                }
            }
        }
    }
}
