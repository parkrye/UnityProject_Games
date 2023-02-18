using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SceneController sceneController;
    public TrafficLight trafficLight;

    Rigidbody rigid;
    new Camera camera;
    public Image handupImg, lookupImg, swingLImg, swingRImg;
    
    public float moveSpeed, turnSpeed;
    float h, v, yRot, xRot;

    bool playing;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        color = handupImg.color;
        playing = true;
    }

    void Update()
    {
        if (playing) 
        {
            Action();
            Look();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playing) Move();
    }

    void Look()
    {
        yRot = Input.GetAxis("Mouse X") * turnSpeed + transform.eulerAngles.y;
        xRot = -Input.GetAxis("Mouse Y") * turnSpeed + transform.eulerAngles.x;

        if (xRot > 300f) xRot -= 360f;
        if (xRot > 30f) xRot = 30f;
        if (xRot < -30f) xRot = -30f;

        transform.eulerAngles = new Vector3(xRot, yRot, 0);
    }

    void Action()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            camera.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
            color.a = 1f;
            swingLImg.color = color;
            color.a = 0.1f;
            swingRImg.color = color;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            camera.transform.localPosition = new Vector3(0.5f, 0f, 0f);
            color.a = 0.1f;
            swingLImg.color = color;
            color.a = 1f;
            swingRImg.color = color;
        }
        else
        {
            camera.transform.localPosition = new Vector3(0f, 0f, 0f);
            color.a = 0.1f;
            swingLImg.color = color;
            swingRImg.color = color;
        }

        if (Input.GetMouseButton(0))
        {
            color.a = 1f;
            handupImg.color = color;
            trafficLight.HandUp(true);
        }
        else
        {
            color.a = 0.1f;
            handupImg.color = color;
            trafficLight.HandUp(false);
        }

        if (Input.GetMouseButton(1))
        {
            camera.fieldOfView = 20;
            color.a = 1f;
            lookupImg.color = color;
        }
        else
        {
            camera.fieldOfView = 60;
            color.a = 0.1f;
            lookupImg.color = color;
        }
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        rigid.velocity = (transform.forward * v + transform.right * h) * moveSpeed;
    }

    public void Stop(bool _kick)
    {
        playing = false;
        if (_kick)
        {
            rigid.constraints = RigidbodyConstraints.None;
            rigid.useGravity = false;
            sceneController.OnKick();
        }
    }
}
