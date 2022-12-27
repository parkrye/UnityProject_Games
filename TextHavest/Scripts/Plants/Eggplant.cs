using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggplant : Plant
{
    private void Start()
    {
        id = 14;
        label = "°¡Áö";
    }

    public override void Initialize()
    {
        growth = 5;
        limAge = 12;
    }
}
