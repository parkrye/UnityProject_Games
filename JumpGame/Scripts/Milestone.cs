using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milestone : MonoBehaviour
{
    // 플랫폼 관리자
    public Platform_Manager platform_Manager;

    // 점수 관리자
    public Score_Manager scoreManager;

    // 삭제 처리
    private bool lost;
    private float scaleSpeed = 1.0f;
    private AudioSource audioSource;

    private void Awake()
    {
        lost = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        scoreManager.UpdateScore(1);
        platform_Manager.SetFlag();
        lost = true;
    }

    private void FixedUpdate()
    {
        if (transform.localScale.y < 0.3f)
        {
            Destroy(gameObject);
        }

        if (lost)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 1.0f * scaleSpeed * Time.deltaTime, 0.0f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f * scaleSpeed * Time.deltaTime, 0.0f);
        }
    }
}
