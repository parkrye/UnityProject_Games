using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    public DataManager dataManager;
    public int[] prices;

    public void Initialize()
    {
        prices = dataManager.data.GetPrices();
    }

    public void PriceChange()
    {
        for (int i = 0; i < 16; i++)
        {
            float range = prices[i] / 10f;
            int change = (int)Mathf.Round(Random.Range(-range, range));
            prices[i] += change;
        }
    }

    public int[] GetPrices()
    {
        return prices;
    }
}
