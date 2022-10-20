using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    // 카메라 위치 조정을 위한 변수
    public Transform target;

    // 배경 이동을 위한 변수
    public GameObject background_1;
    public GameObject background_2;
    private Transform backgroundTranform_1;
    private Transform backgroundTranform_2;
    private BoxCollider2D backgroundBox_1;

    private void Start()
    {
        backgroundTranform_1 = background_1.GetComponent<Transform>();
        backgroundTranform_2 = background_2.GetComponent<Transform>();
        backgroundBox_1 = background_1.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = targetPos;

        if(transform.position.x > backgroundTranform_2.position.x)
        {
            background_1.transform.position = new Vector3(backgroundTranform_2.position.x + backgroundBox_1.size.x, backgroundTranform_1.position.y, backgroundTranform_1.position.z);
            SwitchBg();
        }

        if (transform.position.x < backgroundTranform_1.position.x)
        {
            background_2.transform.position = new Vector3(backgroundTranform_1.position.x - backgroundBox_1.size.x, backgroundTranform_1.position.y, backgroundTranform_1.position.z);
            SwitchBg();
        }
    }

    private void SwitchBg()
    {
        Transform tmp = backgroundTranform_1;
        backgroundTranform_1 = backgroundTranform_2;
        backgroundTranform_2 = tmp;
    }
}
