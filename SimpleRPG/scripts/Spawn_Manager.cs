using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    // 에너미 리스폰 범위
    public Transform prev;
    public Transform next;
    private Vector3 spawnPosition;

    // 에너미, 최대 수, 리스폰 시간, 리스폰 높이
    public GameObject enemyPrefab;
    public int maxEnemyCount;
    public float createTime;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        int count = 0;
        while(count < 10000)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(enemyCount < maxEnemyCount)
            {
                float pointX = Random.Range(prev.position.x + 10f, next.position.x - 10f);
                float pointY = prev.position.y + height;
                spawnPosition = new Vector3(pointX, pointY, 1.0f);

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(createTime);
            }
            else
            {
                yield return null;
            }
        }
    }
}
