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

    // 제조중인 포션의 상태
    public string potionName;                      // 포션 이름
    public float potionPrice;                      // 포션 가격
    public int potionTaste;                        // 종합적인 맛. 소수로 나누어 결과를 확인
    public int potionElemental;                    // 종합적인 속성. 소수로 나누어 결과를 확인
    public int[] potionTastes = new int[5];        // 현재 단맛, 짠맛, 신맛, 쓴맛, 매운맛 성분
    public int[] potionElementals = new int[4];    // 현재 용기, 지혜, 자유, 절제 성분

    // 포션 제조에 사용되는 외부 변수
    public int level;

    // 포션 제조에 사용되는 내부 변수
    int tHigh, eHigh, tCount;
    int[] tmpTaste, tmpElementals, primes = new int[4] { 2, 3, 5, 7 };
    float tmpPrice, tAvg, tAvg2, tasteModifier, eleMentalModifier;

    /// <summary>
    /// 재료가 항아리에 투입되면 발생하는 포션 제조 함수
    /// </summary>
    /// <param name="other">투입된 재료</param>
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
    /// 제조된 포션을 완성하는 함수
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

            // 항아리를 초기화하는 단계
            potionNameText.text = "합성 항아리";
            potionName = "";
            potionPrice = 0f;
            potionTaste = 1;
            potionElemental = 1;
            potionTastes = new int[5] {0, 0, 0, 0, 0};
            potionElementals = new int[4] { 0, 0, 0, 0 };
        }
    }

    /// <summary>
    /// 포션 제조 함수
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
    /// 포션 성분 조합 함수
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
    /// 항아리 속의 포션 성분을 조합하는 함수
    /// </summary>
    void PotionAnalyze()
    {
        // 가장 높은 맛, 속성 성분을 확인하는 단계
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

        // 가장 높은 맛 성분이 여러 개인지, 지나치게 맛이 높은지, 가장 높은 속성 성분이 무엇인지 확인하는 단계
        tCount = 0;                 // 가장 높은 맛 성분의 개수
        tAvg = 0f;                  // 모든 맛 성분의 평균
        tAvg2 = 0f;                 // 가장 높은 맛을 제외한 성분의 평균
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
    /// 현재 성분으로 포션의 이름을 짓는 함수
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
                        potionName += "아주 ";
                    }
                    else if (tAvg - tAvg2 < 2)
                    {
                        potionName += "약간 ";
                    }
                    switch (i)
                    {
                        case 0:
                            potionName += "달콤한 ";
                            break;
                        case 1:
                            potionName += "짭짤한 ";
                            break;
                        case 2:
                            potionName += "시큼한 ";
                            break;
                        case 3:
                            potionName += "씁쓸한 ";
                            break;
                        case 4:
                            potionName += "매콤한 ";
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
                        potionName += "아주 ";
                    }
                    else if (tAvg - tAvg2 < 2)
                    {
                        potionName += "약간 ";
                    }
                    if (tCount == tmpCount)
                    {
                        switch (i)
                        {
                            case 0:
                                potionName += "달콤한 ";
                                break;
                            case 1:
                                potionName += "짭짤한 ";
                                break;
                            case 2:
                                potionName += "시큼한 ";
                                break;
                            case 3:
                                potionName += "씁쓸한 ";
                                break;
                            case 4:
                                potionName += "매콤한 ";
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                potionName += "달고 ";
                                break;
                            case 1:
                                potionName += "짜고 ";
                                break;
                            case 2:
                                potionName += "시고 ";
                                break;
                            case 3:
                                potionName += "쓰고 ";
                                break;
                            case 4:
                                potionName += "맵고 ";
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            potionName += "오묘한 ";
        }

        if (potionElemental % 2 == 0)
        {
            if (potionElemental % 5 == 0)
            {
                potionName += "희망의 물약";
            }
            else if (potionElemental % 7 == 0)
            {
                potionName += "정의의 물약";
            }
            else
            {
                potionName += "용기의 물약";
            }
        }
        else if (potionElemental % 3 == 0)
        {
            if (potionElemental % 5 == 0)
            {
                potionName += "성장의 물약";
            }
            else if (potionElemental % 7 == 0)
            {
                potionName += "규율의 물약";
            }
            else
            {
                potionName += "지혜의 물약";
            }
        }
        else if (potionElemental % 5 == 0)
        {
            potionName += "자유의 물약";
        }
        else if (potionElemental % 7 == 0)
        {
            potionName += "인내의 물약";
        }
        else
        {
            potionName += "순수의 물약";
        }

        potionNameText.text = potionName;
    }

    /// <summary>
    /// 포션의 가격을 결정하는 함수
    /// </summary>
    void PotionPricing()
    {
        // 가장 높은 속성 값 / 10 만큼 가격 추가
        potionPrice += eHigh / 10f;

        // 맛 성분의 (평균 - 최대를 뺀 평균)이 클수록 맛이 편향적이라고 판단하여 감점, 낮을수록 가점하여 1.2 ~ 0.8배
        tasteModifier = 10f - (tAvg - tAvg2);
        tasteModifier *= 0.02f;
        tasteModifier += 1f;
        potionPrice *= tasteModifier;

        // 단일 속성은 1.0배, 이중 속성은 1.1배, 무속성은 1.2배
        eleMentalModifier = 1f;
        if(potionElemental == 1)
        {
            eleMentalModifier = 1.2f;
        }
        else if(potionElemental > 7)
        {
            // 두 속성 간의 차이가 적을 수록 1.1 ~ 0.9배
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

        // 마지막으로 연금술사 레벨에 따라 1 ~ 49를 추가
        potionPrice += (level + 1) * (level + 1);
    }
}
