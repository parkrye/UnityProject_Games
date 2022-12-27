using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : Plant
{
    private void Start()
    {
        id = 4;
        label = "±Ö";
    }

    public override void Initialize()
    {
        growth = 36;
        limAge = 17;
    }
}
