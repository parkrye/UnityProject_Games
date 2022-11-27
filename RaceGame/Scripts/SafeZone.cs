using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public RankSystem rankSystem;
    public GameObject getOutText;
    int counter = 0;

    private void Start()
    {
        getOutText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Safe")
        {
            if (other.gameObject.tag == "CarObject")
            {
                rankSystem.DecSet(0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "Danger")
        {
            if (other.gameObject.tag == "Foot")
            {
                getOutText.SetActive(true);
                counter += 1;
                if (counter > 80)
                {
                    rankSystem.ScoreUp(-1);
                    counter = 40;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Danger")
        {
            if (other.gameObject.tag == "Foot")
            {
                getOutText.SetActive(false);
                counter = 0;
            }
        }
        else if (gameObject.tag == "Safe")
        {
            if (other.gameObject.tag == "CarObject")
            {
                rankSystem.DecSet(1);
            }
        }
    }
}
