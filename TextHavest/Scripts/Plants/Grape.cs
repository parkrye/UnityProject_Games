using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : Plant
{
    private void Start()
    {
        id = 15;
        label = "Æ÷µµ";
    }

    public override void Initialize()
    {
        growth = 1;
        limAge = 12;
    }
}
