using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public GameObject player;

    public GameObject cursor;
    public Sprite[] cursorImage = new Sprite[5];

    float mouseX, mouseY;
    float xRotation, yRotation;

    public float sensitivityOrigin = 250;
    public float sensivityCurrent;

    int mode;
    bool onMenu;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        CursorMode(mode);
        sensivityCurrent = sensitivityOrigin;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onMenu)
        {
            Look();
        }
    }

    void Look()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY * sensivityCurrent * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX * sensivityCurrent * Time.deltaTime;

        player.transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
    }

    /// <summary>
    /// 커서를 바꾸는 함수
    /// </summary>
    /// <param name="_mode">0:잠금, 1:해제</param>
    /// <param name="_shape">0:기본, 1:주문, 2:제조, 3:설정, 4:이동</param>
    public void CursorMode(int _mode, int _shape = 0)
    {
        mode = _mode;

        if (mode == 0)
        {
            if(_shape == 0)
            {
                cursor.GetComponent<RectTransform>().offsetMin = new Vector2(395,295);
                cursor.GetComponent<RectTransform>().offsetMax = new Vector2(-395,-295);
            }
            else
            {
                cursor.GetComponent<RectTransform>().offsetMin = new Vector2(370, 265);
                cursor.GetComponent<RectTransform>().offsetMax = new Vector2(-370, -265);
            }
            cursor.GetComponent<Image>().sprite = cursorImage[_shape];
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMenu(bool b)
    {
        onMenu = b;
    }

    public void SetSensivity(float _sensivity)
    {
        sensivityCurrent = sensitivityOrigin * _sensivity;
    }
}
