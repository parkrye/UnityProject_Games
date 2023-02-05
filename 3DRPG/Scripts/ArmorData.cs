using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmorData : MonoBehaviour
{
    public int aromor;      // 장비의 방어도
    public int weight;      // 장비의 무게

    /// <summary>
    /// 장비의 방어도를 반환하는 함수
    /// </summary>
    /// <returns>장비의 방어도</returns>
    public int GetArmor()
    {
        return aromor;
    }

    /// <summary>
    /// 장비의 무게를 반환하는 함수
    /// </summary>
    /// <returns>장비의 무게</returns>
    public int GetWeight()
    {
        return weight;
    }
}