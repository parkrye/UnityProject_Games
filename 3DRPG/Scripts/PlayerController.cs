using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerManager playerManager;

    new Rigidbody rigidbody;                                                    // ĳ������ ������ٵ�
    Animator animator;                                                          // ĳ������ �ִϸ��̼�
    public new Transform camera;                                                // ���� ī�޶�

    Vector3 dir;                                                                // ī�޶� ��ǥ�� �������� ���� �̵��Ϸ��� ����
    Ray ray;                                                                    // ĳ���Ͱ� ���� ����ִ��� �浹 ���� ����

    float h, v, nowSpeed, weight;                                               // Ű���� ����, ���� �Է�, ���� �ӵ�, �߷� ����ġ
    public float sideSpeed = 3f, walkSpeed = 5f, runSpeed = 8f, jumpPower = 3f; // ���� �ӵ�, �⺻ �ӵ�, �޸��� �ӵ�, ���� �Ŀ�

    bool isGrounded = true;                                                     // ���� ĳ���Ͱ� ���� ����ִ���
    bool inBattle = false;                                                      // ���� ĳ���Ͱ� ���� ��Ȳ����
    bool hitable = true;                                                        // ���ظ� ���� �� �ִ� ��������

    bool jump;                                                                  // ���� �����ߴ���
    bool fall;                                                                  // ���� �߶� ��������
    float fallHeight;                                                           // �߶��� ����
    bool dead;                                                                  // ����ߴ���
    bool block;                                                                 // ���������

    bool fixDir = false;                                                        // ���� ������ ����������
    bool fixMov = false;                                                        // ���� �̵��� ����������

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        weight = playerManager.GetWeightSpeed();
        weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
        animator.SetFloat("ActionSpeed", weight);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            CheckGround();
            BattleAction();
            ChangeEquipment();
        }
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Movement();
        }
    }

    /// <summary>
    /// ���� �ൿ
    /// </summary>
    void BattleAction()
    {
        if (!animator.GetBool("Falling"))
        {
            // ���콺 ������ ��ư�� ������ ��� ����
            if (Input.GetMouseButtonDown(1))
            {
                if (!inBattle)
                {
                    inBattle = true;
                    playerManager.ModifyRecovery(inBattle, true);
                }
                animator.SetBool("Blocking", true);
                fixMov = true;
                block = true;
                playerManager.Block(block);
            }
            // ���콺 ������ ��ư�� ���� ��� ��
            if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("Blocking", false);
                fixMov = false;
                block = false;
                playerManager.Block(block);
            }

            // ����� ���¹̳� �Ҹ�
            if (block)
            {
                playerManager.ModifySP(-0.1f);
            }

            if (!fixMov)
            {
                // FŰ�� ���� ���Ƿ� ������, ���� ��Ȳ�� ��ȯ
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (inBattle)
                    {
                        inBattle = false;
                    }
                    else
                    {
                        inBattle = true;
                    }
                    playerManager.ModifyRecovery(inBattle);
                }

                if (!fixDir)
                {
                    // ���콺 ���� ��ư�� ������ ���� ����
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (playerManager.ModifySP(-10))
                        {
                            if (!inBattle)
                            {
                                inBattle = true;
                                playerManager.ModifyRecovery(inBattle);
                            }
                            animator.SetTrigger("Attack");
                        }
                    }

                    // ĳ���� ������
                    // Left Control �Է����� ������ ����
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        if (playerManager.ModifySP(-10))
                        {
                            animator.SetTrigger("Roll");
                            rigidbody.AddForce(Vector3.up * jumpPower * weight / 2 + transform.forward * jumpPower * weight, ForceMode.VelocityChange);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ű���带 ���� �̵� ����, �̵� ����, �̵� �ӵ��� �Է¹ް� �̵� ����� �÷����ϴ� �Լ�
    /// </summary>
    void Movement()
    {
        // �̵� �Է�
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // �̵� �ӵ� ����
        // LeftShift�� ���� ���¶�� �޸��� �ӵ��� ��ȯ
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (h != 0 || v != 0)
            {
                if (playerManager.ModifySP(-0.2f))
                {
                    nowSpeed = runSpeed * weight;
                }
            }
        }
        else
        {
            nowSpeed = walkSpeed * weight;
        }

        // ȸ�� �� �̵�
        if (h != 0 || v != 0)
        {
            if (!fixDir)
            {
                // ī�޶��� ������ �������� �Է¿� ���� ȸ���ϰ�, ĳ���ʹ� �������� �̵�
                dir = transform.position + camera.transform.forward * v + camera.transform.right * h;
                dir.y = transform.position.y;
                transform.LookAt(dir);
            }

            if (!fixMov)
            {
                if (!fixDir)
                {
                    if (h < 0) h = -h;
                    if (v < 0) v = -v;
                    rigidbody.velocity = transform.forward * nowSpeed * Mathf.Max(h, v) + transform.up * rigidbody.velocity.y;
                }
                else
                {
                    dir = camera.transform.forward * v + camera.transform.right * h;
                    dir *= nowSpeed;
                    dir.y = rigidbody.velocity.y;
                    rigidbody.velocity = dir;
                }
            }
        }

        // �̵� ���
        if (isGrounded)
        {
            animator.SetBool("Falling", false);
            if (h != 0 || v != 0)
            {
                animator.SetBool("RunForward", true);
            }
            else
            {
                animator.SetBool("Combat", inBattle);
                animator.SetBool("RunForward", false);
            }

            if (!animator.GetBool("Blocking"))
            {
                // ĳ���� ����
                // Spcae �Է����� ���� ����
                if (!jump)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (playerManager.ModifySP(-10))
                        {
                            animator.SetTrigger("Jump");
                            rigidbody.AddForce(Vector3.up * jumpPower * weight, ForceMode.VelocityChange);
                        }
                    }
                }
            }
        }
        // ĳ���� �߶� ���
        // ĳ���Ͱ� ���� ������� ���� ��쿡 ����
        else
        {
            if (rigidbody.velocity.y < 0f)
            {
                if (!fall)
                {
                    animator.SetBool("Falling", true);
                    animator.SetBool("RunForward", false);
                    fall = true;
                    fallHeight = transform.position.y;
                }
            }
        }
    }

    /// <summary>
    /// ĳ���Ͱ� ���� ���� ����ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    void CheckGround()
    {
        // ĳ������ ��ġ�κ��� ���� 0.05f �������� �Ʒ��� 0.5f �̳��� Stepable ������Ʈ�� �ִٸ� ���� �������̶�� �Ǵ�
        ray = new Ray(transform.position + Vector3.up * 0.05f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up * 0.05f, Vector3.down * 0.5f, Color.green);
        if (Physics.Raycast(ray, 0.5f, LayerMask.GetMask("Stepable")))
        {
            isGrounded = true;
            if (fall)
            {
                fallHeight -= transform.position.y;
                if (fallHeight < 4)
                {
                    fallHeight = 0;
                }
                else if(fallHeight < 8)
                {
                    fallHeight *= 2;
                }
                else if(fallHeight < 12)
                {
                    fallHeight *= 5;
                }
                else
                {
                    fallHeight *= 10;
                }
                playerManager.ModifyHP(-fallHeight, true);
                fall = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
   
    /// <summary>
    /// ��� �����ϴ� �Լ�
    /// </summary>
    void ChangeEquipment()
    {
        // �۵� ����
        if(true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerManager.ShiftWeapon(0);
                animator.SetBool("Armored", false);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerManager.ShiftWeapon(1);
                animator.SetBool("Armored", true);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                playerManager.ShiftWeapon(2);
                animator.SetBool("Armored", true);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                playerManager.ShiftWeapon(3);
                animator.SetBool("Armored", true);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                playerManager.ShiftWeapon(4);
                animator.SetBool("Armored", true);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                playerManager.ShiftShield();
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                playerManager.ShiftArmor(0);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                playerManager.ShiftArmor(1);
                weight = playerManager.GetWeightSpeed();
                weight = Mathf.Lerp(1.2f, 0.8f, (2f - weight) / 2f);
                animator.SetFloat("ActionSpeed", weight);
            }
        }
    }

    /// <summary>
    /// �÷��̾ ���ظ� ���� �� �ִ� �������� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>true:���ظ� ���� �� ����, false:���ظ� ���� ����</returns>
    public bool GetHitable()
    {
        return hitable;
    }

    /// <summary>
    /// �ǰݽ� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="damage">�ǰ� ������</param>
    public void Attacked(float damage)
    {
        if (hitable && !dead)
        {
            if (!playerManager.ModifyHP(damage, true))
            {
                dead = true;
                animator.SetTrigger("Die");
            }
            else
            {
                if (!inBattle)
                {
                    inBattle = true;
                    playerManager.ModifyRecovery(inBattle);
                }
                if (block)
                {
                    playerManager.ModifySP(-10f);
                }
                animator.SetTrigger("Hit");
            }
        }
    }

    // ���� �ִϸ��̼� �̺�Ʈ
    public void HitEnd()
    {
        hitable = true;
        fixDir = false;
        fixMov = false;
        animator.SetBool("Action", false);
        animator.SetTrigger("TriggerOut");
    }

    public void RollStart()
    {
        hitable = false;
        fixDir = true;
        fixMov = true;
        animator.SetBool("Action", true);
    }

    public void RollEnd()
    {
        hitable = true;
        fixDir = false;
        fixMov = false;
        animator.SetBool("Action", false);
        animator.SetTrigger("TriggerOut");
    }

    public void AttackStart()
    {
        fixDir = true;
        playerManager.Attack(true);
        animator.SetBool("Action", true);
    }

    public void AttackEnd()
    {
        fixDir = false;
        playerManager.Attack(false);
        animator.SetBool("Action", false);
        animator.SetTrigger("TriggerOut");
    }

    public void JumpAnimationStart()
    {
        jump = true;
        animator.SetBool("Action", true);
    }

    public void JumpAnimationEnd()
    {
        jump = false;
        animator.SetBool("Action", false);
    }
}
