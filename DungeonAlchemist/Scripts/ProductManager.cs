using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public ProductDetail productDetail;
    public Sprite productImage;
    public AudioSource dropAudio;

    public string label;
    public float price;
    public int elemental;

    public int[] taste = new int[5];         // ´Ü¸À, Â§¸À, ½Å¸À, ¾´¸À, ¸Å¿î¸À ¼ººÐ. 0~100
    public int[] elementals = new int[4];    // ¿ë±â, ÁöÇý, ÀÚÀ¯, ÀýÁ¦ ¼ººÐ. 0~100

    new Rigidbody rigidbody;
    bool takeOn;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        productDetail = GameObject.Find("TakeDetails").GetComponent<ProductDetail>();
    }

    public void TakeOn()
    {
        transform.rotation = Quaternion.identity;
        takeOn = true;
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;

        productDetail.ShowDetail(label, price);
    }

    public void TakeOff(bool throwing = false, Vector3 dir = new Vector3())
    {
        takeOn = false;
        rigidbody.isKinematic = false;
        rigidbody.freezeRotation = false;
        if (throwing)
        {
            rigidbody.AddForce(dir, ForceMode.Impulse);
        }
        productDetail.ShowOver();
    }

    public void Taking(Vector3 position)
    {
        if (takeOn)
        {
            if(Vector3.Distance(transform.position, position) > 0.2f)
            {
                transform.position = position;
            }
        }
    }
    
    public void SellOn(Transform _transform)
    {
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
    }

    public string GetName()
    {
        return label;
    }

    public float GetPrice()
    {
        return price;
    }

    public bool GetTakeOn()
    {
        return takeOn;
    }

    public int[] GetTaste()
    {
        return taste;
    }

    public int[] GetElementals()
    {
        return elementals;
    }

    public int GetElemental()
    {
        return elemental;
    }

    public void SetValues(string _label, float _price, int _elemental)
    {
        label = _label;
        price = _price;
        elemental = _elemental;
    }

    public Sprite GetSprite()
    {
        return productImage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        dropAudio.Play();
    }
}
