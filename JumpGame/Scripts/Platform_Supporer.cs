using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Supporer : MonoBehaviour
{
    Vector3 pos;

    public void SetPos(Vector3 vec)
    {
        pos = vec;
    } 

    public Vector3 GetPos()
    {
        return pos;
    }
}
