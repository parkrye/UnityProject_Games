using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public DataManager dataManager;
    public FieldsManager fieldsManager;
    public PricesManager pricesManager;

    public string farmName;
    public Text farmNameText;
    public Text moneyText;
    public int farmMoney;

    public GameObject uiCanvas;
    public GameObject inputNamePrefab;
    GameObject inputNameInstance;
    public GameObject seedPrefab;
    GameObject[] seedInstances;

    public bool sale;
    public Text saleButtonText;

    public void Initialize()
    {
        uiCanvas.SetActive(false);
        seedInstances = new GameObject[dataManager.data.GetSeeds().Length];

        sale = false;

        farmName = dataManager.data.GetFarmName();
        farmNameText.text = farmName + "의 농장";
        farmMoney = dataManager.data.GetMoney();
        FarmMoneyChanged();
    }

    public void FarmMoneyChanged()
    {
        moneyText.text = "￦ " + farmMoney;
    }

    public void OnClickFarmName()
    {
        uiCanvas.SetActive(true);
        inputNameInstance = Instantiate(inputNamePrefab);
        inputNameInstance.transform.SetParent(uiCanvas.transform, false);
        inputNameInstance.GetComponentInChildren<Button>().onClick.AddListener(OnEnterFarmName);
    }

    public void OnEnterFarmName()
    {
        string temp = inputNameInstance.GetComponentInChildren<InputField>().text;
        if (!temp.Equals(""))
        {
            farmName = inputNameInstance.GetComponentInChildren<InputField>().text;
            farmNameText.text = farmName + "의 농장";
        }
        NaimingOff();
    }

    public void NaimingOff()
    {
        Destroy(inputNameInstance);
        uiCanvas.SetActive(false);
    }

    public void SowOn(int location)
    {
        uiCanvas.SetActive(true);
        for(int i = 0; i < 16; i++)
        {
            seedInstances[i] = Instantiate(seedPrefab);
            seedInstances[i].transform.SetParent(uiCanvas.transform, false);
            seedInstances[i].GetComponent<RectTransform>().offsetMax = new Vector2(- (6 - i % 4) * 100 - 50, - ((int)(i / 4) + 1) * 100 - 50);   // -right, -top
            seedInstances[i].GetComponent<RectTransform>().offsetMin = new Vector2(((i % 4) + 1) * 100 - 50, (4 - (int)(i / 4)) * 100 - 50);     // left, bottom
            int amount = dataManager.data.GetSeeds()[i];
            seedInstances[i].GetComponentInChildren<Text>().text = dataManager.data.GetPlantName(i) + "\n" + amount;
            if (amount < 1)
            {
                seedInstances[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                int temp = i;
                seedInstances[i].GetComponentInChildren<Button>().onClick.AddListener(() => Sow(location, temp));
            }
        }
    }

    public void Sow(int location, int seed)
    {
        fieldsManager.Plant(location, seed);
        dataManager.data.AddSeed(seed, -1);
        SowOff();
    }

    public void SowOff()
    {
        for (int i = 0; i < seedInstances.Length; i++)
            Destroy(seedInstances[i]);
        uiCanvas.SetActive(false);
    }

    public string GetFarmName()
    {
        return farmName;
    }

    public void ModifyFarmMoney(int m)
    {
        farmMoney += m;
    }

    public int GetFarmMoney()
    {
        return farmMoney;
    }

    public void OpenWareHouse()
    {
        uiCanvas.SetActive(true);
        int j = 0;
        for (int i = 0; i < 16; i++)
        {
            int amount = dataManager.data.GetSeeds()[i];
            if (amount > 0)
            {
                seedInstances[j] = Instantiate(seedPrefab);
                seedInstances[j].transform.SetParent(uiCanvas.transform, false);
                seedInstances[j].GetComponent<Button>().enabled = false;
                seedInstances[j].GetComponent<RectTransform>().offsetMax = new Vector2(-(6 - j % 4) * 100 - 50, -((int)(j / 4) + 1) * 100 - 50);   // -right, -top
                seedInstances[j].GetComponent<RectTransform>().offsetMin = new Vector2(((j % 4) + 1) * 100 - 50, (4 - (int)(j / 4)) * 100 - 50);     // left, bottom
                seedInstances[j].GetComponentInChildren<Text>().text = dataManager.data.GetPlantName(i) + "\n" + amount;
                j++;
            }
        }
    }

    public void OpenMarket()
    {
        uiCanvas.SetActive(true);
        for (int i = 0; i < 16; i++)
        {
            seedInstances[i] = Instantiate(seedPrefab);
            seedInstances[i].transform.SetParent(uiCanvas.transform, false);
            seedInstances[i].GetComponent<RectTransform>().offsetMax = new Vector2(-(6 - i % 4) * 100 - 50, -((int)(i / 4) + 1) * 100 - 50);   // -right, -top
            seedInstances[i].GetComponent<RectTransform>().offsetMin = new Vector2(((i % 4) + 1) * 100 - 50, (4 - (int)(i / 4)) * 100 - 50);     // left, bottom
            seedInstances[i].AddComponent<SeedPriceUpdate>();
            seedInstances[i].GetComponent<SeedPriceUpdate>().SetId(i);
            int tmp = i;
            seedInstances[i].GetComponentInChildren<Button>().onClick.AddListener(() => BuySeed(tmp));
        }
    }

    public void BuySeed(int i)
    {
        int productPrice = pricesManager.GetPrices()[seedInstances[i].GetComponent<SeedPriceUpdate>().GetId()] * 2;
        if (farmMoney >= productPrice)
        {
            farmMoney -= productPrice;
            dataManager.data.AddSeed(seedInstances[i].GetComponent<SeedPriceUpdate>().GetId(), 1);
            FarmMoneyChanged();
        }
    }

    public void OnSaleButton()
    {
        if (sale)
        {
            saleButtonText.text = "물주기";
            sale = false;
        }
        else
        {
            saleButtonText.text = "판매";
            sale = true;
        }
    }

    public bool NowSale()
    {
        return sale;
    }
}
