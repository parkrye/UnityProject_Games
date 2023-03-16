using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform look, player;
    Ray ray;                    // 카메라와 대상 사이 충돌 감지 광선
    RaycastHit hit;             // 카메라와 대상 사이 충돌 정보

    private void Update()
    {
        ObstacleCheck();
    }

    /// <summary>
    /// 카메라 충돌 함수
    /// </summary>
    void ObstacleCheck()
    {
        transform.position = look.position + look.up - look.forward * 14;

        // 광선 방향은 대상으로부터 카메라로
        ray = new Ray(player.position, transform.position - look.position);
        Debug.DrawRay(player.position, transform.position - look.position, Color.red);
        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.tag != "Player")
            {
                // 충돌시 카메라를 충돌 위치로 이동
                transform.position = hit.point;
            }
        }

        transform.LookAt(look);
    }
}
