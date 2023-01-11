using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bird : Enemy_Base
{
    public float flight_limit;  // 비행 한계 거리
    float flight_distance;      // 현재 비행 거리

    public override void Move()
    {
        rigidbody.velocity = new Vector2(move_Speed * direction, rigidbody.velocity.y);
    }

    // 조건에 해당하면 방향을 바꿈
    public override void Turn()
    {
        if (direction == 0)
        {
            direction = 1;
            flight_distance = 0f;
            spriteRenderer.flipX = true;
        }
        if (direction == -1)
        {
            // 벽이 있는지 확인
            Debug.DrawRay(rigidbody.position, Vector2.left, new Color(0f, 1f, 0f));
            rayHit = Physics2D.Raycast(rigidbody.position, Vector2.left, 0.5f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                direction = 1;
                spriteRenderer.flipX = true;
                flight_distance = 0f;
            }

            // 비행 거리를 벗어났으면
            if (flight_distance > flight_limit)
            {
                direction = 1;
                spriteRenderer.flipX = true;
                flight_distance = 0f;
            }
        }
        else
        {
            // 벽이 있는지 확인
            Debug.DrawRay(rigidbody.position, Vector2.right, new Color(0f, 1f, 0f));
            rayHit = Physics2D.Raycast(rigidbody.position, Vector2.right, 0.5f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                direction = -1;
                spriteRenderer.flipX = false;
                flight_distance = 0f;
            }

            // 비행 거리를 벗어났으면
            if (flight_distance > flight_limit)
            {
                direction = -1;
                spriteRenderer.flipX = false;
                flight_distance = 0f;
            }
        }
        flight_distance += Time.deltaTime;
    }
}
