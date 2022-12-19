using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public CanvasSetting canvasSetting;

    public void Erase()
    {
        for (int i = 0; i < 134; i++)
        {
            for (int j = 0; j < 90; j++)
            {
                canvasSetting.canvas[i, j].Paint(0);
            }
        }
    }
}
