using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public int stageLevel;
    public GameObject succesCanvas;
    public GameObject clearCanvas;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            if(stageLevel < 10)
            {
                if (DataManager.Manager.GetOpen() < stageLevel)
                {
                    DataManager.Manager.SetOpen(stageLevel);
                }
                Instantiate(succesCanvas);
            }
            else
            {
                DataManager.Manager.SetOpen(0);
                Instantiate(clearCanvas);
            }
        }
    }
}
