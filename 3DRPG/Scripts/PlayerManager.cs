using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UIManager uiManager;         // UI ������ ��ũ��Ʈ
    public GameObject[] weapons;        // ���
    public GameObject[] armors;         // ��

    public int weapon = 0;              // ���� ���
    public int armor = 0;               // ���� ��
    bool shield;                        // ���и� ��� �ִ��� ����

    public int level;
    public float maxHP, maxSP;          // �ִ� ü��, ���
    public float nowHP, nowSP;          // ���� ü��, ���
    public float recHP, recSP;          // ü��, ��� ȸ����
    public float nowEXP, maxEXP;        // ����, �ִ� ����ġ
    float recDivisor = 5f;              // ȸ���� ����ġ

    public float maxWeight, nowWeight;  // �ִ� �߷�, ���� �߷�
    float armored;                      // ��

    // Start is called before the first frame update
    void Start()
    {
        if (level == 0) level = 1;
        if (maxHP == 0f) maxHP = 100 + (level - 1) * 10;
        if (maxSP == 0f) maxSP = 100 + (level - 1) * 10;
        if (recHP == 0f) recHP = maxHP / 20;
        if (recSP == 0f) recSP = maxSP / 20;
        if (nowHP == 0f) nowHP = maxHP;
        if (nowSP == 0f) nowSP = maxSP;

        if (maxWeight == 0f) maxWeight = 30f;
        nowWeight += weapons[weapon].GetComponent<WeaponData>().GetWeight();
        nowWeight += armors[armor].GetComponent<ArmorData>().GetWeight();
        armored += armors[armor].GetComponent<ArmorData>().GetArmor();

        uiManager.SetSlider(level);

        StartCoroutine(Recovery());
    }

    /// <summary>
    /// �ڿ� ȸ�� �ڷ�ƾ
    /// </summary>
    /// <returns>0.1�ʸ��� �ݺ�</returns>
    IEnumerator Recovery()
    {
        while (nowHP > 0f)
        {
            yield return new WaitForSeconds(0.1f);
            if(recDivisor > 0f)
            {
                ModifyHP(recHP / recDivisor, false);
                ModifySP(recSP / recDivisor);
            }
        }
    }

    /// <summary>
    /// �ڿ� ȸ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="multiply">ȸ������ ����� �����ΰ�</param>
    /// <param name="stop">ȸ������ �ʴ� �����ΰ�</param>
    public void ModifyRecovery(bool multiply, bool stop = false)
    {
        if (stop)
        {
            recDivisor = 0f;
        }
        else
        {
            if (multiply)
            {
                recDivisor = 10f;
            }
            else
            {
                recDivisor = 5f;
            }
        }
    }

    /// <summary>
    /// hp�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="hp">������</param>
    /// <param name="hit">���� ����</param>
    /// <returns>�������� ���� hp�� 0 ���ϰ� �� ��� false�� ��ȯ</returns>
    public bool ModifyHP(float hp, bool hit)
    {
        if(hp >= 0f)
        {
            nowHP += hp;
            if (nowHP > maxHP)
            {
                nowHP = maxHP;
            }
            uiManager.ModifyHP(nowHP);
        }
        else
        {
            if (hit)
            {
                hp += armored;
                if(hp >= 0f)
                {
                    hp = -1f;
                }

                nowHP += hp;
                if (nowHP <= 0f)
                {
                    nowHP = 0f;
                }
            }
            else
            {
                nowHP += hp;
                if (nowHP <= 0f)
                {
                    nowHP = 0f;
                }
            }
        }
        uiManager.ModifyHP(nowHP);

        if (nowHP <= 0f)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// sp�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="sp">������</param>
    /// <returns>�������� �����̰�, ���� sp���� ���밪�� Ŭ ��� false�� ��ȯ</returns>
    public bool ModifySP(float sp)
    {
        if(sp < 0f)
        {
            if(nowSP < -sp)
            {
                return false;
            }
        }
        nowSP += sp;
        if (nowSP > maxSP)
        {
            nowSP = maxSP;
        }
        else if (nowSP < 0f)
        {
            nowSP = 0f;
        }
        uiManager.ModifySP(nowSP);
        return true;
    }

    /// <summary>
    /// exp�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="exp">������</param>
    public void ModifyEXP(float exp)
    {
        nowEXP += exp;
        if(nowEXP >= maxEXP)
        {
            level++;
            maxEXP = level * 100;
            maxHP = 100 + (level - 1) * 10;
            maxSP = 100 + (level - 1) * 10;
            nowEXP = 0;
            nowHP = maxHP;
            nowSP = maxSP;
        }
        uiManager.ModifyEXP(nowEXP, true);
    }

    /// <summary>
    /// ���⸦ ��ü�ϴ� �Լ�
    /// </summary>
    /// <param name="num">0:���, 1:�ܰ�, 2:���, 3:����, 4:�ظ�</param>
    public void ShiftWeapon(int num)
    {
        if(num != weapon)
        {
            if (num >= 0 && num < weapons.Length - 1)
            {
                weapons[weapon].SetActive(false);
                nowWeight -= weapons[weapon].GetComponent<WeaponData>().GetWeight();
                nowWeight += weapons[num].GetComponent<WeaponData>().GetWeight();
                weapons[num].SetActive(true);
                weapon = num;
            }
        }
    }

    /// <summary>
    /// ���и� ����/�����ϴ� �Լ�
    /// </summary>
    public void ShiftShield()
    {
        if (shield)
        {
            nowWeight -= 10f;
            armored -= 3f;
        }
        else
        {
            nowWeight += 10f;
            armored += 3f;
        }
        weapons[weapons.Length - 1].SetActive(!shield);
        shield = !shield;
    }

    /// <summary>
    /// ���� ��ü�ϴ� �Լ�
    /// </summary>
    /// <param name="num">0:õ��, 1:����</param>
    public void ShiftArmor(int num)
    {
        if (num != armor)
        {
            if (num >= 0 && num < armors.Length)
            {
                armors[armor].SetActive(false);
                nowWeight -= armors[armor].GetComponent<ArmorData>().GetWeight();
                armored -= armors[armor].GetComponent<ArmorData>().GetArmor();
                nowWeight += armors[num].GetComponent<ArmorData>().GetWeight();
                armored += armors[num].GetComponent<ArmorData>().GetArmor();
                armors[num].SetActive(true);
                armor = num;
            }
        }
    }

    /// <summary>
    /// ���� �߷��� ���� �ӵ��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>���� �߷��� Ŀ���� ���� 2���� 0.2���� ����</returns>
    public float GetWeightSpeed()
    {
        if (nowWeight < maxWeight / 10f)
        {
            return 2f;
        }
        else if (nowWeight < maxWeight / 5f)
        {
            return 1.8f;
        }
        else if (nowWeight < maxWeight / 4f)
        {
            return 1.5f;
        }
        else if(nowWeight < maxWeight / 3f)
        {
            return 1.2f;
        }
        else if(nowWeight < maxWeight / 2f)
        {
            return 1.0f;
        }
        else if (nowWeight <= maxWeight)
        {
            return 0.8f;
        }
        else if(nowHP < maxWeight * 2f)
        {
            return 0.5f;
        }
        else
        {
            return 0.2f;
        }
    }

    /// <summary>
    /// ��� �� �÷��̾��� ���� ���̴� �Լ�
    /// </summary>
    /// <param name="block">��� ���� ����</param>
    public void Block(bool block)
    {
        if (block)
        {
            armored += 10f;
        }
        else
        {
            armored -= 10f;
        }
    }

    /// <summary>
    /// ������ ���� ���¸� �ο��ϴ� �Լ�
    /// </summary>
    /// <param name="value">���� ���� ��������</param>
    public void Attack(bool value)
    {
        weapons[weapon].GetComponent<WeaponData>().Attack(value);
    }
}
