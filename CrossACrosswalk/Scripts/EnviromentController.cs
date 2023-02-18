using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public Light[] lights;
    bool lightOn;

    // Start is called before the first frame update
    void Start()
    {
        lightOn = false;
        StartCoroutine(Clocking());
    }

    IEnumerator Clocking()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            SunCycle();
        }
    }

    void SunCycle()
    {
        transform.Rotate(-Vector3.right * 0.01f);

        if (transform.eulerAngles.x > 340 && transform.eulerAngles.x < 350)
        {
            if (!lightOn)
            {
                lightOn = true;
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].enabled = lightOn;
                }
            }
        }
        if (transform.eulerAngles.x > 10 && transform.eulerAngles.x < 20)
        {
            if (lightOn)
            {
                lightOn = false;
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].enabled = lightOn;
                }
            }
        }
    }
}
