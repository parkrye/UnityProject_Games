using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    int level;

    public Material[] materials = new Material[2];      // »¡°­, ³ì»ö
    public MeshRenderer[] lights = new MeshRenderer[2]; // ¼­ÀÖ´ÂÂÊ, ¹Ý´ñÆí
    public MeshRenderer[] carLights = new MeshRenderer[4];
    public BoxCollider[] dangers = new BoxCollider[2];
    public GameObject closer;

    public float redTime, greenTime;
    public int mode;

    new Material[] light;
    Material[] carLight;

    bool red;

    public void Setting(int _level)
    {
        level = _level;
        if (lights[0])
        {
            light = lights[0].materials;
            if(carLights[0])
                carLight = carLights[0].materials;
            SetColor(mode, 0);
            StartCoroutine(ColorChange());
        }
    }

    IEnumerator ColorChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(redTime);
            SetColor(mode, 1);
            yield return new WaitForSeconds(greenTime);
            SetColor(mode, 0);
        }
    }

    public void SetColor(int _mode = 0, int color = 0)
    {
        switch (_mode)
        {
            default:
            case 0:
                light[1] = materials[color];
                lights[0].materials = light;
                lights[1].materials = light;
                break;
            case 1:
                light[1] = materials[color];
                lights[0].materials = light;
                break;
            case 2:
                carLight[4] = materials[color];
                carLights[0].materials = carLight;
                carLights[1].materials = carLight;
                carLights[2].materials = carLight;
                carLights[3].materials = carLight;
                break;
        }

        if (color == 0)
        {
            red = true;
            dangers[0].enabled = true;
            dangers[1].enabled = true;
        }
        else
        {
            red = false;
            dangers[0].enabled = false;
            dangers[1].enabled = false;
        }
    }

    public void HandUp(bool handUp)
    {
        if (!red)
        {
            dangers[0].enabled = !handUp;
            dangers[1].enabled = !handUp;
        }
    }
}
