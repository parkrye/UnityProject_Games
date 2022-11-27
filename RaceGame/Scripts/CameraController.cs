using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject cameraPos;
    public GameObject playerPos;

    // Update is called once per frame
    void LateUpdate()
    {
        // 카메라 위치를 지정한 위치로 이동
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        // 카메라가 플레이어를 바라보도록
        gameObject.transform.LookAt(playerPos.transform);
    }
}
