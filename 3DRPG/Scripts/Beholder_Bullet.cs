using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder_Bullet : MonoBehaviour
{
    public new Rigidbody rigidbody;
    float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void Shot()
    {
        rigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Attacked(-damage);
        }
        Destroy(gameObject);
    }
}
