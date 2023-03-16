using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OwlMovement : MonoBehaviour
{
    public StageClear stageClear;
    public GameObject owl, mark;
    new public AudioSource audio;
    bool dontMove;
    Vector3 prevPos;

    void Start()
    {
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (dontMove)
        {
            if(Vector3.Distance(BirdMove.Player.transform.position, prevPos) > 1f)
            {
                owl.SetActive(true);
                owl.transform.position = BirdMove.Player.transform.position + BirdMove.Player.transform.forward * 4 - BirdMove.Player.transform.right * 4;
                owl.transform.LookAt(BirdMove.Player.transform);
                stageClear.OnGameOverEnter();
            }
        }
        else
        {
            prevPos = BirdMove.Player.transform.position;
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));
            for(int i = 0; i < 5; i++)
            {
                mark.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                mark.SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
            audio.Play();
            mark.SetActive(true);
            dontMove = true;
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            for (int i = 0; i < 5; i++)
            {
                mark.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                mark.SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
            audio.Play();
            mark.SetActive(false);
            dontMove = false;
        }
    }
}
