using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public float rotation;
    public float speed;
    public float shotTerm;

    public Transform gun;
    public GameObject bullet;
    bool shot;

    // Start is called before the first frame update
    void Start()
    {
        shot = true;
        if (rotation != 0)
            transform.Rotate(new Vector3(0, 0, rotation));
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, -speed));
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    IEnumerator Attack()
    {
        while (shot)
        {
            Vector3 bulletPos = new Vector3(gun.position.x, gun.position.y, 0f);
            Instantiate(bullet, bulletPos, Quaternion.identity);
            yield return new WaitForSeconds(shotTerm);
        }
    }
}
