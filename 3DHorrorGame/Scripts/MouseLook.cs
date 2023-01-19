using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    public float originMouseXSensitivity = 100f;
    public float mouseXSensitivity = 1f;

    public Transform playerBody;
    public new Camera camera;

    public Transform originPos;
    public Slider mouseSlider, lightSlider;

    public Light directionalLight;

    float xRotation = 0f, zRotation = 0f;
    bool onMenu;

    // Update is called once per frame
    void Update()
    {
        if (!onMenu)
        {
            LookUp();
            Grad();
            Main();
        }
    }
    void LookUp()
    {
        if (Input.GetMouseButton(1))
        {
            camera.fieldOfView = 30f;
        }
        else
        {
            camera.fieldOfView = 60f;
        }
    }

    void Main()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, zRotation);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Grad()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (zRotation < 10f)
            {
                transform.Translate(-0.2f, 0f, 0f);
                zRotation += 2.5f;
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (zRotation > -10f)
            {
                transform.Translate(0.2f, 0f, 0f);
                zRotation -= 2.5f;
            }
        }
        else
        {
            if (zRotation > 0f)
            {
                transform.Translate(0.2f, 0f, 0f);
                zRotation -= 2.5f;
            }
            else if (zRotation < 0f)
            {
                transform.Translate(-0.2f, 0f, 0f);
                zRotation += 2.5f;
            }
        }
    }

    public void OpenMenu()
    {
        onMenu = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitMenu()
    {
        onMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMouseChanged()
    {
        mouseXSensitivity = originMouseXSensitivity * (mouseSlider.value / 10f);
    }

    public void OnLightChanged()
    {
        directionalLight.intensity = lightSlider.value;
    }

    public void InitializeOptions()
    {
        mouseSlider.value = 10f;
        OnMouseChanged();
        lightSlider.value = 0.8f;
        OnLightChanged();
    }
}