using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Chest : MonsterBase
{
    /// <summary>
    /// 공격 코루틴
    /// </summary>
    public override IEnumerator Attack()
    {
        while (battle)
        {
            if (navMeshAgent.remainingDistance <= attackRange)
            {
                int rand = Random.Range(0, 4);
                attack = true;
                if (rand == 0)
                {
                    animator.SetTrigger("Attack1");
                }
                else
                {
                    animator.SetTrigger("Attack2");
                }
            }
            yield return new WaitForSeconds(10f / attackSpeed);
        }
    }
}
