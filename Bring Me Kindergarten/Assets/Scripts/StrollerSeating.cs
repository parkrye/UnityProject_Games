using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollerSeating : MonoBehaviour
{
    public GameObject belt;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Baby")
        {
            collision.transform.position = transform.position;
            collision.transform.rotation = transform.rotation;
            collision.gameObject.AddComponent<FixedJoint>();
            collision.gameObject.GetComponent<FixedJoint>().connectedBody = GetComponent<Rigidbody>();
            collision.gameObject.GetComponent<FixedJoint>().breakForce = 1000;
            belt.transform.position = transform.position + transform.forward / 2 + transform.up / 2;
            belt.SetActive(true);
        }
    }

    private void OnJointBreak(float breakForce)
    {
        belt.SetActive(false);
    }
}
