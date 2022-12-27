using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedPriceUpdate : MonoBehaviour
{
    public PricesManager pricesManager;
    public DataManager dataManager;
    public int id;
    public bool updating;

    // Update is called once per frame
    void Update()
    {
        if (updating)
        {
            gameObject.GetComponentInChildren<Text>().text = dataManager.data.GetPlantName(id) + "\n£Ü " + pricesManager.GetPrices()[id] * 2;
        }
    }

    public void SetId(int i)
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager").gameObject;
        pricesManager = manager.GetComponent<PricesManager>();
        dataManager = manager.GetComponent<DataManager>();
        id = i;
        updating = true;
        gameObject.GetComponentInChildren<Text>().text = dataManager.data.GetPlantName(id) + "\n£Ü " + pricesManager.GetPrices()[id] * 2;
    }

    public int GetId()
    {
        return id;
    }
}