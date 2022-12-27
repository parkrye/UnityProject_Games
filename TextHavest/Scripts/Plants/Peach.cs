using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peach : Plant
{
    private void Start()
    {
        id = 10;
        label = "º¹¼þ¾Æ";
    }

    public override void Initialize()
    {
        growth = 18;
        limAge = 14;
    }
}
