using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmorData : MonoBehaviour
{
    public int aromor;      // ����� ��
    public int weight;      // ����� ����

    /// <summary>
    /// ����� ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>����� ��</returns>
    public int GetArmor()
    {
        return aromor;
    }

    /// <summary>
    /// ����� ���Ը� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>����� ����</returns>
    public int GetWeight()
    {
        return weight;
    }
}