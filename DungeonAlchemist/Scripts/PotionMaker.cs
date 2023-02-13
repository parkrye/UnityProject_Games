using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionMaker : MonoBehaviour
{
    public MarketManager marketManager;
    public Transform potionTransform;
    public GameObject potionPrefab;
    public TextMesh potionNameText;
    public AudioSource bubleAudio;
    public AudioSource completeAudio;

    // �������� ������ ����
    public string potionName;                      // ���� �̸�
    public float potionPrice;                      // ���� ����
    public int potionTaste;                        // �������� ��. �Ҽ��� ������ ����� Ȯ��
    public int potionElemental;                    // �������� �Ӽ�. �Ҽ��� ������ ����� Ȯ��
    public int[] potionTastes = new int[5];        // ���� �ܸ�, §��, �Ÿ�, ����, �ſ�� ����
    public int[] potionElementals = new int[4];    // ���� ���, ����, ����, ���� ����

    // ���� ������ ���Ǵ� �ܺ� ����
    public int level;

    // ���� ������ ���Ǵ� ���� ����
    int tHigh, eHigh, tCount;
    int[] tmpTaste, tmpElementals, primes = new int[4] { 2, 3, 5, 7 };
    float tmpPrice, tAvg, tAvg2, tasteModifier, eleMentalModifier;

    /// <summary>
    /// ��ᰡ �׾Ƹ��� ���ԵǸ� �߻��ϴ� ���� ���� �Լ�
    /// </summary>
    /// <param name="other">���Ե� ���</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Takable")
        {
            ProductManager tmpProduct = other.gameObject.GetComponent<ProductManager>();
            if (!tmpProduct.GetTakeOn())
            {
                bubleAudio.Play();
                tmpPrice = tmpProduct.GetPrice();
                tmpTaste = tmpProduct.GetTaste();
                tmpElementals = tmpProduct.GetElementals();
                PotionMake();
                Destroy(other.gameObject);
            }
        }
    }

    /// <summary>
    /// ������ ������ �ϼ��ϴ� �Լ�
    /// </summary>
    public void PotionComplete()
    {
        if(potionPrice > 0)
        {
            completeAudio.Play();
            level = marketManager.GetLevel();
            PotionMake(true);
            GameObject potion = Instantiate(potionPrefab, potionTransform.position, potionTransform.rotation);
            potion.GetComponent<ProductManager>().SetValues(potionName, potionPrice, potionElemental);

            // �׾Ƹ��� �ʱ�ȭ�ϴ� �ܰ�
            potionNameText.text = "�ռ� �׾Ƹ�";
            potionName = "";
            potionPrice = 0f;
            potionTaste = 1;
            potionElemental = 1;
            potionTastes = new int[5] {0, 0, 0, 0, 0};
            potionElementals = new int[4] { 0, 0, 0, 0 };
        }
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    void PotionMake(bool pricing = false)
    {
        if (!pricing)
        {
            PotionMixing();
            PotionAnalyze();
            PotionNaming();
        }
        else
        {
            PotionPricing();
        }
    }

    /// <summary>
    /// ���� ���� ���� �Լ�
    /// </summary>
    void PotionMixing()
    {
        potionPrice += tmpPrice * 0.8f;
        if (potionPrice > 200)
        {
            potionPrice = 200;
        }

        for (int i = 0; i < 5; i++)
        {
            potionTastes[i] += tmpTaste[i];
            if (potionTastes[i] > 100)
            {
                potionTastes[i] = 100;
            }
            if(i < 4)
            {
                potionElementals[i] += tmpElementals[i];
                if (potionElementals[i] > 100)
                {
                    potionElementals[i] = 100;
                }
            }
        }

        if (potionElementals[0] > 0 && potionElementals[1] > 0)
        {
            while (potionElementals[0] > 0 && potionElementals[1] > 0)
            {
                potionElementals[0]--;
                potionElementals[1]--;
            }
        }

        if (potionElementals[2] > 0 && potionElementals[3] > 0)
        {
            while (potionElementals[2] > 0 && potionElementals[3] > 0)
            {
                potionElementals[2]--;
                potionElementals[3]--;
            }
        }
    }

    /// <summary>
    /// �׾Ƹ� ���� ���� ������ �����ϴ� �Լ�
    /// </summary>
    void PotionAnalyze()
    {
        // ���� ���� ��, �Ӽ� ������ Ȯ���ϴ� �ܰ�
        tHigh = 1;
        eHigh = 1;

        for(int i = 0; i < 5; i++)
        {
            if (potionTastes[i] > tHigh)
            {
                tHigh = potionTastes[i];
            }

            if(i < 4)
            {
                if (potionElementals[i] > eHigh)
                {
                    eHigh = potionElementals[i];
                }
            }
        }

        // ���� ���� �� ������ ���� ������, ����ġ�� ���� ������, ���� ���� �Ӽ� ������ �������� Ȯ���ϴ� �ܰ�
        tCount = 0;                 // ���� ���� �� ������ ����
        tAvg = 0f;                  // ��� �� ������ ���
        tAvg2 = 0f;                 // ���� ���� ���� ������ ������ ���
        potionTaste = 1;
        potionElemental = 1;

        for (int i = 0; i < 5; i++)
        {
            if (potionTastes[i] == tHigh)
            {
                tCount++;
                tAvg += potionTastes[i];
                switch (i)
                {
                    case 0:
                        potionTaste *= 2;
                        break;
                    case 1:
                        potionTaste *= 3;
                        break;
                    case 2:
                        potionTaste *= 5;
                        break;
                    case 3:
                        potionTaste *= 7;
                        break;
                    case 4:
                        potionTaste *= 11;
                        break;
                }
            }
            else
            {
                tAvg += potionTastes[i];
                tAvg2 += potionTastes[i];
            }

            if (i < 4)
            {
                if (potionElementals[i] == eHigh)
                {
                    switch (i)
                    {
                        case 0:
                            potionElemental *= 2;
                            break;
                        case 1:
                            potionElemental *= 3;
                            break;
                        case 2:
                            potionElemental *= 5;
                            break;
                        case 3:
                            potionElemental *= 7;
                            break;
                    }
                }
            }
        }
        if(tCount != 5)
        {
            tAvg /= 5;
            tAvg2 /= (5 - tCount);
        }
        else
        {
            tAvg = 0;
            tAvg2 = 0;
        }
    }

    /// <summary>
    /// ���� �������� ������ �̸��� ���� �Լ�
    /// </summary>
    void PotionNaming()
    {
        potionName = "";
        if (tCount == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (potionTastes[i] == tHigh)
                {
                    if (tAvg - tAvg2 > 10)
                    {
                        potionName += "���� ";
                    }
                    else if (tAvg - tAvg2 < 2)
                    {
                        potionName += "�ణ ";
                    }
                    switch (i)
                    {
                        case 0:
                            potionName += "������ ";
                            break;
                        case 1:
                            potionName += "¬©�� ";
                            break;
                        case 2:
                            potionName += "��ŭ�� ";
                            break;
                        case 3:
                            potionName += "������ ";
                            break;
                        case 4:
                            potionName += "������ ";
                            break;
                    }
                    break;
                }
            }
        }
        else if (tCount < 5)
        {
            int tmpCount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (potionTastes[i] == tHigh)
                {
                    tmpCount++;
                    if (tAvg - tAvg2 > 10)
                    {
                        potionName += "���� ";
                    }
                    else if (tAvg - tAvg2 < 2)
                    {
                        potionName += "�ణ ";
                    }
                    if (tCount == tmpCount)
                    {
                        switch (i)
                        {
                            case 0:
                                potionName += "������ ";
                                break;
                            case 1:
                                potionName += "¬©�� ";
                                break;
                            case 2:
                                potionName += "��ŭ�� ";
                                break;
                            case 3:
                                potionName += "������ ";
                                break;
                            case 4:
                                potionName += "������ ";
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                potionName += "�ް� ";
                                break;
                            case 1:
                                potionName += "¥�� ";
                                break;
                            case 2:
                                potionName += "�ð� ";
                                break;
                            case 3:
                                potionName += "���� ";
                                break;
                            case 4:
                                potionName += "�ʰ� ";
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            potionName += "������ ";
        }

        if (potionElemental % 2 == 0)
        {
            if (potionElemental % 5 == 0)
            {
                potionName += "����� ����";
            }
            else if (potionElemental % 7 == 0)
            {
                potionName += "������ ����";
            }
            else
            {
                potionName += "����� ����";
            }
        }
        else if (potionElemental % 3 == 0)
        {
            if (potionElemental % 5 == 0)
            {
                potionName += "������ ����";
            }
            else if (potionElemental % 7 == 0)
            {
                potionName += "������ ����";
            }
            else
            {
                potionName += "������ ����";
            }
        }
        else if (potionElemental % 5 == 0)
        {
            potionName += "������ ����";
        }
        else if (potionElemental % 7 == 0)
        {
            potionName += "�γ��� ����";
        }
        else
        {
            potionName += "������ ����";
        }

        potionNameText.text = potionName;
    }

    /// <summary>
    /// ������ ������ �����ϴ� �Լ�
    /// </summary>
    void PotionPricing()
    {
        // ���� ���� �Ӽ� �� / 10 ��ŭ ���� �߰�
        potionPrice += eHigh / 10f;

        // �� ������ (��� - �ִ븦 �� ���)�� Ŭ���� ���� �������̶�� �Ǵ��Ͽ� ����, �������� �����Ͽ� 1.2 ~ 0.8��
        tasteModifier = 10f - (tAvg - tAvg2);
        tasteModifier *= 0.02f;
        tasteModifier += 1f;
        potionPrice *= tasteModifier;

        // ���� �Ӽ��� 1.0��, ���� �Ӽ��� 1.1��, ���Ӽ��� 1.2��
        eleMentalModifier = 1f;
        if(potionElemental == 1)
        {
            eleMentalModifier = 1.2f;
        }
        else if(potionElemental > 7)
        {
            // �� �Ӽ� ���� ���̰� ���� ���� 1.1 ~ 0.9��
            int elem1 = 0, elem2 = 0;
            for (int i = 0; i < 4; i++)
            {
                if (potionElemental % primes[i] == 0)
                {
                    if(elem1 == 0)
                    {
                        elem1 = i;
                    }
                    else
                    {
                        elem2 = i;
                    }
                    eleMentalModifier = 1.1f;
                    break;
                }
            }
            int diff = Mathf.Abs(potionElementals[elem1] - potionElementals[elem2]);
            if(diff < 5)
            {
                eleMentalModifier *= 1.1f;
            }
            else if(diff > 15)
            {
                eleMentalModifier *= 0.9f;
            }
        }
        potionPrice *= eleMentalModifier;

        // ���������� ���ݼ��� ������ ���� 1 ~ 49�� �߰�
        potionPrice += (level + 1) * (level + 1);
    }
}
