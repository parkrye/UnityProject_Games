using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameObject minutes_Tens;
    public GameObject minutes_Ones;
    public GameObject seconds_Tens;
    public GameObject seconds_Ones;

    SpriteRenderer[] minTs;
    SpriteRenderer[] minOs;
    SpriteRenderer[] secTs;
    SpriteRenderer[] secOs;

    int min;
    int sec;
    bool clocking;

    // Start is called before the first frame update
    void Start()
    {
        min = 0;
        sec = 0;
        clocking = true;

        minTs = minutes_Tens.GetComponentsInChildren<SpriteRenderer>();
        minOs = minutes_Ones.GetComponentsInChildren<SpriteRenderer>();
        secTs = seconds_Tens.GetComponentsInChildren<SpriteRenderer>();
        secOs = seconds_Ones.GetComponentsInChildren<SpriteRenderer>();

        StartCoroutine(Clocking());
    }

    IEnumerator Clocking()
    {
        while (clocking)
        {
            sec += 1;
            if (sec == 60)
            {
                min += 1;
                sec = 0;
            }
            if (min == 60)
            {
                min = 0;
            }
            SetClock();
            yield return new WaitForSeconds(1);
        }
    }

    void SetClock()
    {
        int min_ten = min / 10;
        int min_one = min % 10;
        int sec_ten = sec / 10;
        int sec_one = sec % 10;

        for (int i = 0; i < 7; i++)
        {
            minTs[i].enabled = true;
            minOs[i].enabled = true;
            secTs[i].enabled = true;
            secOs[i].enabled = true;
        }

        switch (min_ten)
        {
            default:
            case 0:
                minTs[3].enabled = false;
                break;
            case 1:
                minTs[0].enabled = false;
                minTs[1].enabled = false;
                minTs[3].enabled = false;
                minTs[4].enabled = false;
                minTs[6].enabled = false;
                break;
            case 2:
                minTs[1].enabled = false;
                minTs[5].enabled = false;
                break;
            case 3:
                minTs[1].enabled = false;
                minTs[4].enabled = false;
                break;
            case 4:
                minTs[0].enabled = false;
                minTs[4].enabled = false;
                minTs[6].enabled = false;
                break;
            case 5:
                minTs[2].enabled = false;
                minTs[4].enabled = false;
                break;
            case 6:
                minTs[2].enabled = false;
                break;
            case 7:
                minTs[3].enabled = false;
                minTs[4].enabled = false;
                minTs[6].enabled = false;
                break;
            case 8:
                break;
            case 9:
                minTs[4].enabled = false;
                break;
        }

        switch (min_one)
        {
            default:
            case 0:
                minOs[3].enabled = false;
                break;
            case 1:
                minOs[0].enabled = false;
                minOs[1].enabled = false;
                minOs[3].enabled = false;
                minOs[4].enabled = false;
                minOs[6].enabled = false;
                break;
            case 2:
                minOs[1].enabled = false;
                minOs[5].enabled = false;
                break;
            case 3:
                minOs[1].enabled = false;
                minOs[4].enabled = false;
                break;
            case 4:
                minOs[0].enabled = false;
                minOs[4].enabled = false;
                minOs[6].enabled = false;
                break;
            case 5:
                minOs[2].enabled = false;
                minOs[4].enabled = false;
                break;
            case 6:
                minOs[2].enabled = false;
                break;
            case 7:
                minOs[3].enabled = false;
                minOs[4].enabled = false;
                minOs[6].enabled = false;
                break;
            case 8:
                break;
            case 9:
                minOs[4].enabled = false;
                break;
        }

        switch (sec_ten)
        {
            default:
            case 0:
                secTs[3].enabled = false;
                break;
            case 1:
                secTs[0].enabled = false;
                secTs[1].enabled = false;
                secTs[3].enabled = false;
                secTs[4].enabled = false;
                secTs[6].enabled = false;
                break;
            case 2:
                secTs[1].enabled = false;
                secTs[5].enabled = false;
                break;
            case 3:
                secTs[1].enabled = false;
                secTs[4].enabled = false;
                break;
            case 4:
                secTs[0].enabled = false;
                secTs[4].enabled = false;
                secTs[6].enabled = false;
                break;
            case 5:
                secTs[2].enabled = false;
                secTs[4].enabled = false;
                break;
            case 6:
                secTs[2].enabled = false;
                break;
            case 7:
                secTs[3].enabled = false;
                secTs[4].enabled = false;
                secTs[6].enabled = false;
                break;
            case 8:
                break;
            case 9:
                secTs[4].enabled = false;
                break;
        }

        switch (sec_one)
        {
            default:
            case 0:
                secOs[3].enabled = false;
                break;
            case 1:
                secOs[0].enabled = false;
                secOs[1].enabled = false;
                secOs[3].enabled = false;
                secOs[4].enabled = false;
                secOs[6].enabled = false;
                break;
            case 2:
                secOs[1].enabled = false;
                secOs[5].enabled = false;
                break;
            case 3:
                secOs[1].enabled = false;
                secOs[4].enabled = false;
                break;
            case 4:
                secOs[0].enabled = false;
                secOs[4].enabled = false;
                secOs[6].enabled = false;
                break;
            case 5:
                secOs[2].enabled = false;
                secOs[4].enabled = false;
                break;
            case 6:
                secOs[2].enabled = false;
                break;
            case 7:
                secOs[3].enabled = false;
                secOs[4].enabled = false;
                secOs[6].enabled = false;
                break;
            case 8:
                break;
            case 9:
                secOs[4].enabled = false;
                break;
        }
    }

    public void StopClock()
    {
        clocking = false;
    }
}
