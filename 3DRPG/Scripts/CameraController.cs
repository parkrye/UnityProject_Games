using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // 카메라가 바라보는 대상(플레이어)
    public float targetY;       // 높이 오프셋

    public float xRotMax;       // x 회전 한도: x는 위아래 회전이기에 한도 없이 회전시 어지러움 유발
    public float rotSpeed;      // 회전 속도
    public float scrollSpeed;   // 스크롤 확대, 축소 속도

    public float distance;      // 현재 거리
    public float minDistance;   // 최소 거리
    public float maxDistance;   // 최대 거리

    float xRot, yRot;           // x, y 회전 각도
    Vector3 targetPos, dir;     // 대상 위치 벡터, 카메라 시선 벡터

    Ray ray;                    // 카메라와 대상 사이 충돌 감지 광선
    RaycastHit hit;             // 카메라와 대상 사이 충돌 정보

    private void Start()
    {
        // 커서 숨김 및 잠금
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraMove();
        ObstacleCheck();
    }

    /// <summary>
    /// 카메라 이동 함수
    /// </summary>
    void CameraMove()
    {
        // 마우스 좌표로 카메라 위치, 스크롤링으로 대상과의 거리 측정
        xRot += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        // x 회전각, 거리는 한도에 맞게 수정
        xRot = Mathf.Clamp(xRot, -xRotMax, xRotMax);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // 높이 오프셋 적용한 위치를 특정
        targetPos = target.position + Vector3.up * targetY;

        // 수정한 값으로 카메라 위치 조정
        dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
        transform.position = targetPos + dir * -distance;
    }

    /// <summary>
    /// 카메라 충돌 함수
    /// </summary>
    void ObstacleCheck()
    {
        // 광선 방향은 대상으로부터 카메라로
        ray = new Ray(targetPos, (transform.position + Vector3.back * minDistance) - targetPos);
        Debug.DrawRay(targetPos, ((transform.position + Vector3.back * minDistance) - targetPos) * distance, Color.red);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if(hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Weapon")
            {
                // 충돌시 카메라를 충돌 위치로 이동
                transform.position = hit.point;
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(targetPos);
    }
}
