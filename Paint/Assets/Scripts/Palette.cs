using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public int color;
    public Brush brush;

    public void UsePalette()
    {
        brush.ColorChange(color);
    }
}
