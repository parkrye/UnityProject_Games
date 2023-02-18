using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    public Transform player;
    public Transform dest;
    NavMeshAgent nav;
    public float dist;
    bool start;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        dist = 1000f;
        start = false;
    }

    void Update()
    {
        if (start)
        {
            transform.LookAt(new Vector3(player.position.x, 0f, player.position.z));
            SetDest(player);
        }
    }

    public void SetDest(Transform _dest = null)
    {
        if (!start) start = true;

        if (_dest != null) nav.destination = _dest.position;
        else nav.destination = dest.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            nav.enabled = false;
            enabled = false;
        }
    }

    public void Crash()
    {
        Destroy(nav);
        Destroy(this);
    }
}
