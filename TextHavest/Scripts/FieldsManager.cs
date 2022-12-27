using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsManager : MonoBehaviour
{
    public DataManager dataManager;

    public GameObject fields;                               // UI ³» ¹ç Æú´õ
    public GameObject fieldPrefab;                          // ¹ç ÇÁ¸®ÆÕ
    public GameObject[] fieldList = new GameObject[25];     // ¹ç ¹è¿­
    public int fieldPrice;                                  // ¹ç ÇØ±Ý °¡°Ý
    public bool[] fieldOpens;                               // ¹ç ÇØ±Ý ¿©ºÎ

    // Start is called before the first frame update
    public void Initialize()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                fieldList[i * 5 + j] = Instantiate(fieldPrefab);
                fieldList[i * 5 + j].transform.SetParent(fields.transform, false);
                fieldList[i * 5 + j].GetComponent<Field>().Initilize(i * 5 + j);
                fieldList[i * 5 + j].GetComponent<RectTransform>().offsetMax = new Vector2(-(7 - j) * 100, - (i + 1) * 100);  // -right, -top
                fieldList[i * 5 + j].GetComponent<RectTransform>().offsetMin = new Vector2(j * 100, (4 - i) * 100);           // left, bottom
            }
        }

        for (int i = 0; i < 25; i++)
        {
            fieldList[i].GetComponent<Field>().SetOpen(dataManager.data.GetOpen()[i]);
            string[] temp = dataManager.data.GetField(i).Split("/");
            if (temp[0].Equals("True"))
            {
                int seed = int.Parse(temp[1]);
                Plant(i, seed);
                fieldList[i].GetComponent<Plant>().Setting(int.Parse(temp[2]), int.Parse(temp[3]));
            }
        }

        fieldPrice = dataManager.data.GetFieldPrice();
        fieldOpens = dataManager.data.GetOpen();
    }

    public void Plant(int location, int seed)
    {
        switch (seed)
        {
            default:
            case 0:
                fieldList[location].AddComponent<Carrot>();
                break;
            case 1:
                fieldList[location].AddComponent<Cabbage>();
                break;
            case 2:
                fieldList[location].AddComponent<Onion>();
                break;
            case 3:
                fieldList[location].AddComponent<Pumpkin>();
                break;
            case 4:
                fieldList[location].AddComponent<Orange>();
                break;
            case 5:
                fieldList[location].AddComponent<Potato>();
                break;
            case 6:
                fieldList[location].AddComponent<Apple>();
                break;
            case 7:
                fieldList[location].AddComponent<Tomato>();
                break;
            case 8:
                fieldList[location].AddComponent<Pear>();
                break;
            case 9:
                fieldList[location].AddComponent<Banana>();
                break;
            case 10:
                fieldList[location].AddComponent<Peach>();
                break;
            case 11:
                fieldList[location].AddComponent<Corn>();
                break;
            case 12:
                fieldList[location].AddComponent<Chili>();
                break;
            case 13:
                fieldList[location].AddComponent<Mango>();
                break;
            case 14:
                fieldList[location].AddComponent<Eggplant>();
                break;
            case 15:
                fieldList[location].AddComponent<Grape>();
                break;
        }
        fieldList[location].GetComponent<Field>().Planting(fieldList[location].GetComponent<Plant>());
    }

    public int GetFieldPrice()
    {
        return fieldPrice;
    }

    public void BuyField()
    {
        fieldPrice += 10;
        dataManager.data.SetFieldPrice(fieldPrice);
    }

    public void FieldOpen(int i)
    {
        fieldOpens[i] = true;
    }

    public bool[] GetFieldOpens()
    {
        return fieldOpens;
    }
}
