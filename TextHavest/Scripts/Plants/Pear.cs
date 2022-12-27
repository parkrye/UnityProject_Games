using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pear : Plant
{
    private void Start()
    {
        id = 8;
        label = "น่";
    }

    public override void Initialize()
    {
        growth = 25;
        limAge = 15;
    }
}
