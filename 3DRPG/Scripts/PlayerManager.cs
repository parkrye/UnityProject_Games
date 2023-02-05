using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UIManager uiManager;         // UI 관리자 스크립트
    public GameObject[] weapons;        // 장비
    public GameObject[] armors;         // 방어구

    public int weapon = 0;              // 현재 장비
    public int armor = 0;               // 현재 방어구
    bool shield;                        // 방패를 들고 있는지 여부

    public int level;
    public float maxHP, maxSP;          // 최대 체력, 기력
    public float nowHP, nowSP;          // 현재 체력, 기력
    public float recHP, recSP;          // 체력, 기력 회복량
    public float nowEXP, maxEXP;        // 현재, 최대 경험치
    float recDivisor = 5f;              // 회복량 분할치

    public float maxWeight, nowWeight;  // 최대 중량, 현재 중량
    float armored;                      // 방어도

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
    /// 자연 회복 코루틴
    /// </summary>
    /// <returns>0.1초마다 반복</returns>
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
    /// 자연 회복량을 조정하는 함수
    /// </summary>
    /// <param name="multiply">회복량이 증배된 상태인가</param>
    /// <param name="stop">회복하지 않는 상태인가</param>
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
    /// hp를 수정하는 함수
    /// </summary>
    /// <param name="hp">수정값</param>
    /// <param name="hit">공격 여부</param>
    /// <returns>공격으로 인해 hp가 0 이하가 될 경우 false를 반환</returns>
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
    /// sp를 수정하는 함수
    /// </summary>
    /// <param name="sp">수정값</param>
    /// <returns>수정값이 음수이고, 현재 sp보다 절대값이 클 경우 false를 반환</returns>
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
    /// exp를 수정하는 함수
    /// </summary>
    /// <param name="exp">수정값</param>
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
    /// 무기를 교체하는 함수
    /// </summary>
    /// <param name="num">0:빈손, 1:단검, 2:장검, 3:도끼, 4:해머</param>
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
    /// 방패를 장착/해제하는 함수
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
    /// 방어구를 교체하는 함수
    /// </summary>
    /// <param name="num">0:천옷, 1:갑옷</param>
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
    /// 현재 중량에 따라 속도를 반환하는 함수
    /// </summary>
    /// <returns>현재 중량이 커짐에 따라 2에서 0.2까지 감소</returns>
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
    /// 방어 중 플레이어의 방어도를 높이는 함수
    /// </summary>
    /// <param name="block">방어 상태 여부</param>
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
    /// 무기의 공격 상태를 부여하는 함수
    /// </summary>
    /// <param name="value">현재 공격 상태인지</param>
    public void Attack(bool value)
    {
        weapons[weapon].GetComponent<WeaponData>().Attack(value);
    }
}
