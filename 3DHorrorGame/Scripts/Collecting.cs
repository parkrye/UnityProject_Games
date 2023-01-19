using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecting : MonoBehaviour
{
    public Transform player;
    public BookSpawner bookSpawner;
    public new AudioSource audio;
    public AudioManager audioManager;

    public int mode = 0;

    float dist;
    float volume = 1f;

    void OnMouseOver()
    {
        if (player)
        {
            dist = Vector3.Distance(player.position, transform.position);
            if (dist < 5)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    audio.volume = volume * audioManager.GetVolume();
                    audio.Play();
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(SoundOver());
                }
            }
        }
    }

    IEnumerator SoundOver()
    {
        yield return new WaitForSeconds(0.895f);
        player.gameObject.GetComponent<ObjectManager>().ObjectCollect();
        gameObject.SetActive(false);
        if(mode == 1)
        {
            bookSpawner.SpawnNewBook();
        }
    }

    public void SetValues(int _mode)
    {
        mode = _mode;
    }
}
