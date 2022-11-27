using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemsLocation;
    Transform[] points;
    bool[] exist;
    int size;
    public GameObject[] items = new GameObject[3];

    bool spawn = true;
    int count = 0;
    int max = 15;
    int charge = 0;
    int order = 0;

    // Start is called before the first frame update
    void Start()
    {
        points = itemsLocation.GetComponentsInChildren<Transform>();
        size = points.Length;
        exist = Enumerable.Repeat(false, size).ToArray();

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (spawn)
        {
            if(count < max)
            {
                if(charge >= size - 10)
                {
                    exist = Enumerable.Repeat(false, size).ToArray();
                }

                int here = Random.Range(1, size);
                if (!exist[here])
                {
                    exist[here] = true;
                    count += 1;
                    charge += 1;
                    switch (order)
                    {
                        case 0:
                            Instantiate(items[0], points[here]);
                            order = 1;
                            break;
                        case 1:
                            Instantiate(items[1], points[here]);
                            order = 2;
                            break;
                        case 2:
                            Instantiate(items[2], points[here]);
                            order = 0;
                            break;
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ItemUsed()
    {
        count -= 1;
    }
}
