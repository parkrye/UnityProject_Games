using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public int canvasType;
    public AudioSource audio;

    void Update()
    {
        if(canvasType == 0)
        {
            OnAnyKey();
        }
        else if(canvasType == 1)
        {
            if (Input.anyKeyDown)
            {
                audio.Play();
                SceneManager.LoadScene("01_Map");
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
        }
        else if (canvasType == 2)
        {
            if (Input.anyKeyDown)
            {
                audio.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
        }
        else if(canvasType == 3)
        {
            if (Input.anyKeyDown)
            {
                audio.Play();
                SceneManager.LoadScene("00_Home");
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
        }
    }

    void OnAnyKey()
    {
        if (Input.anyKeyDown)
        {
                audio.Play();
            Destroy(gameObject);
        }
    }
}
