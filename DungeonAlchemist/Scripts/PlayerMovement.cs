using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new Rigidbody rigidbody;
    Vector3 move;
    float v, h;

    public float moveSpeed;
    bool onMenu;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!onMenu)
        {
            Movement();
        }
    }

    void Movement()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        move = transform.forward * v + transform.right * h;
        rigidbody.velocity = move * moveSpeed;
    }

    public void OnMenu(bool b)
    {
        onMenu = b;
    }
}
