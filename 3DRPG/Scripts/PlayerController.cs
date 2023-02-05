using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerManager playerManager;

    new Rigidbody rigidbody;                                                    // 캐릭터의 리지드바디
    Animator animator;                                                          // 캐릭터의 애니메이션
    public new Transform camera;                                                // 메인 카메라

    Vector3 dir;                                                                // 카메라 좌표를 기준으로 현재 이동하려는 방향
    Ray ray;                                                                    // 캐릭터가 땅에 닿아있는지 충돌 감지 광선

    float h, v, nowSpeed, weight;                                               // 키보드 수평, 수직 입력, 현재 속도, 중량 보정치
    public float sideSpeed = 3f, walkSpeed = 5f, runSpeed = 8f, jumpPower = 3f; // 측행 속도, 기본 속도, 달리기 속도, 점프 파워

    bool isGrounded = true;                                                     // 현재 캐릭터가 땅에 닿아있는지
    bool inBattle = false;                                                      // 현재 캐릭터가 전투 상황인지
    bool hitable = true;                                                        // 피해를 받을 수 있는 상태인지

    bool jump;                                                                  // 현재 점프했는지
    bool fall;                                                                  // 현재 추락 상태인지
    float fallHeight;                                                           // 추락시 높이
    bool dead;                                                                  // 사망했는지
    bool block;                                                                 // 방어중인지

    bool fixDir = false;                                                        // 현재 방향을 고정중인지
    bool fixMov = false;                                                        // 현재 이동을 고정중인지

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
    /// 전투 행동
    /// </summary>
    void BattleAction()
    {
        if (!animator.GetBool("Falling"))
        {
            // 마우스 오른쪽 버튼을 누르면 방어 시작
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
            // 마우스 오른쪽 버튼을 떼면 방어 끝
            if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("Blocking", false);
                fixMov = false;
                block = false;
                playerManager.Block(block);
            }

            // 방어중 스태미나 소모
            if (block)
            {
                playerManager.ModifySP(-0.1f);
            }

            if (!fixMov)
            {
                // F키를 눌러 임의로 비전투, 전투 상황을 전환
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
                    // 마우스 왼쪽 버튼을 누르는 순간 공격
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

                    // 캐릭터 구르기
                    // Left Control 입력으로 구르기 실행
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
    /// 키보드를 통해 이동 유형, 이동 방향, 이동 속도를 입력받고 이동 모션을 플레이하는 함수
    /// </summary>
    void Movement()
    {
        // 이동 입력
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 이동 속도 수정
        // LeftShift를 누른 상태라면 달리기 속도로 전환
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

        // 회전 및 이동
        if (h != 0 || v != 0)
        {
            if (!fixDir)
            {
                // 카메라의 전방을 기준으로 입력에 따라 회전하고, 캐릭터는 전방으로 이동
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

        // 이동 모션
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
                // 캐릭터 점프
                // Spcae 입력으로 점프 실행
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
        // 캐릭터 추락 모션
        // 캐릭터가 땅에 닿아있지 않은 경우에 진입
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
    /// 캐릭터가 현재 땅에 닿아있는지 확인하는 함수
    /// </summary>
    void CheckGround()
    {
        // 캐릭터의 위치로부터 위로 0.05f 지점에서 아래로 0.5f 이내에 Stepable 오브젝트가 있다면 땅에 접촉중이라고 판단
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
    /// 장비를 장착하는 함수
    /// </summary>
    void ChangeEquipment()
    {
        // 작동 조건
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
    /// 플레이어가 피해를 받을 수 있는 상태인지 반환하는 함수
    /// </summary>
    /// <returns>true:피해를 받을 수 있음, false:피해를 받지 않음</returns>
    public bool GetHitable()
    {
        return hitable;
    }

    /// <summary>
    /// 피격시 호출되는 함수
    /// </summary>
    /// <param name="damage">피격 데미지</param>
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

    // 이하 애니메이션 이벤트
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
