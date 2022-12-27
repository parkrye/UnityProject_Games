using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Plant
{
    private void Start()
    {
        id = 1;
        label = "¾ç¹èÃß";
    }

    public override void Initialize()
    {
        growth = 44;
        limAge = 19;
    }
}
