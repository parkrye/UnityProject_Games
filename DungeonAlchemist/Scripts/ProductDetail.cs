using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductDetail : MonoBehaviour
{
    public Text productName;
    public Text productPrice;

    public void ShowDetail(string name, float price)
    {
        productName.enabled = true;
        productPrice.enabled = true;
        productName.text = name;
        productPrice.text = "$" + (int)price;
    }

    public void ShowOver()
    {
        productName.enabled = false;
        productPrice.enabled = false;
    }
}
