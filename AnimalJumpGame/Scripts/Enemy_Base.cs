using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public new Rigidbody2D rigidbody;
    public RaycastHit2D rayHit;
    public AudioSource die;

    public int direction;   // 이동 방향
    public int move_Speed;  // 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        die = GetComponent<AudioSource>();

        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();
    }

    public virtual void Move()
    {

    }

    public virtual void Turn()
    {

    }

    public void Die()
    {
        die.Play();
        GetComponent<PlatformEffector2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("Die");
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }

    public void ChangeDirection()
    {
        direction = -direction;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
