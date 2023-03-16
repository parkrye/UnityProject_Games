using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform look, player;
    Ray ray;                    // ī�޶�� ��� ���� �浹 ���� ����
    RaycastHit hit;             // ī�޶�� ��� ���� �浹 ����

    private void Update()
    {
        ObstacleCheck();
    }

    /// <summary>
    /// ī�޶� �浹 �Լ�
    /// </summary>
    void ObstacleCheck()
    {
        transform.position = look.position + look.up - look.forward * 14;

        // ���� ������ ������κ��� ī�޶��
        ray = new Ray(player.position, transform.position - look.position);
        Debug.DrawRay(player.position, transform.position - look.position, Color.red);
        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.tag != "Player")
            {
                // �浹�� ī�޶� �浹 ��ġ�� �̵�
                transform.position = hit.point;
            }
        }

        transform.LookAt(look);
    }
}
