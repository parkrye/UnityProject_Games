using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant
{
    private void Start()
    {
        id = 0;
        label = "´ç±Ù";
    }
    public override void Initialize()
    {
        growth = 49;
        limAge = 19;
    }
}
