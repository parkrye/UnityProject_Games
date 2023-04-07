using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    private void OnJointBreak(float breakForce)
    {
        foreach (FixedJoint fixedJoint in GetComponents<FixedJoint>())
        {
            Destroy(fixedJoint);
        }
    }
}
