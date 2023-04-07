using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    new public GameObject camera;
    public HandController leftHand, rightHand;
    new Rigidbody rigidbody;

    public float speed;

    float mouseX, mouseY, h, v, xRotation, yRotation, sensitivityOrigin, sensivityCurrent;
    float speedMul = 1;

    int cursorMode;
    bool onPlay, twoHanded;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        speedMul = 1;
        cursorMode = 0;
        onPlay = true;
        sensitivityOrigin = 300;
        sensivityCurrent = sensitivityOrigin;

        CursorMode(cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlay)
        {
            Look();
            Walk();
            Take();
            Crawl();
            Jump();
        }
        else
        {
            PressESC();
        }
    }

    // 마우스 이벤트
    void Look()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY * sensivityCurrent * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        yRotation += mouseX * sensivityCurrent * Time.deltaTime;

        transform.localEulerAngles = new Vector3(0f, yRotation, 0f);
        camera.transform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
    }

    /// <summary>
    /// 커서 모드 변경
    /// </summary>
    /// <param name="_mode">0:고정, 보이지 않음 1:해제, 보임</param>
    public void CursorMode(int _mode)
    {
        cursorMode = _mode;

        if (cursorMode == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMenu(bool b)
    {
        onPlay = b;
    }

    public void SetSensivity(float _sensivity)
    {
        sensivityCurrent = sensitivityOrigin * _sensivity;
    }

    void Take()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (leftHand.GetState())
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5f))
                {
                    if (hit.collider.tag == "OneHand" || hit.collider.tag == "Baby")
                    {
                        hit.collider.gameObject.transform.position = leftHand.transform.position;
                        leftHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                    }
                    else if (hit.collider.tag == "TwoHand")
                    {
                        if (rightHand.GetState())
                        {
                            twoHanded = true;
                            hit.collider.gameObject.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;
                            leftHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                            rightHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                        }
                    }
                }
            }
            else
            {
                leftHand.Take();
                if(twoHanded) rightHand.Take(null, twoHanded);
                twoHanded = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (rightHand.GetState())
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5f))
                {
                    if (hit.collider.tag == "OneHand" || hit.collider.tag == "Baby")
                    {
                        hit.collider.gameObject.transform.position = rightHand.transform.position;
                        rightHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                    }
                    else if (hit.collider.tag == "TwoHand")
                    {
                        if (leftHand.GetState())
                        {
                            twoHanded = true;
                            hit.collider.gameObject.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;
                            leftHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                            rightHand.Take(hit.collider.gameObject.GetComponent<Rigidbody>());
                        }
                    }
                }
            }
            else
            {
                rightHand.Take();
                if (twoHanded) leftHand.Take(null, twoHanded);
                twoHanded = false;
            }
        }
    }

    void PressESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPlay)
            {

            }
            else
            {

            }
        }
    }

    // 키보드 이벤트
    void Walk()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMul = 1.2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMul = 1;
        }

        rigidbody.AddForce((transform.right * h + transform.forward * v) * speed * speedMul, ForceMode.Force);
    }

    void Crawl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speedMul = 0.9f;
            camera.transform.localPosition = new Vector3(0, 0, 0.1f);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedMul = 1;
            camera.transform.localPosition = new Vector3(0, 0.5f, 0.1f);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * speed * 5, ForceMode.Impulse);
        }
    }
}
