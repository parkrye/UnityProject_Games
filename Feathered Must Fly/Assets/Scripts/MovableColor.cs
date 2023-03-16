using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableColor : MonoBehaviour
{
    public Material[] materials;
    MeshRenderer[] parts;

    // Start is called before the first frame update
    void Start()
    {
        parts = GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < parts.Length; i++)
        {
            parts[i].material = materials[Random.Range(0, materials.Length)];
        }
    }
}
