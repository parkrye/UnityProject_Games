using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public PotionSellerManager potionSellerManager;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public Transform goalTransform;

    public int pocket;
    bool start = false;
    bool buy = false;
    bool customer = false;
    int space;
    int customerNum;

    void Update()
    {
        if (start)
        {
            if (navMeshAgent.remainingDistance <= 0.2f)
            {
                if (!buy)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if (!customer)
                    {
                        LooInkProduct();
                    }
                }
            }
        }
    }

    public void Setting(PotionSellerManager _potionSellerManager, float _speed, Transform _goal)
    {
        pocket = 0;
        while(pocket < 400)
        {
            int rand = Random.Range(0, 10);
            if (rand > 0)
            {
                pocket += 10;
            }
            else
            {
                break;
            }
        }

        potionSellerManager = _potionSellerManager;
        navMeshAgent.speed = _speed;
        goalTransform = _goal;
        navMeshAgent.destination = goalTransform.position;
        start = true;
    }

    public void BeCustomer()
    {
        if (potionSellerManager.HasProduct())
        {
            int random = Random.Range(0, 10);
            if (random <= potionSellerManager.GetLevel())
            {
                space = potionSellerManager.HasSpace();
                if (space != -1)
                {
                    buy = true;
                    customerNum = potionSellerManager.UseSpace(space);
                    navMeshAgent.destination = potionSellerManager.GetTransform(space).position;
                }
            }
        }
    }

    void LooInkProduct()
    {
        customer = true;
        navMeshAgent.avoidancePriority = 3 -  customerNum;
        transform.LookAt(potionSellerManager.transform);
        animator.SetBool("Look", true);
        StartCoroutine(Delay());
    }

    void LookOutProduct()
    {
        potionSellerManager.BuyProduct(pocket);
        buy = false;
        navMeshAgent.destination = goalTransform.position;
        potionSellerManager.OutSpace(space);
        navMeshAgent.avoidancePriority = 50;
        animator.SetBool("Look", false);
    }

    IEnumerator Delay()
    {
        float delay = Random.Range(3, 10);
        yield return new WaitForSeconds(delay);
        LookOutProduct();
    }
}
