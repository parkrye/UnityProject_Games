using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public int location;// 어디 밭인지
    public bool open;   // 해당 밭이 열려있는지
    public bool grow;   // 해당 밭에서 작물을 기르는 중인지
    public Plant plant; // 해당 밭에서 기르고 있는 작물
    public Text text;   // 작물 정보(이름, 레벨, 수분)
    public Image btn;   // 버튼 색

    public GameObject manager;

    public void Initilize(int n)
    {
        location = n;
        text = gameObject.GetComponentInChildren<Text>();
        btn = gameObject.GetComponent<Image>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void Planting(Plant seed)
    {
        grow = true;
        plant = seed;
        plant.Planting(location);
    }

    private void Update()
    {
        if (open)
        {
            if (grow)
            {
                text.text = plant.GetName() + "\n나이 : " + plant.GetAge() + "\n수분 : " + plant.GetMo();
            }
            else
            {
                text.text = "빈 땅";
            }
        }
        else
        {
            text.text = "잠김\n￦ " + manager.GetComponent<FieldsManager>().GetFieldPrice();
        }
    }

    public void OnClick()
    {
        if (open)
        {
            if (grow)
            {
                if (manager.GetComponent<FarmManager>().NowSale())
                {
                    Dump();
                }
                else
                {
                    plant.Watering();
                }
            }
            else
            {
                manager.GetComponent<FarmManager>().SowOn(location);
            }
        }
        else
        {
            if(manager.GetComponent<FarmManager>().GetFarmMoney() >= manager.GetComponent<FieldsManager>().GetFieldPrice())
            {
                manager.GetComponent<FarmManager>().ModifyFarmMoney(-manager.GetComponent<FieldsManager>().GetFieldPrice());
                open = true;
                btn.color = Color.white;
                manager.GetComponent<FieldsManager>().BuyField();
                manager.GetComponent<FieldsManager>().FieldOpen(location);
                manager.GetComponent<FarmManager>().FarmMoneyChanged();
            }
        }
    }

    public void Dump()
    {
        float price = Mathf.Lerp(-4f, 1f, ((float)plant.GetAge() / (float)plant.GetLimAge()));
        if (price < 0)
            price = 0f;
        price *= manager.GetComponent<PricesManager>().GetPrices()[plant.GetId()];
        price *= 5;
        manager.GetComponent<FarmManager>().ModifyFarmMoney((int)price);
        manager.GetComponent<FarmManager>().FarmMoneyChanged();

        grow = false;
        text.text = "빈 땅";
        Destroy(gameObject.GetComponent<Plant>());
    }

    public void SetGrow(bool g)
    {
        grow = g;
    }

    public bool IsGrow()
    {
        return grow;
    }

    public void SetOpen(bool o)
    {
        open = o;
        if (open)
        {
            btn.color = Color.white;
        }
        else
        {
            btn.color = Color.gray;
        }
    }

    public bool IsOpen()
    {
        return open;
    }

    public string ToSaveStyle()
    {
        string returnString = grow.ToString() + "/";
        if (grow)
        {
            returnString += plant.GetId() + "/" + plant.GetAge() + "/" + plant.GetMo();
        }
        else
        {
            returnString += "-1/-1/-1";
        }
        return returnString;
    }
}
