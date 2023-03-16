using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public enum State { None, Weak, Noraml, Strong };
    public State state;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            switch (state)
            {
                case State.None: break;
                case State.Weak: other.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Force); break; 
                case State.Noraml: other.GetComponent<Rigidbody>().AddForce(transform.forward * 30f, ForceMode.Force); break;
                case State.Strong: other.GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Force); break;
            }
        }
    }
}
