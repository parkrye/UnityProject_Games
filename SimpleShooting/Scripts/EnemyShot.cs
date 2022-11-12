using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float power;
    private GameObject playerDot;

    // Start is called before the first frame update
    void Start()
    {
        playerDot = GameObject.FindGameObjectWithTag("PlayerDot");
        Vector3 dir = playerDot.GetComponent<Transform>().position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, power));

        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
        if (worldpos.x < -0.1f) Destroy(gameObject);
        if (worldpos.y < -0.1f) Destroy(gameObject);
        if (worldpos.x > 1.1f) Destroy(gameObject);
        if (worldpos.y > 1.1f) Destroy(gameObject);
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
            Destroy(gameObject);
        }
    }
}
