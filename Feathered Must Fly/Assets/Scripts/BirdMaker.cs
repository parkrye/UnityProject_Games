using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMaker : MonoBehaviour
{
    public GameObject birdPrefab;

    private void Awake()
    {
        if (BirdMove.Player == null)
        {
            Instantiate(birdPrefab);
        }
    }
}
