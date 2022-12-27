using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Plant
{
    private void Start()
    {
        id = 6;
        label = "»ç°ú";
    }

    public override void Initialize()
    {
        growth = 29;
        limAge = 16;
    }
}
