using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{
    public float rotation;
    public float speed;
    public float shotTerm;

    public Transform gun1;
    public Transform gun2;
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
            Vector3 bulletPos1 = new Vector3(gun1.position.x, gun1.position.y, 0f);
            Vector3 bulletPos2 = new Vector3(gun2.position.x, gun2.position.y, 0f);
            Instantiate(bullet, bulletPos1, Quaternion.identity);
            Instantiate(bullet, bulletPos2, Quaternion.identity);
            yield return new WaitForSeconds(shotTerm);
        }
    }
}
