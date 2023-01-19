using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThudManager : MonoBehaviour
{
    public AudioSource[] audioSource = new AudioSource[5];
    public AudioManager audioManager;
    float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Thud());
    }

    IEnumerator Thud()
    {
        float delay = Random.Range(30f, 120f);
        int index = Random.Range(0, 5);
        while (true)
        {
            yield return new WaitForSeconds(delay);
            audioSource[index].volume = volume * audioManager.GetVolume();
            audioSource[index].Play();
            delay = Random.Range(30f, 120f);
            index = Random.Range(0, 5);
        }
    }
}
