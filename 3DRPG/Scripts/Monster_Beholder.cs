using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Beholder : MonsterBase
{
    public GameObject bulletPrefab; // 탄환 프리펩
    public Transform shotPoint;     // 탄환 시작 위치

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    public override IEnumerator Attack()
    {
        while (battle)
        {
            if (navMeshAgent.remainingDistance <= attackRange)
            {
                navMeshAgent.SetDestination(transform.position);
                attack = true;
                animator.SetTrigger("Attack");
                GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
                bullet.transform.LookAt(player.transform);
                bullet.GetComponent<Beholder_Bullet>().SetDamage(damage);
                bullet.GetComponent<Beholder_Bullet>().Shot();
            }
            else
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            yield return new WaitForSeconds(10f / attackSpeed);
        }
    }
}
