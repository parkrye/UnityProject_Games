using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    public int mode;

    GameObject[] spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = GameObject.FindGameObjectsWithTag("Book");
        int[] randList = new int[10];
        for(int i = 0; i < 10; i++)
        {
            randList[i] = Random.Range(0, spawnPos.Length);
            for(int j = 0; j < i; j++)
            {
                if (randList[i] == randList[j])
                {
                    i--;
                    break;
                }
            }
        }

        int count = 0;
        bool inList;
        for(int i = 0; i < spawnPos.Length; i++)
        {
            inList = false;
            spawnPos[i].GetComponent<Collecting>().SetValues(mode);
            if (count < 10)
            {
                for (int j = 0; j < randList.Length; j++)
                {
                    if (i == randList[j])
                    {
                        inList = true;
                        count++;
                        break;
                    }
                }
            }
            if (!inList)
            {
                spawnPos[i].SetActive(false);
            }
        }
    }

    public void SpawnNewBook()
    {
        int randIdx = 0;
        while (spawnPos[randIdx].activeInHierarchy)
        {
            randIdx = Random.Range(0, spawnPos.Length);
        }
        spawnPos[randIdx].SetActive(true);
    }
}
