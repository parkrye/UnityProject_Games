using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionReview : MonoBehaviour
{
    public MarketManager marketManager;
    public Queue<ProductManager> reviews;
    public GameObject ui;
    public Text text;

    int[] primes = new int[4] { 2, 3, 5, 7 };
    int[] score = new int[2];                   // 포션에 대한 점수들
    int scoreIndex;

    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
        reviews = new Queue<ProductManager>();
        StartCoroutine(ShowReview());
    }

    public void EnQueue(ProductManager product)
    {
        reviews.Enqueue(product);
    }

    IEnumerator ShowReview()
    {
        while (true)
        {
            if(reviews.Count > 0)
            {
                ui.SetActive(true);
                marketManager.ModifyReputation(score[scoreIndex]);
                text.text = MakeReview(reviews.Dequeue());
                if (score[scoreIndex] < 0)
                {
                    text.color = Color.red;
                }
                else if (score[scoreIndex] == 0)
                {
                    text.color = Color.white;
                }
                else
                {
                    text.color = Color.green;
                }
            }
            else
            {
                text.text = "";
                text.color = Color.white;
                ui.SetActive(false);
            }
            yield return new WaitForSeconds(10f);
        }
    }

    string MakeReview(ProductManager product)
    {
        string review = product.GetName();

        string[] list = new string[2];  // 포션에 대한 평가들

        // 가장 높은 맛, 속성 성분을 확인하는 단계
        int tHigh = 1;
        int eHigh = 1;

        for (int i = 0; i < 5; i++)
        {
            if (product.GetTaste()[i] > tHigh)
            {
                tHigh = product.GetTaste()[i];
            }

            if (i < 4)
            {
                if (product.GetElementals()[i] > eHigh)
                {
                    eHigh = product.GetElementals()[i];
                }
            }
        }

        // 가장 높은 맛 성분이 여러 개인지, 지나치게 맛이 높은지, 가장 높은 속성 성분이 무엇인지 확인하는 단계
        int tCount = 0;                 // 가장 높은 맛 성분의 개수
        int tSum = 0;                   // 모든 맛 성분의 합
        int tSum2 = 0;                  // 가장 높은 맛을 제외한 성분의 합
        int potionTaste = 1;            // 가장 높은 맛의 종류
        int potionElemental = 1;        // 가장 높은 속성의 종류
        int random;

        for (int i = 0; i < 5; i++)
        {
            if (product.GetTaste()[i] == tHigh)
            {
                tCount++;
                tSum += product.GetTaste()[i];
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
                tSum += product.GetTaste()[i];
                tSum2 += product.GetTaste()[i];
            }

            if (i < 4)
            {
                if (product.GetElementals()[i] == eHigh)
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

        if (tCount == 5)
        {
            random = Random.Range(0, 10);
            switch (random)
            {
                default:
                case 0:
                    list[0] = "의 맛에는 완벽이라는 표현밖에 쓸 수가 없어!";
                    break;
                case 1:
                    list[0] = "의 다섯가지 맛이 절묘한 균형을 이루고 있어!";
                    break;
                case 2:
                    list[0] = "을 마시기 전으로는 돌아갈 수 없게 되버렸어!";
                    break;
                case 3:
                    list[0] = "은 대체 어떻게 이런 훌륭한 맛을 내는 거지?";
                    break;
                case 4:
                    list[0] = "은 정말 환상적인 물약이야! 중독될 것만 같아...";
                    break;
                case 5:
                    list[0] = "을 만든 연금술사는 정말이지 천재가 분명해!";
                    break;
                case 6:
                    list[0] = "을 마시는 꿈을 꿨어. 내일 또 사러 가야겠어!";
                    break;
                case 7:
                    list[0] = "과 나는 천 년의 사랑에 빠진 것 같아!";
                    break;
                case 8:
                    list[0] = "의 존재는 세상 모두가 알아야해!";
                    break;
                case 9:
                    list[0] = " 만세!";
                    break;
            }
            score[0] = 5;
        }
        else
        {
            if ((tSum - tSum2) <= 5)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] = "의 맛은 정말 조화로워. 혀가 편안한 맛이야";
                        break;
                    case 1:
                        list[0] = "의 절묘한 맛을 나는 정말 좋아해";
                        break;
                    case 2:
                        list[0] = "은 음료수 대신 마셔도 될 것 같아";
                        break;
                    case 3:
                        list[0] = "은 어떻게 이런 맛을 내는 거지?";
                        break;
                    case 4:
                        list[0] = " 정말 맛있었어. 다음에도 또 사러 와야지";
                        break;
                    case 5:
                        list[0] = "을 만든 사람은 분명 훌륭한 연금술사야";
                        break;
                    case 6:
                        list[0] = "이라면 꿈에서라도 마시고 싶어";
                        break;
                    case 7:
                        list[0] = "...부족한데. 한 병 거 살걸 그랬나?";
                        break;
                    case 8:
                        list[0] = "이 맛있다는 건 모두가 알고 있겠지";
                        break;
                    case 9:
                        list[0] = "은 솔직히 너무 맛있어서 물약같지가 않아";
                        break;
                }
                score[0] = 5;
            }
            else if((tSum - tSum2) <= 10)
            {
                list[0] = "은 ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "달콤한 맛";
                                break;
                            case 1:
                                list[0] += "짭짤한 맛";
                                break;
                            case 2:
                                list[0] += "시큼한 맛";
                                break;
                            case 3:
                                list[0] += "씁쓸한 맛";
                                break;
                            case 4:
                                list[0] += "매콤한 맛";
                                break;
                        }
                        break;
                    }
                }
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] += "이 정말 인상적인데! 마음에 들었어!";
                        break;
                    case 1:
                        list[0] = "이 딱 내가 좋아하는 정도야";
                        break;
                    case 2:
                        list[0] = "이 강하다는 게 가장 큰 매력이지";
                        break;
                    case 3:
                        list[0] = "이 핵심이야. 생각하니 또 마시고 싶다";
                        break;
                    case 4:
                        list[0] = "이라고 모두가 좋아해. 물론 나도";
                        break;
                    case 5:
                        list[0] = " 때문에 계속 사게 되는 것 같아";
                        break;
                    case 6:
                        list[0] = "을 좋아하는 사람에게는 필수품이야";
                        break;
                    case 7:
                        list[0] = "이 입에 오랫동안 남아서 좋아";
                        break;
                    case 8:
                        list[0] = "으로 만들겠다고 어떻게 고안한 걸까?";
                        break;
                    case 9:
                        list[0] = "이라고 헤서 샀는데, 역시 좋은 선택이었던 것 같아";
                        break;
                }
                score[0] = 3;
            }
            else if ((tSum - tSum2) <= 15)
            {
                list[0] = "은 ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "달콤한 맛";
                                break;
                            case 1:
                                list[0] += "짭짤한 맛";
                                break;
                            case 2:
                                list[0] += "시큼한 맛";
                                break;
                            case 3:
                                list[0] += "씁쓸한 맛";
                                break;
                            case 4:
                                list[0] += "매콤한 맛";
                                break;
                        }
                        break;
                    }
                }
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] += "이 조금 신경쓰여. 그래도 맛있긴 해";
                        break;
                    case 1:
                        list[0] = "이라서 나는 좋아하는데 동료들은 별로인가봐";
                        break;
                    case 2:
                        list[0] = "이라고 해서 사봤는데 나쁘지 않았어";
                        break;
                    case 3:
                        list[0] = "을 좋아하는 사람이라면 한두 번은 사볼만해";
                        break;
                    case 4:
                        list[0] = "을 조금 줄이면 더 많이 사가지 않을까?";
                        break;
                    case 5:
                        list[0] = "이 진한데. 불쾌할 정도는 아니지만...";
                        break;
                    case 6:
                        list[0] = " 때문에 조금 아쉬워. 의도한 건 아니겠지?";
                        break;
                    case 7:
                        list[0] = "이 입에 오랫동안 남아서 뒷맛이 미묘해";
                        break;
                    case 8:
                        list[0] = "을 왜 이렇게 넣었을까? 나쁜 시도는 아니지만...";
                        break;
                    case 9:
                        list[0] = " 조절을 실패했나봐. 연금술사는 연습을 더 해야겠어";
                        break;
                }
                score[0] = 1;
            }
            else if ((tSum - tSum2) <= 20)
            {
                list[0] = "은 ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "달콤한 맛";
                                break;
                            case 1:
                                list[0] += "짭짤한 맛";
                                break;
                            case 2:
                                list[0] += "시큼한 맛";
                                break;
                            case 3:
                                list[0] += "씁쓸한 맛";
                                break;
                            case 4:
                                list[0] += "매콤한 맛";
                                break;
                        }
                        break;
                    }
                }
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] += "이 지나치게 강렬해서 다른 생각이 안들어";
                        break;
                    case 1:
                        list[0] = "을 무슨 생각으로 이렇게 많이 넣은 거야?";
                        break;
                    case 2:
                        list[0] = " 중독자나 좋아할 것 같은 맛이야";
                        break;
                    case 3:
                        list[0] = "만 보면 10배는 희석해서 마시는 걸 추천해";
                        break;
                    case 4:
                        list[0] = " 때문에 갈증이 생겨서 물값이 두 배는 더 나왔어";
                        break;
                    case 5:
                        list[0] = " 조절을 실패한 게 분명해. 습작을 팔지 말라고";
                        break;
                    case 6:
                        list[0] = "을 좋아하는 내게도 너무 강해서 아쉬워";
                        break;
                    case 7:
                        list[0] = "이 입에 오랫동안 남아서 아주 불쾌해";
                        break;
                    case 8:
                        list[0] = " 때문에 두 번 다시 사고 싶지 않아";
                        break;
                    case 9:
                        list[0] = "을 얼마나 넣어야 손님이 토할지 고민한 결과야?";
                        break;
                }
                score[0] = 0;
            }
            else if ((tSum - tSum2) <= 25)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] += "은 정말 맛없어. 그냥. 정말. 맛없어";
                        break;
                    case 1:
                        list[0] += "을 마시고 나면 혀를 닦고 싶어져";
                        break;
                    case 2:
                        list[0] += "을 좋아하는 사람이 세상에 존재하려나?";
                        break;
                    case 3:
                        list[0] += "에서는 이상한 냄새도 나고 맛도 별로야";
                        break;
                    case 4:
                        list[0] += "에 대체 뭘 쳐넣은 거야? 마실 수 있는 건 맞지?";
                        break;
                    case 5:
                        list[0] += "의 장점. 마시고 나서는 뭘 먹어도 맛있음";
                        break;
                    case 6:
                        list[0] += "은 지금까지의 인생 최악의 물약이야. 아마 앞으로도";
                        break;
                    case 7:
                        list[0] += "! 그건 좋은 고문 도구지";
                        break;
                    case 8:
                        list[0] += "을 동료에게 먹이고 사이가 나빠졌어. 책임져";
                        break;
                    case 9:
                        list[0] += "을 만들면서 맛보기는 안했나봐. 그냥 생각이 없었거나";
                        break;
                }
                score[0] = -1;
            }
            else
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[0] += "을 만든 연금술사는 눈도 코도 혀도 없나? 그렇지 않고서야...";
                        break;
                    case 1:
                        list[0] += "은 일주일은 묵힌 구정물에 푹 삶은 걸레 맛이야!";
                        break;
                    case 2:
                        list[0] += "은 시궁창 바닥에 깔린 이끼보다 삼키기 힘들어!";
                        break;
                    case 3:
                        list[0] += "을 마실 바에야 죽는 게 나아!";
                        break;
                    case 4:
                        list[0] += "을 던졌더니 몬스터가 악취 때문에 도망갔어!";
                        break;
                    case 5:
                        list[0] += "을 내 동료가 공공장소에서 열었다가 체포됐어!";
                        break;
                    case 6:
                        list[0] += "? 그게 물약이었다고? 나는 투척용 무기인줄 알았는데...";
                        break;
                    case 7:
                        list[0] += "을 마셨더니 며칠째 후각과 미각이 돌아오지 않아!";
                        break;
                    case 8:
                        list[0] += " 때문에 이상한 냄새가 난다고 애인에게 차였어!";
                        break;
                    case 9:
                        list[0] += "을 사기 전으로 돌아가고 싶어...";
                        break;
                }
                score[0] = -2;
            }
        }

        // 단일 속성은 1.0배, 이중 속성은 1.1배, 무속성은 1.2배
        if (potionElemental == 1)
        {
            if (eHigh >= 45)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "은 마시자마자 바로 효과가 나와서 좋아";
                        break;
                    case 1:
                        list[1] += " 덕분에 이번 모험에서 목숨을 건졌어!";
                        break;
                    case 2:
                        list[1] += "은 모험가의 필수품이지!";
                        break;
                    case 3:
                        list[1] += "은 몇 개를 챙겨도 늘 부족한 것 같아!";
                        break;
                    case 4:
                        list[1] += "의 존재를 모두에게 알리고 싶어!";
                        break;
                    case 5:
                        list[1] += "의 효과는 정말 마법같아!";
                        break;
                    case 6:
                        list[1] += "이라면 가격이 아무리 비싸더라도 사고 싶어!";
                        break;
                    case 7:
                        list[1] += "의 환상적인 효과를 보고 나면 물약을 보는 눈이 높아져!";
                        break;
                    case 8:
                        list[1] += "과 함께라면 아무리 험난한 모험도 든든하지!";
                        break;
                    case 9:
                        list[1] += "의 재고가 내일은 좀 넉넉했으면...";
                        break;
                }
                score[1] = 5;
            }
            else if (eHigh >= 35)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "의 효과는 다른 물약보다 탁월한 것 같아";
                        break;
                    case 1:
                        list[1] += " 덕분에 이번에도 위기를 넘겼어...";
                        break;
                    case 2:
                        list[1] += "이 없었다면 아마도 위험에 처했을 거야";
                        break;
                    case 3:
                        list[1] += "은 늘 배낭에 하나쯤은 구비해둬야지";
                        break;
                    case 4:
                        list[1] += "을 모른다면 진정한 모험가라 할 수 없지";
                        break;
                    case 5:
                        list[1] += "과 같은 가격에서 이보다 나은 선택은 없어";
                        break;
                    case 6:
                        list[1] += "을 추천받아서 처음 써봤는데 정말 좋았어";
                        break;
                    case 7:
                        list[1] += "은 다음에 들릴 일이 있으면 꼭 다시 사야겠어";
                        break;
                    case 8:
                        list[1] += "은 정확히 내가 원하던 대로의 성능을 보여줘";
                        break;
                    case 9:
                        list[1] += "을 만든 연금술사라면 확실히 믿을 수 있지";
                        break;
                }
                score[1] = 3;
            }
            else if (eHigh >= 25)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += " 정도면 사실 꽤 괜찮은 물약이지";
                        break;
                    case 1:
                        list[1] += "은 성능이 두드러지지는 않지만 준수해";
                        break;
                    case 2:
                        list[1] += "은 당장 여유가 된다면 배낭에 챙겨둘만해";
                        break;
                    case 3:
                        list[1] += "은 조금 할인을 하는 편이 좋을 것 같은데";
                        break;
                    case 4:
                        list[1] += "라면 초보 모험가들에게는 추천할만 해";
                        break;
                    case 5:
                        list[1] += "을 사용하다가 수입이 늘면 바꿔야겠다";
                        break;
                    case 6:
                        list[1] += "을 하나쯤 사둬서 나쁠 건 없지";
                        break;
                    case 7:
                        list[1] += "은 가격이라던가 고려해보면 합리적인 선택이야";
                        break;
                    case 8:
                        list[1] += "보다 좋은 물약은 있겠지만 이정도라면 뭐...";
                        break;
                    case 9:
                        list[1] += "은 적당히 내가 원하는 성능을 보여줘";
                        break;
                }
                score[1] = 1;
            }
            else if (eHigh >= 15)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "을 굳이 돈 주고 사야 할 이유가 있나?";
                        break;
                    case 1:
                        list[1] += "은 효과가 거의 없는 것 같은데";
                        break;
                    case 2:
                        list[1] += "은 정 당장 물약이 시급하다면 하나쯤 사볼 정도야";
                        break;
                    case 3:
                        list[1] += "은 그냥 다른 물약에 덤으로 줘도 되지 않나?";
                        break;
                    case 4:
                        list[1] += "을 다른 사람에게 추천할 일은 없을 거야";
                        break;
                    case 5:
                        list[1] += "을 두 번 다시 사고 싶지 않아";
                        break;
                    case 6:
                        list[1] += "을 누군가 사겠다면 적극적으로 만류할 거야";
                        break;
                    case 7:
                        list[1] += "보다 나은 선택이 있지 않을까?";
                        break;
                    case 8:
                        list[1] += "을 사용했는데도 전혀 효과가 나타나지 않았어";
                        break;
                    case 9:
                        list[1] += "에 굳이 악평을 남기고 싶지 않아. 어차피 누가 할테니까";
                        break;
                }
                score[1] = 0;
            }
            else if (eHigh >= 5)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "을 만든 연금술사는 잘못하지 않았어. 그냥 못한거지";
                        break;
                    case 1:
                        list[1] += "은 배가 터지도록 마셔도 별다른 효과가 드러나지 않을 거야";
                        break;
                    case 2:
                        list[1] += "을 사는 건 투자의 개념이야. 대상을 잘못 고른 것 같지만";
                        break;
                    case 3:
                        list[1] += "은 솔직히 제정신으로 살만한 물건이 아니야";
                        break;
                    case 4:
                        list[1] += "을 추천하는 사람은 분명 사기꾼이야. 파는 사람은 강도고";
                        break;
                    case 5:
                        list[1] += "같은 건 그냥 재미용으로 사보는 거지. 탱탱볼처럼";
                        break;
                    case 6:
                        list[1] += "을 두 번 사는 멍청이는 모험가를 그만두는 게 좋아";
                        break;
                    case 7:
                        list[1] += "이라면 한 번 사보는 정도는 괜찮지. 낭비도 교육은 되거든";
                        break;
                    case 8:
                        list[1] += "? 아 그게 그냥 물이 아니라 약도 들어 있는 거였어?";
                        break;
                    case 9:
                        list[1] += "은 물 대신 마셔도 돼. 다른 성분이 있을 리가 없으니까";
                        break;
                }
                score[1] = -1;
            }
            else
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "은 물약이라는 이름으로 파는 것부터 그냥 사기야";
                        break;
                    case 1:
                        list[1] += "에 긴 평가는 필요 없지. 그냥 엉망진창이거든";
                        break;
                    case 2:
                        list[1] += "을 살 바에야 그 돈을 태워서 모닥불을 지피는 게 나아";
                        break;
                    case 3:
                        list[1] += "에 값을 매겨 파는 연금술사는 사람으로 신뢰할 수 없어";
                        break;
                    case 4:
                        list[1] += "을 마시는 것보다 신선한 사과를 먹는 게 더 몸에 좋아";
                        break;
                    case 5:
                        list[1] += " 따위에 쓴 돈 때문에 며칠째 잠을 설치고 있어";
                        break;
                    case 6:
                        list[1] += "같은 쓰레기를 팔면서 어떻게 장사를 하고 있는 거야?";
                        break;
                    case 7:
                        list[1] += "은 법으로 금지해야 해. 파는 것도, 만드는 것도, 사는 것도";
                        break;
                    case 8:
                        list[1] += "은 그 자체로 우리에게 큰 질문을 던져. 자연 보호라는 질문을";
                        break;
                    case 9:
                        list[1] += "을 살 바야에 깨끗한 물이나 하나 더 챙기는 게 나아";
                        break;
                }
                score[1] = -2;
            }
        }
        else if (potionElemental > 7)
        {
            // 두 속성 간의 차이가 적을 수록 1.1 ~ 0.9배
            int elem1 = 0, elem2 = 0;
            for (int i = 0; i < 4; i++)
            {
                if (potionElemental % primes[i] == 0)
                {
                    if (elem1 == 0)
                    {
                        elem1 = i;
                    }
                    else
                    {
                        elem2 = i;
                    }
                    break;
                }
            }
            int diff = Mathf.Abs(product.GetElementals()[elem1] - product.GetElementals()[elem2]);

            if (diff < 5)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "은 마시자마자 바로 효과가 나와서 좋아";
                        break;
                    case 1:
                        list[1] += " 덕분에 이번 모험에서 목숨을 건졌어!";
                        break;
                    case 2:
                        list[1] += "은 모험가의 필수품이지!";
                        break;
                    case 3:
                        list[1] += "은 몇 개를 챙겨도 늘 부족한 것 같아!";
                        break;
                    case 4:
                        list[1] += "의 존재를 모두에게 알리고 싶어!";
                        break;
                    case 5:
                        list[1] += "의 효과는 정말 마법같아!";
                        break;
                    case 6:
                        list[1] += "이라면 가격이 아무리 비싸더라도 사고 싶어!";
                        break;
                    case 7:
                        list[1] += "의 환상적인 효과를 보고 나면 물약을 보는 눈이 높아져!";
                        break;
                    case 8:
                        list[1] += "과 함께라면 아무리 험난한 모험도 든든하지!";
                        break;
                    case 9:
                        list[1] += "의 재고가 내일은 좀 넉넉했으면...";
                        break;
                }
                score[1] = 5;
            }
            else if (diff < 10)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "의 효과는 다른 물약보다 탁월한 것 같아";
                        break;
                    case 1:
                        list[1] += " 덕분에 이번에도 위기를 넘겼어...";
                        break;
                    case 2:
                        list[1] += "이 없었다면 아마도 위험에 처했을 거야";
                        break;
                    case 3:
                        list[1] += "은 늘 배낭에 하나쯤은 구비해둬야지";
                        break;
                    case 4:
                        list[1] += "을 모른다면 진정한 모험가라 할 수 없지";
                        break;
                    case 5:
                        list[1] += "과 같은 가격에서 이보다 나은 선택은 없어";
                        break;
                    case 6:
                        list[1] += "을 추천받아서 처음 써봤는데 정말 좋았어";
                        break;
                    case 7:
                        list[1] += "은 다음에 들릴 일이 있으면 꼭 다시 사야겠어";
                        break;
                    case 8:
                        list[1] += "은 정확히 내가 원하던 대로의 성능을 보여줘";
                        break;
                    case 9:
                        list[1] += "을 만든 연금술사라면 확실히 믿을 수 있지";
                        break;
                }
                score[1] = 3;
            }
            else if (diff < 15)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += " 정도면 사실 꽤 괜찮은 물약이지";
                        break;
                    case 1:
                        list[1] += "은 성능이 두드러지지는 않지만 준수해";
                        break;
                    case 2:
                        list[1] += "은 당장 여유가 된다면 배낭에 챙겨둘만해";
                        break;
                    case 3:
                        list[1] += "은 조금 할인을 하는 편이 좋을 것 같은데";
                        break;
                    case 4:
                        list[1] += "라면 초보 모험가들에게는 추천할만 해";
                        break;
                    case 5:
                        list[1] += "을 사용하다가 수입이 늘면 바꿔야겠다";
                        break;
                    case 6:
                        list[1] += "을 하나쯤 사둬서 나쁠 건 없지";
                        break;
                    case 7:
                        list[1] += "은 가격이라던가 고려해보면 합리적인 선택이야";
                        break;
                    case 8:
                        list[1] += "보다 좋은 물약은 있겠지만 이정도라면 뭐...";
                        break;
                    case 9:
                        list[1] += "은 적당히 내가 원하는 성능을 보여줘";
                        break;
                }
                score[1] = 1;
            }
            else if (diff < 20)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "을 굳이 돈 주고 사야 할 이유가 있나?";
                        break;
                    case 1:
                        list[1] += "은 효과가 거의 없는 것 같은데";
                        break;
                    case 2:
                        list[1] += "은 정 당장 물약이 시급하다면 하나쯤 사볼 정도야";
                        break;
                    case 3:
                        list[1] += "은 그냥 다른 물약에 덤으로 줘도 되지 않나?";
                        break;
                    case 4:
                        list[1] += "을 다른 사람에게 추천할 일은 없을 거야";
                        break;
                    case 5:
                        list[1] += "을 두 번 다시 사고 싶지 않아";
                        break;
                    case 6:
                        list[1] += "을 누군가 사겠다면 적극적으로 만류할 거야";
                        break;
                    case 7:
                        list[1] += "보다 나은 선택이 있지 않을까?";
                        break;
                    case 8:
                        list[1] += "을 사용했는데도 전혀 효과가 나타나지 않았어";
                        break;
                    case 9:
                        list[1] += "에 굳이 악평을 남기고 싶지 않아. 어차피 누가 할테니까";
                        break;
                }
                score[1] = 0;
            }
            else if (diff < 25)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "을 만든 연금술사는 잘못하지 않았어. 그냥 못한거지";
                        break;
                    case 1:
                        list[1] += "은 배가 터지도록 마셔도 별다른 효과가 드러나지 않을 거야";
                        break;
                    case 2:
                        list[1] += "을 사는 건 투자의 개념이야. 대상을 잘못 고른 것 같지만";
                        break;
                    case 3:
                        list[1] += "은 솔직히 제정신으로 살만한 물건이 아니야";
                        break;
                    case 4:
                        list[1] += "을 추천하는 사람은 분명 사기꾼이야. 파는 사람은 강도고";
                        break;
                    case 5:
                        list[1] += "같은 건 그냥 재미용으로 사보는 거지. 탱탱볼처럼";
                        break;
                    case 6:
                        list[1] += "을 두 번 사는 멍청이는 모험가를 그만두는 게 좋아";
                        break;
                    case 7:
                        list[1] += "이라면 한 번 사보는 정도는 괜찮지. 낭비도 교육은 되거든";
                        break;
                    case 8:
                        list[1] += "? 아 그게 그냥 물이 아니라 약도 들어 있는 거였어?";
                        break;
                    case 9:
                        list[1] += "은 물 대신 마셔도 돼. 다른 성분이 있을 리가 없으니까";
                        break;
                }
                score[1] = -1;
            }
            else
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "은 물약이라는 이름으로 파는 것부터 그냥 사기야";
                        break;
                    case 1:
                        list[1] += "에 긴 평가는 필요 없지. 그냥 엉망진창이거든";
                        break;
                    case 2:
                        list[1] += "을 살 바에야 그 돈을 태워서 모닥불을 지피는 게 나아";
                        break;
                    case 3:
                        list[1] += "에 값을 매겨 파는 연금술사는 사람으로 신뢰할 수 없어";
                        break;
                    case 4:
                        list[1] += "을 마시는 것보다 신선한 사과를 먹는 게 더 몸에 좋아";
                        break;
                    case 5:
                        list[1] += " 따위에 쓴 돈 때문에 며칠째 잠을 설치고 있어";
                        break;
                    case 6:
                        list[1] += "같은 쓰레기를 팔면서 어떻게 장사를 하고 있는 거야?";
                        break;
                    case 7:
                        list[1] += "은 법으로 금지해야 해. 파는 것도, 만드는 것도, 사는 것도";
                        break;
                    case 8:
                        list[1] += "은 그 자체로 우리에게 큰 질문을 던져. 자연 보호라는 질문을";
                        break;
                    case 9:
                        list[1] += "을 살 바야에 깨끗한 물이나 하나 더 챙기는 게 나아";
                        break;
                }
                score[1] = -2;
            }
        }
        else
        {
            random = Random.Range(0, 10);
            switch (random)
            {
                default:
                case 0:
                    list[1] += "은 마시자마자 바로 효과가 나와서 좋아";
                    break;
                case 1:
                    list[1] += " 덕분에 이번 모험에서 목숨을 건졌어!";
                    break;
                case 2:
                    list[1] += "은 모험가의 필수품이지!";
                    break;
                case 3:
                    list[1] += "은 몇 개를 챙겨도 늘 부족한 것 같아!";
                    break;
                case 4:
                    list[1] += "의 존재를 모두에게 알리고 싶어!";
                    break;
                case 5:
                    list[1] += "의 효과는 정말 마법같아!";
                    break;
                case 6:
                    list[1] += "이라면 가격이 아무리 비싸더라도 사고 싶어!";
                    break;
                case 7:
                    list[1] += "의 환상적인 효과를 보고 나면 물약을 보는 눈이 높아져!";
                    break;
                case 8:
                    list[1] += "과 함께라면 아무리 험난한 모험도 든든하지!";
                    break;
                case 9:
                    list[1] += "의 재고가 내일은 좀 넉넉했으면...";
                    break;
            }
            score[1] = 5;
        }

        int resultRandom = Random.Range(0, Mathf.Abs(score[0]) + 1 + Mathf.Abs(score[1] + 1));
        if(resultRandom <= score[0])
        {
            review += list[0];
            scoreIndex = 0;
        }
        else
        {
            review += list[1];
            scoreIndex = 1;
        }

        return review;
    }
}
