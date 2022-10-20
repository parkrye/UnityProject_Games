using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Platform_Manager : MonoBehaviour
{
    // 복사할 원본 prefab
    public GameObject prefab_platform;

    // 다음 플랫폼 위치 지정용
    private Vector3 nextPos;

    // 플랫폼 좌우 범위
    private float minDistance;
    private float maxDistance;

    // 플랫폼 상하 초기값, 범위
    private float stdHeight;
    private float minHeight;
    private float maxHeight;

    // 플랫폼 추가
    private int count;
    private bool flag;
    private float endTime;
    private float nowTime;

    // Start is called before the first frame update
    void Awake()
    {
        // 최초 플랫폼 위치 저장
        nextPos = prefab_platform.transform.position;

        minDistance = 1.0f;
        maxDistance = 3.0f;
        minHeight = -1.0f;
        maxHeight = 1.0f;

        stdHeight = nextPos.y;

        count = 100;
        flag = false;
        endTime = 1.0f;
        nowTime = 0.0f;
    }

    private void FixedUpdate()
    {
        nowTime += Time.deltaTime;

        if (nowTime >= endTime & (flag & count > 0))
        {
            AddNewPlatform();
            nowTime = 0.0f;
            count--;
        }
    }

    public void SetFlag()
    {
        flag = true;
    }

    private void AddNewPlatform()
    {
        // Instantiate 함수에 넣어준 prefab과 같은 GameObject를 생성
        GameObject added_Platform = Instantiate(prefab_platform);

        // 생성된 플랫폼 위치를 다음 위치로 조정
        added_Platform.transform.position = new Vector3(nextPos.x + prefab_platform.GetComponent<BoxCollider2D>().size.x + RandomRange(minDistance, maxDistance), stdHeight + RandomRange(minHeight, maxHeight), 0);

        //최근 플랫폼 위치에 생성된 플랫폼 위치 저장
        nextPos = added_Platform.transform.position;
    }

    private float RandomRange(float a, float b)
    {
        return Random.Range(a, b);
    }
}
