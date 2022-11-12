using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool touchOn;
    Vector2 firstTouchPosition;
    Vector2 nowTouchPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        // 사용자가 터치패드를 터치한 상태라면
        if (touchOn)
        {
            // 초기 터치 지점과의 차이 만큼 이동
            nowTouchPosition = Input.mousePosition;
            Vector2 move = nowTouchPosition - firstTouchPosition;
            if(move.x > 40)
            {
                gameObject.GetComponent<Animator>().SetInteger("direction", 1);
            }
            else if(move.x < -40)
            {
                gameObject.GetComponent<Animator>().SetInteger("direction", -1);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetInteger("direction", 0);
            }
            transform.Translate(move/1000f);

            // 플레이 화면 밖으로는 벗어날 수 없음
            Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
            if (worldpos.x < 0.08f) worldpos.x = 0.08f;
            if (worldpos.y < 0.32f) worldpos.y = 0.32f;
            if (worldpos.x > 0.92f) worldpos.x = 0.92f;
            if (worldpos.y > 0.95f) worldpos.y = 0.95f;
            transform.position = Camera.main.ViewportToWorldPoint(worldpos);
        }
    }

    public void TouchDown()
    {
        touchOn = true;
        firstTouchPosition = Input.mousePosition;
    }

    public void TouchUp()
    {
        touchOn = false;
        gameObject.GetComponent<Animator>().SetInteger("direction", 0);
    }
}
