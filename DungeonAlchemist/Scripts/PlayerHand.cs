using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHand : MonoBehaviour
{
    public OrderManager orderManager;
    public OptionManager optionManager;
    public PlayerLook playerLook;

    RaycastHit hit;
    ProductManager takeObject;
    bool take, onMenu;

    public float power;
    LayerMask ignoreLayer;

    void Start()
    {
        ignoreLayer = -1 - (1 << LayerMask.NameToLayer("IgnoreHitRaycast"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!onMenu)
        {
            PressESC();
            if (!take)
            {
                TakeUp();
            }
            else
            {
                TakeDown();
            }
        }
        else
        {
            PressESC();
        }
    }

    void TakeUp()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            if(hit.collider.tag == "Takable" || hit.collider.tag == "Potion")
            {
                playerLook.CursorMode(0, 4);
                if (Input.GetMouseButtonDown(0))
                {
                    playerLook.CursorMode(0, 0);
                    take = true;
                    takeObject = hit.collider.gameObject.GetComponent<ProductManager>();
                    takeObject.TakeOn();
                }
            }
            else if (hit.collider.tag == "Order")
            {
                playerLook.CursorMode(0, 1);
                if (Input.GetMouseButtonDown(0))
                {
                    orderManager.Order();
                }
            }
            else if (hit.collider.tag == "Pot")
            {
                playerLook.CursorMode(0, 2);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponentInChildren<PotionMaker>().PotionComplete();
                }
            }
            else if (hit.collider.tag == "Board")
            {
                playerLook.CursorMode(0, 3);
                if (Input.GetMouseButtonDown(0))
                {
                    optionManager.OpenOption();
                }
            }
            else
            {
                playerLook.CursorMode(0, 0);
            }
        }
    }

    void TakeDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            take = false;
            takeObject.TakeOff(true, power * transform.forward);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            take = false;
            takeObject.TakeOff();
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, ignoreLayer))
            {
                takeObject.Taking(hit.point);
            }
            else
            {
                takeObject.Taking(transform.position + transform.forward * 2);
            }
        }
    }

    void PressESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onMenu)
            {
                optionManager.OpenOption();
            }
            else
            {
                if (optionManager.GetOn())
                {
                    optionManager.OnQuitOptionButton();
                }
                else if (orderManager.GetOn())
                {
                    orderManager.OnQuitOrderButton();
                }
            }
        }
    }

    public void OnMenu(bool b)
    {
        onMenu = b;
    }
}
