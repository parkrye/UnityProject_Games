using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    public float speed;

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
        transform.Translate(new Vector2(0, speed));
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
