using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public Transform area;
    public Transform goal1, goal2;
    public GameObject goalPrefab;

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "BallArea")
        {
            transform.position = area.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Goal1")
        {
            transform.position = area.position;
            Instantiate(goalPrefab, goal1.transform);
        }
        if (other.name == "Goal2")
        {
            transform.position = area.position;
            Instantiate(goalPrefab, goal2.transform);
        }
    }
}
