using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float nowHP, maxHP;          // 현재 체력, 최대 체력
    public float nowSP, maxSP;          // 현재 기력, 최대 기력
    public float nowEXP, maxEXP;        // 현재 경험치, 최대 경험치
    public int level;                   // 레벨
    public Slider barHP, barSP, barEXP; // 체력, 기력, 경험치 ui

    /// <summary>
    /// ui 초기 설정 함수
    /// </summary>
    /// <param name="_level">현재 레벨</param>
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
    /// 체력 ui 수정 함수
    /// </summary>
    /// <param name="value">현재 체력</param>
    public void ModifyHP(float value)
    {
        nowHP = value;
        barHP.value = nowHP / maxHP;
    }


    /// <summary>
    /// 기력 ui 수정 함수
    /// </summary>
    /// <param name="value">현재 기력</param>
    public void ModifySP(float value)
    {
        nowSP = value;
        barSP.value = nowSP / maxSP;
    }

    /// <summary>
    /// 경험치 ui 수정 함수
    /// </summary>
    /// <param name="value">현재 경험치</param>
    /// <param name="levelUp">레벨업 여부</param>
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
