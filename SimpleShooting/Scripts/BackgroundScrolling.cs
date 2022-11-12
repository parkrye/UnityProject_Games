using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public Transform[] backgrounds;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Transform bg in backgrounds)
        {
            bg.Translate(0f, -speed, 0f);
            if(bg.position.y < -11.5f)
            {
                bg.position = new Vector3(bg.position.x, bg.position.y + 24f, bg.position.z);
            }
        }
    }
}
