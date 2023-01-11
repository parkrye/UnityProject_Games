using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public int buttonType;      // 0: 오브젝트 생성, 1: 오브젝트 삭제, 2: 접촉을 벗어나면 오브젝트 삭제, 3: 접촉을 벗어나면 오브젝트 생성
    public GameObject[] objects;
    SpriteRenderer spriteRenderer;
    public int buttonOn;
    AudioSource audio;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        spriteRenderer.color = Color.gray;
        buttonOn = 0;
    }

    private void Update()
    {
        if (buttonOn > 0)
        {
            switch (buttonType)
            {
                default:
                case 0:
                case 2:
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SetActive(true);
                    }
                    break;
                case 1:
                case 3:
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (buttonType)
            {
                default:
                case 0:
                case 1:
                    break;
                case 2:
                    spriteRenderer.color = Color.gray;
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SetActive(false);
                    }
                    break;
                case 3:
                    spriteRenderer.color = Color.gray;
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SetActive(true);
                    }
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audio.Play();
            spriteRenderer.color = Color.white;
            buttonOn++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonOn--;
        }
    }
}
