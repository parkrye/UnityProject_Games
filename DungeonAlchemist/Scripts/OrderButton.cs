using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderButton : MonoBehaviour
{
    public OrderManager orderManager;
    public GameObject product;
    public Transform orderTransform;
    ProductManager productManager;
    Text text;
    Image[] image;

    // Start is called before the first frame update
    void Start()
    {
        productManager = product.GetComponent<ProductManager>();
        text = GetComponentInChildren<Text>();
        image = GetComponentsInChildren<Image>();
        text.text = productManager.GetName() + "\n$" + productManager.GetPrice();
        image[1].sprite = productManager.GetSprite();
    }

    public void OnOrderButton()
    {
        if (orderManager.ModifyMoney(-productManager.GetPrice()))
        {
            Instantiate(product, orderTransform.position, orderTransform.rotation);
        }
    }
}
