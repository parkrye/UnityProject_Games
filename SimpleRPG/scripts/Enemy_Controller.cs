using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controller : MonoBehaviour
{
    // 플레이어, HP바, HP바 캔버스, 플로팅 데미지, 플로팅 네임
    private GameObject player;
    public GameObject prfHPBar;
    private GameObject canvas;
    public GameObject floatingDamage;
    public GameObject floatingName;

    // 에너미 이미지, 위치, rigidbody, 플레이어 위치, 플레이어 컨트롤러(스크립트)
    private SpriteRenderer sprite;
    private Transform enemyTransform;
    private Rigidbody2D enemyRigid;
    private Transform playerTransform;
    private Player_Controller player_Controller;

    // 애니메이터
    private Animator animator;

    // 총 hp바, 플로팅 데미지바, 플로팅 네임바, 현재 hp바, hp바 위치 조정
    RectTransform hpBar;
    private GameObject floatingDamageInstant;
    RectTransform floatingNameBar;
    private Image nowHPbar;
    private float height;

    // 에너미 이름, 최대 hp, 현재 hp, 공격력
    public string enemyName;
    public float maxHP;
    private float nowHP;
    public float attackDamage;
    public int exp;
    public float speed;

    // 피격 여부, 다음 이동 방향, 현재 땅을 잘 밟고 있는지
    private bool attacked;
    private int nextMove;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("HPCanvas");

        // 총 hp바, 현재 hp바 초기화
        hpBar = Instantiate(prfHPBar, canvas.transform).GetComponent<RectTransform>();
        nowHPbar = hpBar.transform.GetChild(0).GetComponent<Image>();

        // 플로팅 네임바 초기화
        floatingNameBar = Instantiate(floatingName, canvas.transform).GetComponent<RectTransform>();
        floatingNameBar.GetComponent<Text>().text = enemyName;

        // 에너미 관련 초기화
        sprite = gameObject.GetComponent<SpriteRenderer>();
        enemyTransform = gameObject.GetComponent<Transform>();
        enemyRigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        // 플레이어 관련 초기화
        playerTransform = player.GetComponent<Transform>();
        player_Controller = player.GetComponent<Player_Controller>();

        // 초기 상태 설정
        height = 0.7f;
        attacked = false;
        nowHP = maxHP;
        nextMove = 0;

        // 이동 인보크 실행
        StartCoroutine(Think());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(DistanceBetweenEnemy() < 8.0f)
        {
            // hp바, 현재 hp바를 에너미 위에 표시
            Vector2 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + height));
            hpBar.position = _hpBarPos;
            nowHPbar.fillAmount = (float)nowHP / (float)maxHP;

            // 에너미 이름을 에너미 아래에 표시
            Vector2 _nameBarPos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y - height));
            floatingNameBar.position = _nameBarPos;
        }
        else
        {
            Vector2 _zeroPos = Camera.main.WorldToScreenPoint(new Vector2(-4f, -4f));
            hpBar.position = _zeroPos;
            floatingNameBar.position = _zeroPos;
            nowHPbar.fillAmount = 0;
        }

        // 에너미 진행방향이 절벽인지 확인
        Vector2 frontVec = new Vector2(enemyRigid.position.x + nextMove * 0.4f, enemyRigid.position.y);
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(raycast.collider == null)
        {
            nextMove *= -1;
        }

        // 에너미 이동
        EnemyMove();
    }

    private void EnemyMove()
    {
        // 피격 상태가 아니라면
        if (!attacked)
        {
            // 거리가 7f 미만이면
            if (DistanceBetweenEnemy() < 7.0f)
            {
                // 플레이어 방향으로 이동
                if (enemyTransform.position.x > playerTransform.position.x)
                {
                    sprite.flipX = false;
                    enemyRigid.velocity = new Vector3(-1f, enemyRigid.velocity.y, 0f);
                }
                else
                {
                    sprite.flipX = true;
                    enemyRigid.velocity = new Vector3(1f, enemyRigid.velocity.y, 0f);
                }
                animator.SetInteger("state", 1);
            }
            else
            {
                // nextMove에 따라 왼쪽, 정지, 오른쪽을 선택해 이동
                if (nextMove == -1)
                {
                    sprite.flipX = false;
                    animator.SetInteger("state", 1);
                    enemyRigid.velocity = new Vector3(-1f, enemyRigid.velocity.y, 0f);
                }
                else if (nextMove == 0)
                {
                    animator.SetInteger("state", 0);
                }
                else
                {
                    sprite.flipX = true;
                    animator.SetInteger("state", 1);
                    enemyRigid.velocity = new Vector3(1f, enemyRigid.velocity.y, 0f);
                }
            }
        }
    }

    // 지속적으로 호출되며 다음 이동 방향 지정
    IEnumerator Think()
    {
        int count = 0;
        while (count < 10000)
        {
            nextMove = Random.Range(-1, 2);
            count ++;
            yield return new WaitForSecondsRealtime(2.0f);
        }
    }

    // 에너미와 플레이어 사이 거리를 구하는 함수
    private float DistanceBetweenEnemy()
    {
        float xDistance = enemyTransform.position.x - playerTransform.position.x;
        float yDistance = enemyTransform.position.y - playerTransform.position.y;
       
        if (xDistance < 0)
            xDistance *= -1;
        if (yDistance < 0)
            yDistance *= -1;

        return xDistance + yDistance;
    }

    // 플레이어 공격과 충돌 판정
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 피격 상태가 아니라면
        if (!attacked)
        {
            // 피격당한 거라면
            if (collision.CompareTag("Attack"))
            {
                // 플레이어가 공격 상태라면
                if (player_Controller.GetAttacked() == 1)
                {
                    // 피격 상태로 하고 애니메이션 실행
                    attacked = true;
                    animator.SetInteger("state", 2);

                    // 공격 반대 방향으로 점프
                    if (sprite.flipX)
                    {
                        enemyRigid.AddForce(Vector2.left * 2f + Vector2.up * 3f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        enemyRigid.AddForce(Vector2.right * 2f + Vector2.up * 3f, ForceMode2D.Impulse);
                    }

                    // 플레이어 공격력 만큼 데미지를 입고 hp가 0 이하가 되면 사망 
                    float playerDamage = player_Controller.GetAttackDamage();
                    
                    // 플레이어가 입힌 데미지를 표시
                    if (floatingDamageInstant != null)
                    {
                        Destroy(floatingDamageInstant);
                    }
                    floatingDamageInstant = Instantiate(floatingDamage, canvas.transform);
                    floatingDamageInstant.GetComponent<Text>().text = playerDamage.ToString();
                    floatingDamageInstant.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector2(playerTransform.position.x, playerTransform.position.y + 2f));
                    Destroy(floatingDamageInstant, 1f);

                    nowHP -= playerDamage;
                    if (nowHP <= 0)
                    {
                        player_Controller.AddExp(exp);
                        Destroy(gameObject);
                        Destroy(hpBar.gameObject);
                        Destroy(floatingNameBar.gameObject);
                    }
                }
            }
        }
    }

    // 플레이어와 충돌 판정
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 플레이어와 부딪치면
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 입은 데미지를 붉게 표시
            if (floatingDamageInstant != null)
            {
                Destroy(floatingDamageInstant);
            }
            floatingDamageInstant = Instantiate(floatingDamage, canvas.transform);
            floatingDamageInstant.GetComponent<Text>().text = attackDamage.ToString();
            floatingDamageInstant.GetComponent<Text>().color = Color.red;
            floatingDamageInstant.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector2(playerTransform.position.x, playerTransform.position.y + 2f));
            Destroy(floatingDamageInstant, 1f);

            player_Controller.Damaged(attackDamage, enemyTransform.position.x > playerTransform.position.x);
        }
    }

    // 피격 상태 해소 함수. 애니메이터 사용
    public void AttackedOver()
    {
        attacked = false;
    }
}
