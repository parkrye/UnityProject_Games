using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColliderManager : MonoBehaviour
{
    public MonsterBase monsterBase; // ���� ��ũ��Ʈ

    /// <summary>
    /// �浹�� ȣ��
    /// </summary>
    /// <param name="collision">�浹�� ��ü</param>
    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹�� ���
        if (collision.gameObject.tag == "Player")
        {
            // ���Ͱ� ���� ���� ���¶��
            if (monsterBase.GetAttack())
            {
                // �÷��̾�� ������
                collision.gameObject.GetComponent<PlayerController>().Attacked(-monsterBase.GetDamage());
            }
        }
        // ����� �浹�� ���
        else if (collision.gameObject.tag == "Weapon")
        {
            // ���Ⱑ ���� ���� ���¶��
            if (collision.gameObject.GetComponent<WeaponData>().GetAttack())
            {
                // ���Ϳ��� ������
                monsterBase.Hit(collision.gameObject.GetComponent<WeaponData>().GetDamage());
            }
        }
    }
}
