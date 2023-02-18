using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject player;
    public SceneController sceneController;
    PlayerController controller;

    void Start()
    {
        controller = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controller.Stop(false);
            sceneController.OnGoal();
        }
    }
}
