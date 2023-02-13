using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRange : MonoBehaviour
{
    public Transform respawnTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Takable" || collision.gameObject.tag == "Potion")
        {
            collision.transform.position = respawnTransform.position;
        }
    }
}
