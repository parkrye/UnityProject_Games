using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    public GameObject player;                           // �÷��̾� ������Ʈ
    public Animator animator;                           // ���� �ִϸ�����
    public NavMeshAgent navMeshAgent;                   // �׺�޽� ������Ʈ

    public float maxHP, nowHP, exp;                     // �ִ� ü��, ���� ü��, ����ġ
    public float damage;                                // ���ݷ�
    public float attackSpeed, moveSpeed;                // ���� �ӵ�(�ֱ�), �̵� �ӵ�
    public float moveRange, awareRange, attackRange;    // �̵� �Ÿ�, �ν� ����, ���� ����
    public bool battle, die, attack;                    // ���� ����, ��� ����, ���� ����

    Vector3 point;                                      // �̵� ��ǥ

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
    /// ��ȸ �Լ�. ������ �⺻ �̵� ó��
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
    /// ���� �̵� �Լ�. ������ ���� �̵� ��ġ�� ����
    /// </summary>
    /// <param name="center">�߽� ��ǥ(���� ��ġ)</param>
    /// <param name="_range">�̵� ����</param>
    /// <param name="result">����� ������ ����</param>
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
    /// �÷��̾� Ž�� �Լ�
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
    /// ���� �ڷ�ƾ. ���� ���� ����
    /// </summary>
    public virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(0f);
    }

    /// <summary>
    /// ���� ���Ͱ� ���� �������� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ ���� ����</returns>
    public bool GetAttack()
    {
        return attack;
    }

    /// <summary>
    /// ������ ���� ���¸� �����ϴ� �Լ�. �ִϸ��̼� ȣ��
    /// </summary>
    public void AttackEnd()
    {
        attack = false;
    }

    /// <summary>
    /// �������� ȣ���ϴ� �Լ�
    /// </summary>
    /// <returns>������ ���ݷ�</returns>
    public float GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// ���Ͱ� ���ݴ������� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="damage">���Ͱ� �Դ� ����</param>
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
    /// ���� ����� ȣ��Ǵ� �ڷ�ƾ. ���� �ð� �� ����
    /// </summary>
    IEnumerator Die()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
