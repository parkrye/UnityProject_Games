using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHp;
    private int hp;
    public GameObject explosion;
    bool show;

    private void Start()
    {
        hp = maxHp;
        show = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
        if (show)
        {
            // 시야에 들어온 뒤 밖으로 나가면 파괴
            if (worldpos.x < -0.1f) Destroy(gameObject);
            if (worldpos.y < 0.34f) Destroy(gameObject);
            if (worldpos.x > 1.1f) Destroy(gameObject);
            if (worldpos.y > 1.1f) Destroy(gameObject);
        }
        else
        {
            // 시야에 들어왔는지
            if (worldpos.x > 0f && worldpos.y > 0f && worldpos.x < 1f && worldpos.y < 1f)
                show = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDot"))
        {
            LifeManager.AddLife(-1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
            if (hp <= 0)
            {
                ScoreManager.AddScore(maxHp);
                GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
                explode.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f) / 2;
                Destroy(gameObject);
            }
        }
    }
}