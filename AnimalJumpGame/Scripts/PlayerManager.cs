using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    new Rigidbody2D rigidbody;
    RaycastHit2D rayHit;
    Vector3 inCameraPos;
    public AudioSource[] audios;

    public GameObject failCanvas;

    public int playerNum;       // 캐릭터 넘버
    public float move_Speed;    // 이동 속도
    public float jump_Power;    // 접프 정도
    bool isGround;              // 현재 땅에 닿아있는지
    bool breakBox;              // 현재 박스를 부쉈는지

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        isGround = false;
        breakBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataManager.Manager.GetMenu())
        {
            OpenMenu();
            CheckGround();
            PlayerMove();
            PlayerJump();
        }
    }

    // 플레이어 이동
    void PlayerMove()
    {
        if(playerNum == 0)
        {
            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(-move_Speed, rigidbody.velocity.y);
            }
            else if (Input.GetKey("d") && ! Input.GetKey("a"))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(move_Speed, rigidbody.velocity.y);
            }
            else
            {
                if (isGround)
                {
                    animator.SetBool("Move", false);
                }
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }
        else
        {
            if (Input.GetKey("left") && !Input.GetKey("right"))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(-move_Speed, rigidbody.velocity.y);
            }
            else if (Input.GetKey("right") && !Input.GetKey("left"))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(move_Speed, rigidbody.velocity.y);
            }
            else
            {
                if (isGround)
                {
                    animator.SetBool("Move", false);
                }
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }

        // 카메라 밖으로 나갈 수 없음
        inCameraPos = Camera.main.WorldToViewportPoint(transform.position);
        if (inCameraPos.x < 0f) inCameraPos.x = 0f;
        if (inCameraPos.x > 1f) inCameraPos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(inCameraPos);
    }

    // 플레이어 점프
    void PlayerJump()
    {
        if (isGround)
        {
            if (playerNum == 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    audios[0].Play();
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Power);
                    animator.SetBool("Move", true);
                    isGround = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audios[0].Play();
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Power);
                    animator.SetBool("Move", true);
                    isGround = false;
                }
            }
        }
    }

    // 플레이어가 땅에 닿아있는지 확인
    void CheckGround()
    {
        if(rigidbody.velocity.y <= 0)
        {
            Debug.DrawRay(rigidbody.position, Vector2.down, new Color(0f, 1f, 0f));
            rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance <= 1.5f)
                {
                    isGround = true;
                }
            }
        }
    }

    // 메뉴 열기
    void OpenMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            DataManager.Manager.OpenManu();
        }
    }

    // 플레이어 충돌 이벤트
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn" || collision.gameObject.tag == "Trap")                // 리스폰, 함정 태그와 충돌시
        {
            Die();                                                                                      // 캐릭터 사망
        }
        else if (collision.gameObject.tag == "Box")                                                     // 박스 태그와 충돌시
        {
            if (!breakBox)                                                                              // 현재 박스를 부수고 있지 않고
            {
                if(rigidbody.velocity.y >= 0 && transform.position.y <= collision.transform.position.y) // 캐릭터가 상승중, 캐릭터 위치가 박스 아래라면
                {
                    breakBox = true;                                                                    // 박스 파괴중 상태에 돌입하고
                    collision.gameObject.GetComponent<BoxManager>().BreakBox();                         // 박스를 파괴. 파괴 애니메이션 후 파괴 상태에서 벗어남
                }
            }
        }
        else if (collision.gameObject.tag == "Fall")                                                    // 추락 태그와 충돌시
        {
            if (rigidbody.velocity.y <= 0 && transform.position.y >= collision.transform.position.y)    // 캐릭터가 하강중, 캐릭터 위치가 추락 위라면
            {
                collision.gameObject.GetComponent<BoxManager>().SlipDown();                             // 추락 태그 오브젝트가 추락함
            }
        }
        else if (collision.gameObject.tag == "Enemy")                                                   // 적 태그와 충돌시
        {
            if (rigidbody.velocity.y < 0 && transform.position.y > collision.transform.position.y)      // 캐릭터가 하강중, 캐릭터 위치가 적 위라면
            {
                collision.transform.gameObject.GetComponent<Enemy_Base>().Die();                        // 적 캐릭터 사망
            }
            else
            {
                Die();                                                                                  // 아니라면 플레이어 캐릭터 사망
            }
        }
    }

    // 박스 파괴 상태에서 벗어남. 박스 애니메이션이 호출
    public void BreakBox()
    {
        breakBox = false;
    }

    // 플레이어 사망
    public void Die()
    {
        audios[1].Play();
        Time.timeScale = 0f;
        Instantiate(failCanvas);
    }


}
