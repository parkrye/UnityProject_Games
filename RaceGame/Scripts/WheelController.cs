using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];   // 휠콜라이더
    GameObject[] wheelMesh = new GameObject[4];             // 차량 모델의 바퀴
    GameObject bodyMesh;                                    // 차량 모델의 몸통

    // Start is called before the first frame update
    void Start()
    {
        SetWheelCollider();
    }

    private void FixedUpdate()
    {
        // 바퀴 위치로 이동한 콜라이더가 차량을 들어올리면
        // 공중에 뜬 바퀴와 차체를 다시 원래 위치(콜라이더 위치)로 복귀시킴
        WheelPosAndAni();
        BodyPosAndAni();
    }

    // 바퀴를 휠콜라이더 위치로 이동시키는 함수
    void WheelPosAndAni()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    void BodyPosAndAni()
    {
        float y = 0;
        for(int i = 0; i < 4; i++)
        {
            y -= wheels[i].transform.localPosition.y;
        }
        bodyMesh.transform.localPosition = new Vector3(0, y/4, 0);
    }

    public void SetWheelCollider()
    {
        // 몸통 모델을 태그를 통해 찾음
        bodyMesh = GameObject.FindGameObjectWithTag("BodyMesh");

        // 바퀴 모델을 태그를 통해 찾음
        wheelMesh = GameObject.FindGameObjectsWithTag("WheelMesh");
        for (int i = 0; i < 4; i++)
        {
            // 휠콜라이더 위치를 바퀴 위치로 이동시킨다
            wheels[i].transform.position = wheelMesh[i].transform.position;
        }
    }
}
