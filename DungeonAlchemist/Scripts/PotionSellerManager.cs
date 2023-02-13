using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSellerManager : MonoBehaviour
{
    public MarketManager marketManager;
    public PotionReview potionReview;

    public PotionSeller[] potionSellers;
    public Transform[] customerTransforms = new Transform[3];
    public AudioSource sellAudio;

    public bool[] customers = new bool[3];
    public bool[] products;
    public int productCount;
    int customerCount;

    // Start is called before the first frame update
    void Start()
    {
        potionSellers = GetComponentsInChildren<PotionSeller>();
        products = new bool[potionSellers.Length];

        for(int i = 0; i < potionSellers.Length; i++)
        {
            potionSellers[i].SetNum(i);
        }
        customerCount = 0;
    }

    public int HasSpace()
    {
        for(int i = 0; i < 3; i++)
        {
            if (!customers[i])
            {
                return i;
            }
        }
        return -1;
    }

    public int UseSpace(int space)
    {
        customers[space] = true;
        customerCount++;
        return customerCount;
    }

    public void OutSpace(int space)
    {
        customers[space] = false;
        customerCount--;
    }

    public Transform GetTransform(int space)
    {
        return customerTransforms[space];
    }

    public int GetLevel()
    {
        return marketManager.GetLevel();
    }

    public void ProductOn(int num)
    {
        products[num] = true;
        productCount++;
    }

    public void ProductOff(int num)
    {
        products[num] = false;
        productCount--;
    }

    public bool HasProduct()
    {
        if(productCount > 0)
        {
            return true;
        }
        return false;
    }

    public void BuyProduct(int pocket)
    {
        int startPoint = Random.Range(0, products.Length);
        int dir = Random.Range(0, 2);
        if(dir == 0)
        {
            dir = -1;
        }
        for(int i = startPoint; i < products.Length && i >= 0; i += dir)
        {
            if (products[i])
            {
                if (pocket + marketManager.GetLevel() * 10 >= potionSellers[i].GetProductPrice())
                {
                    sellAudio.Play();
                    potionReview.EnQueue(potionSellers[i].GetProduct());
                    marketManager.ModifyMoney(potionSellers[i].GetProductPrice());
                    potionSellers[i].SellPotion();
                    break;
                }
            }
        }
    }
}
