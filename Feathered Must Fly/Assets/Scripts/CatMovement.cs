using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CatMovement : MonoBehaviour
{
    new public AudioSource audio;
    public StageClear stageClear;
    Vector3 target, start;
    float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LookAround());
        StartCoroutine(Weep());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target, transform.position) > 100f) speed = 1f;
        else speed = 0.02f;
        transform.position += (target - start) * speed * Time.deltaTime;
    }

    IEnumerator LookAround()
    {
        while (true)
        {
            start = transform.position;
            target = BirdMove.Player.GetComponent<Transform>().position;
            target.y = 0f;
            transform.LookAt(target);
            yield return new WaitForSeconds(10f);
        }

    }

    IEnumerator Weep()
    {
        while (true)
        {
            audio.Play();
            yield return new WaitForSeconds(Random.Range(3f, 13f));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stageClear.OnGameOverEnter();
        }
    }
}
