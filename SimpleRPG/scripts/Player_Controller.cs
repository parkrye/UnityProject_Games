using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    // 플레이어 rigid, 위치, 이미지, 애니메이터
    private new Rigidbody2D rigidbody2D;
    private new Transform transform;
    private SpriteRenderer sprite;
    private Animator animator;

    // 무기, 무기 위치
    public GameObject sword;
    private Transform swordTransform;

    // 플레이어 최대 hp, 현재 hp, 공격 데미지, 공격 속도, 공격 상태, 피격 상태, 현재 hp바
    private float maxHP;
    private float nowHP;
    private float attackDamage;
    private float attackSpeed;
    private int nowAttack;
    private bool nowhit;
    public Image nowHPbar;

    // 플레이어 이동 속도, 점프 파워, 착지, 생존 여부
    private float moveSpeed;
    private float jumpPower;
    private bool isGround;
    private bool alive;

    // 플레이어 레벨, 경험치, 근력, 민첩, 건강, 포인트
    private int[] data;

    // 파티클
    public GameObject particle;

    // 효과음
    public AudioClip audio_jump;
    public AudioClip audio_attack_1;
    public AudioClip audio_attack_2;
    public AudioClip audio_levelup;
    public AudioClip audio_hit;
    public AudioClip audio_die;
    AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        // 플레이어 rigid, 위치, 이미지, 애니메이터 초기화
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        // 플레이어 무기 위치, 크기 초기화
        swordTransform = sword.GetComponent<Transform>();
        swordTransform.localScale = new Vector3(0, 0, 1);

        // 효과음
        audioSource = gameObject.GetComponent<AudioSource>();

        // 초기값 설정
        nowHP = 0;
        if (PlayerPrefs.HasKey("level"))
        {
            data = new int[] { PlayerPrefs.GetInt("level"), PlayerPrefs.GetInt("exp"), PlayerPrefs.GetInt("str"), PlayerPrefs.GetInt("dex"), PlayerPrefs.GetInt("con"), PlayerPrefs.GetInt("point") };
            nowHP = PlayerPrefs.GetInt("hp");
        }
        else
        {
            data = new int[] { 1, 0, 1, 1, 1, 0 };
        }
        SaveData();
        SetPlayingData();

        // 초기 상태 설정
        nowAttack = 0;
        nowhit = false;
        isGround = false;
        alive = true;

        // 1/공격속도 초 마다 공격 준비 상태가 될 수 있다
        Invoke("ReadyToAttack", 1 / attackSpeed);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // 현재 hp바를 갱신
        nowHPbar.fillAmount = (float)nowHP / (float)maxHP;

        // 살아있다면
        if (alive)
        {
            // 공격, 이동, 점프 수행
            PlayerAttack();
            PlayerMove();
            PlayerJump();
        }
    }

    // 플레이어 이동 입력 처리
    private void PlayerMove()
    { 
        // 피격 상태가 아닐 때
        if (!nowhit)
        {
            // 오른쪽, 왼쪽 화살표로 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                sprite.flipX = false;
                swordTransform.position = new Vector2(transform.position.x + 0.0f, transform.position.y - 0.0f);
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.flipX = true;
                swordTransform.position = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.0f);
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
            }
        }
    }

    // 플레이어 점프 입력 처리
    private void PlayerJump()
    {
        // 피격 상태가 아닐 때
        if (!nowhit)
        {
            // 빔 시작 위치, 방향, 길이, 특정 마스크로 한정
            RaycastHit2D raycast = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            // 플레이어가 내려올 때
            if (rigidbody2D.velocity.y <= 0)
            {
                // 빔에 맞은 오브젝트가 있을 때
                if (raycast.collider != null)
                {
                    // 거리가 0.1보다 작아지면
                    if (raycast.distance < 0.1f)
                    {
                        // 플레이어는 땅에 있다고 취급한다
                        isGround = true;
                    }
                }
            }

            // 플레이어가 땅에 있다면
            if (isGround)
            {
                // 스페이스 바로 점프
                if (Input.GetKey(KeyCode.Space))
                {
                    isGround = false;
                    audioSource.clip = audio_jump;
                    audioSource.Play();
                    rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                }
            }
        }
    }

    // 플레이어 기본 공격 입력 처리
    private void PlayerAttack()
    {
        // 피격 상태가 아닐 때
        if (!nowhit)
        {
            // 플레이어가 Z키를 눌렀고 현재 공격 준비 상태라면
            if (Input.GetKey(KeyCode.Z) && nowAttack == 0)
            {
                // 공격의 판정을 키우고, 공격 중 상태가 되고, 공격 애니메이션을 재생
                audioSource.clip = audio_attack_1;
                audioSource.Play();
                swordTransform.localScale = new Vector3(1, 1, 1);
                nowAttack++;
                animator.SetInteger("attack", nowAttack);
            }
        }
    }
    
    // 공격 준비 완료
    private void ReadyToAttack()
    {
        // 현재 공격 종료 상태라면
        if(nowAttack == 2)
        {
            // 공격 준비 상태가 된다
            nowAttack = 0;
        }

        // 1/공격속도 초 후 다시 공격 준비 상태가 될 수 있다
        Invoke("ReadyToAttack", 1 / attackSpeed);
    }

    // 공격 종료 상태 함수, 애니메이터에 의해 호출
    public void AttackOver()
    {
        // 공격의 판정을 지우고, 공격 종료 상태가 되고, 공격 애니메이션을 종료
        swordTransform.localScale = new Vector3(0, 0, 1);
        nowAttack++;
        animator.SetInteger("attack", nowAttack);
    }

    // 공격 상태인지 반환, 에너미 피격 시 호출
    public int GetAttacked()
    {
        return nowAttack;
    }

    // 공격 데미지 반환, 에너미 피격 시 호출
    public float GetAttackDamage()
    {
        return attackDamage;
    }

    // 에너미 충돌 시 호출
    public void Damaged(float damage, bool direction)
    {
        if (alive)
        {
            // hit이 true인 동안에는 데미지를 받지 않음
            if (!nowhit)
            {
                nowHP -= damage;
            }

            // hp가 0 이하가 되면 사망
            if (nowHP <= 0)
            {
                alive = false;
                animator.SetTrigger("die");
                audioSource.clip = audio_die;
                audioSource.Play();
                rigidbody2D.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            }
            else
            {
                // 피격 애니메이션 실행
                nowhit = true;
                animator.SetBool("hit", nowhit);
                audioSource.clip = audio_hit;
                audioSource.Play();

                // 충돌 반대 방향으로 점프
                if (direction)
                {
                    rigidbody2D.AddForce(Vector2.left * 0.3f + Vector2.up * 0.8f, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody2D.AddForce(Vector2.right * 0.3f + Vector2.up * 0.8f, ForceMode2D.Impulse);
                }
            }
        }
    }

    // die 애니메이션 종료 시 호출
    public void die()
    {
        SceneManager.LoadScene("02_GameOverScene");
    }

    // 피격 종료 상태 함수, 애니메이터에 의해 호출
    public void HitOver()
    {
        nowhit = false;
        animator.SetBool("hit", nowhit);
    }

    // 경험치 획득 / 레벨업
    public void AddExp(int exp)
    {
        data[1] += exp;
        if (data[1] >= data[0] * 100)
        {
            audioSource.clip = audio_levelup;
            audioSource.Play();
            Instantiate(particle, transform.position, Quaternion.identity);
            data[1] -= data[0] * 10;
            data[0] += 1;
            data[5] += 3;
            nowHP = maxHP;
        }
    }

    // 데이터 저장
    public void SaveData()
    {
        PlayerPrefs.SetInt("level", data[0]);
        PlayerPrefs.SetInt("exp", data[1]);
        PlayerPrefs.SetInt("str", data[2]);
        PlayerPrefs.SetInt("dex", data[3]);
        PlayerPrefs.SetInt("con", data[4]);
        PlayerPrefs.SetInt("point", data[5]);
        PlayerPrefs.SetFloat("hp", nowHP);
        if (!PlayerPrefs.HasKey("location"))
        {
            PlayerPrefs.SetInt("location", 0);
        }
        PlayerPrefs.SetString("map", SceneManager.GetActiveScene().name);
    }

    // 플레잉 데이터 수정
    private void SetPlayingData()
    {
        maxHP = data[4] * 10;
        attackDamage = data[2];
        jumpPower = 5f;
        moveSpeed = 5f;
        attackSpeed = (1 + (data[3] - 10f) / 50) * 2f;
        if (nowHP == 0)
        {
            nowHP = maxHP;
        }
    }

    // 체력 회복
    public void Heal(float healpoint)
    {
        if(healpoint < 0)
        {
            nowHP = maxHP;
        }
        else
        {
            nowHP += healpoint;
            if (nowHP > maxHP)
            {
                nowHP = maxHP;
            }
        }
    }

    // 능력치 수정
    public void SetData(int[] _data)
    {
        data = _data;
    }
}