using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColliderManager : MonoBehaviour
{
    public MonsterBase monsterBase; // 몬스터 스크립트

    /// <summary>
    /// 충돌시 호출
    /// </summary>
    /// <param name="collision">충돌한 객체</param>
    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.gameObject.tag == "Player")
        {
            // 몬스터가 현재 공격 상태라면
            if (monsterBase.GetAttack())
            {
                // 플레이어에게 데미지
                collision.gameObject.GetComponent<PlayerController>().Attacked(-monsterBase.GetDamage());
            }
        }
        // 무기와 충돌한 경우
        else if (collision.gameObject.tag == "Weapon")
        {
            // 무기가 현재 공격 상태라면
            if (collision.gameObject.GetComponent<WeaponData>().GetAttack())
            {
                // 몬스터에게 데미지
                monsterBase.Hit(collision.gameObject.GetComponent<WeaponData>().GetDamage());
            }
        }
    }
}
