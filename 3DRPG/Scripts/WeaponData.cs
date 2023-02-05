using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public float damage;      // 무기의 데미지
    public float weight;      // 무기의 무게
    public float speed;       // 무기의 속도
    bool attack = false;      // 공격 상태

    /// <summary>
    /// 무기의 데미지를 반환하는 함수
    /// </summary>
    /// <returns>무기의 데미지</returns>
    public float GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// 무기의 무게를 반환하는 함수
    /// </summary>
    /// <returns>무기의 무게</returns>
    public float GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// 무기의 속도를 반환하는 함수
    /// </summary>
    /// <returns>무기의 속도</returns>
    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// 무기의 공격 상태를 설정하는 함수
    /// </summary>
    /// <param name="value">현재 공격 상태인지</param>
    public void Attack(bool value)
    {
        attack = value;
    }

    /// <summary>
    /// 무기의 공격 상태 여부를 반환하는 함수
    /// </summary>
    /// <returns>무기의 공격 상태</returns>
    public bool GetAttack()
    {
        return attack;
    }
}