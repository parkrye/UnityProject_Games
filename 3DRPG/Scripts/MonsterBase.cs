using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    public GameObject player;                           // 플레이어 오브젝트
    public Animator animator;                           // 몬스터 애니메이터
    public NavMeshAgent navMeshAgent;                   // 네비메쉬 에이전트

    public float maxHP, nowHP, exp;                     // 최대 체력, 현재 체력, 경험치
    public float damage;                                // 공격력
    public float attackSpeed, moveSpeed;                // 공격 속도(주기), 이동 속도
    public float moveRange, awareRange, attackRange;    // 이동 거리, 인식 범위, 공격 범위
    public bool battle, die, attack;                    // 전투 상태, 사망 상태, 공격 상태

    Vector3 point;                                      // 이동 좌표

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        nowHP = maxHP;

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            WalkAround();
            SearchPlayer();
        }
    }

    /// <summary>
    /// 배회 함수. 몬스터의 기본 이동 처리
    /// </summary>
    void WalkAround()
    {
        if (!battle)
        {
            if (navMeshAgent.remainingDistance < 1f)
            {
                if (RandomPoint(transform.position, moveRange, out point))
                {
                    navMeshAgent.SetDestination(point);
                }
            }
        }
    }

    /// <summary>
    /// 랜덤 이동 함수. 몬스터의 다음 이동 위치를 지정
    /// </summary>
    /// <param name="center">중심 좌표(몬스터 위치)</param>
    /// <param name="_range">이동 범위</param>
    /// <param name="result">결과를 저장할 벡터</param>
    /// <returns></returns>
    bool RandomPoint(Vector3 center, float _range, out Vector3 result)
    {
        if (moveRange > 0f)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * _range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    animator.SetBool("Move", true);
                    return true;
                }
            }
            result = Vector3.zero;
            animator.SetBool("Move", false);
            return false;
        }
        result = Vector3.zero;
        animator.SetBool("Move", false);
        return false;
    }

    /// <summary>
    /// 플레이어 탐색 함수
    /// </summary>
    void SearchPlayer()
    {
        if (!battle)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < awareRange)
            {
                battle = true;
                StartCoroutine(Attack());
                animator.SetBool("Battle", battle);
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) > awareRange * 2)
            {
                battle = false;
                StopCoroutine(Attack());
                animator.SetBool("Battle", battle);
                navMeshAgent.SetDestination(point);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            else
            {
                transform.LookAt(player.transform);
            }
        }
    }

    /// <summary>
    /// 공격 코루틴. 몬스터 별로 구현
    /// </summary>
    public virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(0f);
    }

    /// <summary>
    /// 현재 몬스터가 공격 상태인지 반환하는 함수
    /// </summary>
    /// <returns>몬스터의 공격 상태</returns>
    public bool GetAttack()
    {
        return attack;
    }

    /// <summary>
    /// 몬스터의 공격 상태를 종료하는 함수. 애니메이션 호출
    /// </summary>
    public void AttackEnd()
    {
        attack = false;
    }

    /// <summary>
    /// 데미지를 호출하는 함수
    /// </summary>
    /// <returns>몬스터의 공격력</returns>
    public float GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// 몬스터가 공격당했을때 호출되는 함수
    /// </summary>
    /// <param name="damage">몬스터가 입는 피해</param>
    public void Hit(float damage)
    {
        if (!die)
        {
            nowHP -= damage;
            if (nowHP <= 0)
            {
                player.GetComponent<PlayerManager>().ModifyEXP(exp);
                nowHP = 0;
                animator.SetTrigger("Die");
                die = true;
                StopAllCoroutines();
                StartCoroutine(Die());
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }
    }

    /// <summary>
    /// 몬스터 사망시 호출되는 코루틴. 일정 시간 후 삭제
    /// </summary>
    IEnumerator Die()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
