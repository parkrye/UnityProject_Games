using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public float damage;      // ������ ������
    public float weight;      // ������ ����
    public float speed;       // ������ �ӵ�
    bool attack = false;      // ���� ����

    /// <summary>
    /// ������ �������� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ ������</returns>
    public float GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// ������ ���Ը� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ ����</returns>
    public float GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// ������ �ӵ��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ �ӵ�</returns>
    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// ������ ���� ���¸� �����ϴ� �Լ�
    /// </summary>
    /// <param name="value">���� ���� ��������</param>
    public void Attack(bool value)
    {
        attack = value;
    }

    /// <summary>
    /// ������ ���� ���� ���θ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ ���� ����</returns>
    public bool GetAttack()
    {
        return attack;
    }
}