using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public StageClear stageClear;
    public GameObject head;
    new public AudioSource audio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audio.Play();
            head.SetActive(true);
            head.transform.position = BirdMove.Player.transform.position - BirdMove.Player.transform.right * 4;
            head.transform.LookAt(BirdMove.Player.transform);
            stageClear.OnGameOverEnter();
        }
    }
}
