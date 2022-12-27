using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant
{
    private void Start()
    {
        id = 3;
        label = "È£¹Ú";
    }

    public override void Initialize()
    {
        growth = 38;
        limAge = 18;
    }
}
