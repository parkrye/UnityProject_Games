using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform cameraTransform;
    public int dir;
    Rigidbody takedThing;

    private void Update()
    {
        transform.position = cameraTransform.position + cameraTransform.forward * 2f + transform.right / 2 * dir;
    }

    public void Take(Rigidbody targetRigid = null, bool throwBoth = false)
    {
        if(throwBoth == false)
        {
            if (targetRigid != null)
            {
                takedThing = targetRigid;
                takedThing.GetComponent<Collider>().enabled = false;
                FixedJoint tmp = gameObject.AddComponent<FixedJoint>();
                tmp.connectedBody = targetRigid;
            }
            else
            {
                takedThing.GetComponent<Collider>().enabled = true;
                takedThing = null;
                Destroy(GetComponent<FixedJoint>());
            }
        }
        else
        {
            takedThing.GetComponent<Collider>().enabled = true;
            takedThing = null;
            Destroy(GetComponent<FixedJoint>());
        }
    }

    public bool GetState()
    {
        return GetComponent<FixedJoint>() == null;
    }
}
