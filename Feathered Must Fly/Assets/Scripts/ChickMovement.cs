using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, 0.0005f);
    }
}
