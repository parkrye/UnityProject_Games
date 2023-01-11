using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public AudioSource audio;

    // 박스 파괴 애니메이션 시작 이벤트
    public void BreakBox()
    {
        audio.Play();
        GetComponent<Animator>().SetTrigger("Hit");
    }

    // 박스 파괴 애니메이션 종료 이벤트
    void AnimationEnd()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerManager>().BreakBox();
        }
        Destroy(gameObject);
    }

    // 함정 태그와 충돌시 파괴
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Destroy(gameObject);
        }
    }

    // 추락 이벤트
    public void SlipDown()
    {
        StartCoroutine(SandFlash());
    }

    IEnumerator SandFlash()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
