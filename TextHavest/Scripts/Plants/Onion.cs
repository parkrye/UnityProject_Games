using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : Plant
{
    private void Start()
    {
        id = 2;
        label = "¾çÆÄ";
    }

    public override void Initialize()
    {
        growth = 41;
        limAge = 18;
    }
}
