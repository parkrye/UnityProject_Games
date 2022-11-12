using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotTime = 0.2f;
    bool generate;

    private void Start()
    {
        generate = true;
        StartCoroutine(ShotBullet());
    }

    IEnumerator ShotBullet()
    {
        while (generate)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y + 0.5f, 2f);
            Instantiate(bulletPrefab, bulletPos, transform.rotation);
            yield return new WaitForSeconds(shotTime);
        }
    }
}
