using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Plant
{
    private void Start()
    {
        id = 5;
        label = "°¨ÀÚ";
    }

    public override void Initialize()
    {
        growth = 32;
        limAge = 17;
    }
}
