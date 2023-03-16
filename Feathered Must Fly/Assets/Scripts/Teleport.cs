using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform location;

    public bool moveX, moveY, moveZ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Vector3 moveLocation = other.transform.position;
            if (moveX) moveLocation.x = location.position.x;
            if (moveY) moveLocation.y = location.position.y;
            if (moveZ) moveLocation.z = location.position.z;
            other.transform.position = moveLocation;
        }
    }
}
