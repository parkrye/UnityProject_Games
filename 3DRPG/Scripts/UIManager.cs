using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float nowHP, maxHP;          // ���� ü��, �ִ� ü��
    public float nowSP, maxSP;          // ���� ���, �ִ� ���
    public float nowEXP, maxEXP;        // ���� ����ġ, �ִ� ����ġ
    public int level;                   // ����
    public Slider barHP, barSP, barEXP; // ü��, ���, ����ġ ui

    /// <summary>
    /// ui �ʱ� ���� �Լ�
    /// </summary>
    /// <param name="_level">���� ����</param>
    public void SetSlider(int _level)
    {
        level = _level;
        maxHP = 100 + (level - 1) * 10;
        nowHP = maxHP;
        maxSP = 100 + (level - 1) * 10;
        nowSP = maxSP;
        maxEXP = level * 100;
        nowEXP = 0;
    }

    /// <summary>
    /// ü�� ui ���� �Լ�
    /// </summary>
    /// <param name="value">���� ü��</param>
    public void ModifyHP(float value)
    {
        nowHP = value;
        barHP.value = nowHP / maxHP;
    }


    /// <summary>
    /// ��� ui ���� �Լ�
    /// </summary>
    /// <param name="value">���� ���</param>
    public void ModifySP(float value)
    {
        nowSP = value;
        barSP.value = nowSP / maxSP;
    }

    /// <summary>
    /// ����ġ ui ���� �Լ�
    /// </summary>
    /// <param name="value">���� ����ġ</param>
    /// <param name="levelUp">������ ����</param>
    public void ModifyEXP(float value, bool levelUp = false)
    {
        nowEXP = value;
        if (levelUp)
        {
            level++;
            maxEXP = level * 100;
            maxHP = 100 + (level - 1) * 10;
            nowHP = maxHP;
            maxSP = 100 + (level - 1) * 10;
            nowSP = maxSP;
        }
        barEXP.value = nowEXP / maxEXP;
    }
}
