using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrollerSetting : MonoBehaviour
{
    [SerializeField]    Rigidbody[] rigids;

    // Start is called before the first frame update
    void Start()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].gameObject.AddComponent<StrollerDetails>();
            for (int j = i + 1; j < rigids.Length; j++)
            {
                FixedJoint tmp = rigids[i].AddComponent<FixedJoint>();
                tmp.connectedBody = rigids[j];
                tmp.breakForce = 1000;
            }
        }
    }
}
