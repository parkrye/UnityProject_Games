using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public StageClear stageClear;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            stageClear.OnGameOverEnter();
        }
    }

    public void SetStageClear(StageClear _stageClear)
    {
        stageClear = _stageClear;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (stageClear)
            {
                stageClear.OnGameOverEnter();
            }
        }
    }
}
