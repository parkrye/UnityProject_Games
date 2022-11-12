using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public float fixX;
    public float fixY;
    public float randX;
    public float randY;

    float spawnX;
    float spawnY;
    bool generate;

    // Start is called before the first frame update
    void Start()
    {
        generate = true;
        StartCoroutine(Targeting());
    }

    IEnumerator Targeting()
    {
        while (generate)
        {

            spawnX = Random.Range(fixX - randX, fixX + randX);
            spawnY = Random.Range(fixY - randY, fixY + randY);
            Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 2f), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
