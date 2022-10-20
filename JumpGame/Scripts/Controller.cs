using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // 플레이어
    public Component player;

    // 플레이어 컴포넌트
    private Transform player_Transform;
    private Rigidbody2D player_Rigidbody2D;
    private SpriteRenderer player_SpriteRenderer;
    private Animator animator;
    private AudioSource audioSource;

    // 이동 속도 / 점프 파워
    private float moveSpeed;
    private float jumpPower;

    // 현재 이동 방향, 땅에 서있는지 여부
    private bool inputLeft = false;
    private bool inputRight = false;
    private int maxCount;
    private int count;

    // 게임 오버
    public GameOver_Manager panel_GameOver;
    public GameObject underline;
    private AudioSource diedAudioSource;

    // 초기화
    private void Awake()
    {
        player_Transform = player.transform;
        player_Rigidbody2D = player.GetComponent<Rigidbody2D>();
        player_SpriteRenderer = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();

        diedAudioSource = panel_GameOver.GetComponent<AudioSource>();

        moveSpeed = 2;
        jumpPower = 300;
        inputLeft = false;
        inputRight = false;
        maxCount = 1;
        count = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // 왼쪽 버튼이 눌려져 있는 상태라면
        if (inputLeft)
        {
            // 프레임마다 왼쪽으로 moveSpeed만큼 이동한다
            player_Transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // 오른쪽 버튼이 눌려져 있는 상태라면
        if (inputRight)
        {
            // 프레임마다 오른쪽으로 moveSpeed만큼 이동한다
            player_Transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // 현재 플레이어의 수직 이동이 없을 때 == 땅에 서있을 때
        if (player_Rigidbody2D.velocity.y == 0)
        {
            count = maxCount;
            AnimvationPlay();
        }

        if(player_Transform.position.y <= underline.transform.position.y)
        {
            diedAudioSource.Play();

            Score_Manager scoreText = FindObjectOfType<Score_Manager>();
            scoreText.SetHighScore(scoreText.GetScore());

            panel_GameOver.Show();
            gameObject.SetActive(false);
        }
    }

    // 왼쪽 버튼이 눌렸을 때 이벤트
    public void LeftClickDown()
    {
        // 이미지 왼쪽으로, 애니메이션 작동, 왼쪽 버튼 눌림 활성화
        inputLeft = true;
        player_SpriteRenderer.flipX = false;
        AnimvationPlay();
    }

    // 왼쪽 버튼이 떼어졌을 때 이벤트
    public void LeftClickUp()
    {
        // 애니메이션 작동, 왼쪽 버튼 눌림 비활성화
        inputLeft = false;
        AnimvationPlay();
    }

    // 오른쪽 버튼이 눌렸을 때 이벤트
    public void RightClickDown()
    {
        // 이미지 오른쪽으로, 애니메이션 작동, 오른쪽 버튼 눌림 활성화
        inputRight = true;
        player_SpriteRenderer.flipX = true;
        AnimvationPlay();
    }

    // 오른쪽 버튼이 떼어졌을 때 이벤트
    public void RightClickUp()
    {
        // 애니메이션 작동, 오른쪽 버튼 눌림 비활성화
        inputRight = false;
        AnimvationPlay();
    }

    // 점프
    public void JumpClickDown()
    {
        // 땅 위에 있는 상태일 때만 점프 가능
        if (count > 0 & player_Rigidbody2D.velocity.y == 0)
        {
            count--;
            // moveSpeed의 힘으로 수직방향 밀기
            player_Rigidbody2D.AddForce(new Vector2(0, jumpPower));
            audioSource.Play();
            AnimvationPlay();
        }
    }

    // 애니메이션 설정 0:대기 1:이동
    private void AnimvationPlay()
    {
        // 왼쪽 이동, 오른쪽 이동, 점프 중 하나라도 해당되면
        if (inputLeft | inputRight | count < maxCount)
        {
            animator.SetFloat("moving", 1);
        }
        else
        {
            animator.SetFloat("moving", 0);
        }
    }
}
