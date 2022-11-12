using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * power);

        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
        if (worldpos.x < -0.1f) Destroy(gameObject);
        if (worldpos.y < -0.1f) Destroy(gameObject);
        if (worldpos.x > 1.1f) Destroy(gameObject);
        if (worldpos.y > 1.1f) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
