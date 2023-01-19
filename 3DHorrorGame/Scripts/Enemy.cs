using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject target;
    public Animator animator;
    public Transform[] waypoints;
    public AudioManager audioManager;
    public new AudioSource audio;

    Transform noisePoint;
    bool findPlayer, turn, hearNoise;
    float minDist, secDist, tmpDist, speed;
    float volume = 1f;
    int minIndex, secIndex;

    // Start is called before the first frame update
    void Start()
    {
        findPlayer = false;
        animator.SetBool("Find", false);
        nav.speed = speed;

        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = volume * audioManager.GetVolume();
        if (findPlayer)
        {
            nav.SetDestination(target.transform.position);
        }
        else if (hearNoise)
        {
            nav.SetDestination(noisePoint.position);
            if (Vector3.Distance(transform.position, noisePoint.position) < 2f)
            {
                hearNoise = false;
            }
        }
        else
        {
            NearPlayerFloor();
            NearMove();
        }
    }

    void NearPlayerFloor()
    {
        minDist = Vector3.Distance(target.transform.position, waypoints[0].position);
        minIndex = 0;
        secDist = Vector3.Distance(target.transform.position, waypoints[1].position);
        secIndex = 1;

        for (int i = 2; i < waypoints.Length; i++)
        {
            tmpDist = Vector3.Distance(target.transform.position, waypoints[i].position);
            if (tmpDist < secDist)
            {
                if(tmpDist < minDist)
                {
                    secDist = minDist;
                    secIndex = minIndex;
                    minDist = tmpDist;
                    minIndex = i;
                }
                else
                {
                    secDist = tmpDist;
                    secIndex = i;
                }
            }
        }
    }

    void NearMove()
    {
        if (!turn)
        {
            nav.SetDestination(waypoints[minIndex].position);
            if(Vector3.Distance(transform.position, waypoints[minIndex].position) < 2f)
            {
                turn = true;
            }
        }
        else
        {
            nav.SetDestination(waypoints[secIndex].position);
            if (Vector3.Distance(transform.position, waypoints[secIndex].position) < 2f)
            {
                turn = false;
            }
        }
    }

    public void LevelUp(int o, int d)
    {
        if (o > 10)
            o = 10;
        speed = (o / 5f) + (d / 2f) + 1f;
        nav.speed = speed;
    }

    public void FindPlayer()
    {
        findPlayer = true;
        animator.SetBool("Find", true);
    }

    public void LostPlayer()
    {
        findPlayer = false;
        animator.SetBool("Find", false);
    }

    public void HearNoise(Transform _noisePoint, float _noiseDist)
    {
        noisePoint = _noisePoint;
        if (Vector3.Distance(transform.position, noisePoint.position) < _noiseDist)
        {
            hearNoise = true;
        }
    }
}
