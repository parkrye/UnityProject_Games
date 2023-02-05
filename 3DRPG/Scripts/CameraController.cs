using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // ī�޶� �ٶ󺸴� ���(�÷��̾�)
    public float targetY;       // ���� ������

    public float xRotMax;       // x ȸ�� �ѵ�: x�� ���Ʒ� ȸ���̱⿡ �ѵ� ���� ȸ���� �������� ����
    public float rotSpeed;      // ȸ�� �ӵ�
    public float scrollSpeed;   // ��ũ�� Ȯ��, ��� �ӵ�

    public float distance;      // ���� �Ÿ�
    public float minDistance;   // �ּ� �Ÿ�
    public float maxDistance;   // �ִ� �Ÿ�

    float xRot, yRot;           // x, y ȸ�� ����
    Vector3 targetPos, dir;     // ��� ��ġ ����, ī�޶� �ü� ����

    Ray ray;                    // ī�޶�� ��� ���� �浹 ���� ����
    RaycastHit hit;             // ī�޶�� ��� ���� �浹 ����

    private void Start()
    {
        // Ŀ�� ���� �� ���
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraMove();
        ObstacleCheck();
    }

    /// <summary>
    /// ī�޶� �̵� �Լ�
    /// </summary>
    void CameraMove()
    {
        // ���콺 ��ǥ�� ī�޶� ��ġ, ��ũ�Ѹ����� ������ �Ÿ� ����
        xRot += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        // x ȸ����, �Ÿ��� �ѵ��� �°� ����
        xRot = Mathf.Clamp(xRot, -xRotMax, xRotMax);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // ���� ������ ������ ��ġ�� Ư��
        targetPos = target.position + Vector3.up * targetY;

        // ������ ������ ī�޶� ��ġ ����
        dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
        transform.position = targetPos + dir * -distance;
    }

    /// <summary>
    /// ī�޶� �浹 �Լ�
    /// </summary>
    void ObstacleCheck()
    {
        // ���� ������ ������κ��� ī�޶��
        ray = new Ray(targetPos, (transform.position + Vector3.back * minDistance) - targetPos);
        Debug.DrawRay(targetPos, ((transform.position + Vector3.back * minDistance) - targetPos) * distance, Color.red);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if(hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Weapon")
            {
                // �浹�� ī�޶� �浹 ��ġ�� �̵�
                transform.position = hit.point;
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(targetPos);
    }
}
