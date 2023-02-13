using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSeller : MonoBehaviour
{
    public PotionSellerManager potionSellerManager;
    public bool lockOn;
    public ProductManager product;
    public int num;

    GameObject ghost;

    private void Start()
    {
        ghost = transform.GetChild(0).gameObject;
    }

    public void SetNum(int _num)
    {
        num = _num;
    }

    public ProductManager GetProduct()
    {
        return product;
    }

    public float GetProductPrice()
    {
        return product.GetPrice();
    }

    public void SellPotion()
    {
        Destroy(product.gameObject);
        lockOn = false;
        product = null;
        potionSellerManager.ProductOff(num);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!lockOn)
        {
            if (other.tag == "Potion")
            {
                product = other.GetComponent<ProductManager>();
                if (!product.GetTakeOn())
                {
                    ghost.SetActive(false);
                    lockOn = true;
                    product.SellOn(transform);
                    potionSellerManager.ProductOn(num);
                }
                else
                {
                    ghost.SetActive(true);
                    product = null;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Potion")
        {
            ghost.SetActive(false);
            if (lockOn)
            {
                lockOn = false;
                product = null;
                potionSellerManager.ProductOff(num);
            }
        }
    }
}
