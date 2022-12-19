using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : MonoBehaviour
{
    public GameObject dotPrefab;
    public Canvas[,] canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = new Canvas[134, 90];
        for (int i = 0; i < 134; i++)
        {
            for (int j = 0; j < 90; j++)
            {
                GameObject tmp = Instantiate(dotPrefab);
                tmp.transform.position = new Vector3((i / 10f) - 6.65f, (j / 10f) - 4.95f, 10f);
                canvas[i, j] = tmp.GetComponent<Canvas>();
                canvas[i, j].SetLocation(i, j);
            }
        }
    }
}
