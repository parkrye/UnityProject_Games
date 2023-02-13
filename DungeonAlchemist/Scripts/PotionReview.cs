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
    int[] score = new int[2];                   // ���ǿ� ���� ������
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

        string[] list = new string[2];  // ���ǿ� ���� �򰡵�

        // ���� ���� ��, �Ӽ� ������ Ȯ���ϴ� �ܰ�
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

        // ���� ���� �� ������ ���� ������, ����ġ�� ���� ������, ���� ���� �Ӽ� ������ �������� Ȯ���ϴ� �ܰ�
        int tCount = 0;                 // ���� ���� �� ������ ����
        int tSum = 0;                   // ��� �� ������ ��
        int tSum2 = 0;                  // ���� ���� ���� ������ ������ ��
        int potionTaste = 1;            // ���� ���� ���� ����
        int potionElemental = 1;        // ���� ���� �Ӽ��� ����
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
                    list[0] = "�� ������ �Ϻ��̶�� ǥ���ۿ� �� ���� ����!";
                    break;
                case 1:
                    list[0] = "�� �ټ����� ���� ������ ������ �̷�� �־�!";
                    break;
                case 2:
                    list[0] = "�� ���ñ� �����δ� ���ư� �� ���� �ǹ��Ⱦ�!";
                    break;
                case 3:
                    list[0] = "�� ��ü ��� �̷� �Ǹ��� ���� ���� ����?";
                    break;
                case 4:
                    list[0] = "�� ���� ȯ������ �����̾�! �ߵ��� �͸� ����...";
                    break;
                case 5:
                    list[0] = "�� ���� ���ݼ���� �������� õ�簡 �и���!";
                    break;
                case 6:
                    list[0] = "�� ���ô� ���� ���. ���� �� �緯 ���߰ھ�!";
                    break;
                case 7:
                    list[0] = "�� ���� õ ���� ����� ���� �� ����!";
                    break;
                case 8:
                    list[0] = "�� ����� ���� ��ΰ� �˾ƾ���!";
                    break;
                case 9:
                    list[0] = " ����!";
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
                        list[0] = "�� ���� ���� ��ȭ�ο�. ���� ����� ���̾�";
                        break;
                    case 1:
                        list[0] = "�� ������ ���� ���� ���� ������";
                        break;
                    case 2:
                        list[0] = "�� ����� ��� ���ŵ� �� �� ����";
                        break;
                    case 3:
                        list[0] = "�� ��� �̷� ���� ���� ����?";
                        break;
                    case 4:
                        list[0] = " ���� ���־���. �������� �� �緯 �;���";
                        break;
                    case 5:
                        list[0] = "�� ���� ����� �и� �Ǹ��� ���ݼ����";
                        break;
                    case 6:
                        list[0] = "�̶�� �޿����� ���ð� �;�";
                        break;
                    case 7:
                        list[0] = "...�����ѵ�. �� �� �� ��� �׷���?";
                        break;
                    case 8:
                        list[0] = "�� ���ִٴ� �� ��ΰ� �˰� �ְ���";
                        break;
                    case 9:
                        list[0] = "�� ������ �ʹ� ���־ ���ర���� �ʾ�";
                        break;
                }
                score[0] = 5;
            }
            else if((tSum - tSum2) <= 10)
            {
                list[0] = "�� ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "������ ��";
                                break;
                            case 1:
                                list[0] += "¬©�� ��";
                                break;
                            case 2:
                                list[0] += "��ŭ�� ��";
                                break;
                            case 3:
                                list[0] += "������ ��";
                                break;
                            case 4:
                                list[0] += "������ ��";
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
                        list[0] += "�� ���� �λ����ε�! ������ �����!";
                        break;
                    case 1:
                        list[0] = "�� �� ���� �����ϴ� ������";
                        break;
                    case 2:
                        list[0] = "�� ���ϴٴ� �� ���� ū �ŷ�����";
                        break;
                    case 3:
                        list[0] = "�� �ٽ��̾�. �����ϴ� �� ���ð� �ʹ�";
                        break;
                    case 4:
                        list[0] = "�̶�� ��ΰ� ������. ���� ����";
                        break;
                    case 5:
                        list[0] = " ������ ��� ��� �Ǵ� �� ����";
                        break;
                    case 6:
                        list[0] = "�� �����ϴ� ������Դ� �ʼ�ǰ�̾�";
                        break;
                    case 7:
                        list[0] = "�� �Կ� �������� ���Ƽ� ����";
                        break;
                    case 8:
                        list[0] = "���� ����ڴٰ� ��� ����� �ɱ�?";
                        break;
                    case 9:
                        list[0] = "�̶�� �켭 ��µ�, ���� ���� �����̾��� �� ����";
                        break;
                }
                score[0] = 3;
            }
            else if ((tSum - tSum2) <= 15)
            {
                list[0] = "�� ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "������ ��";
                                break;
                            case 1:
                                list[0] += "¬©�� ��";
                                break;
                            case 2:
                                list[0] += "��ŭ�� ��";
                                break;
                            case 3:
                                list[0] += "������ ��";
                                break;
                            case 4:
                                list[0] += "������ ��";
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
                        list[0] += "�� ���� �Ű澲��. �׷��� ���ֱ� ��";
                        break;
                    case 1:
                        list[0] = "�̶� ���� �����ϴµ� ������� �����ΰ���";
                        break;
                    case 2:
                        list[0] = "�̶�� �ؼ� ��ôµ� ������ �ʾҾ�";
                        break;
                    case 3:
                        list[0] = "�� �����ϴ� ����̶�� �ѵ� ���� �纼����";
                        break;
                    case 4:
                        list[0] = "�� ���� ���̸� �� ���� �簡�� ������?";
                        break;
                    case 5:
                        list[0] = "�� ���ѵ�. ������ ������ �ƴ�����...";
                        break;
                    case 6:
                        list[0] = " ������ ���� �ƽ���. �ǵ��� �� �ƴϰ���?";
                        break;
                    case 7:
                        list[0] = "�� �Կ� �������� ���Ƽ� �޸��� �̹���";
                        break;
                    case 8:
                        list[0] = "�� �� �̷��� �־�����? ���� �õ��� �ƴ�����...";
                        break;
                    case 9:
                        list[0] = " ������ �����߳���. ���ݼ���� ������ �� �ؾ߰ھ�";
                        break;
                }
                score[0] = 1;
            }
            else if ((tSum - tSum2) <= 20)
            {
                list[0] = "�� ";
                for (int i = 0; i < 5; i++)
                {
                    if (product.GetTaste()[i] == tHigh)
                    {
                        switch (i)
                        {
                            case 0:
                                list[0] += "������ ��";
                                break;
                            case 1:
                                list[0] += "¬©�� ��";
                                break;
                            case 2:
                                list[0] += "��ŭ�� ��";
                                break;
                            case 3:
                                list[0] += "������ ��";
                                break;
                            case 4:
                                list[0] += "������ ��";
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
                        list[0] += "�� ����ġ�� �����ؼ� �ٸ� ������ �ȵ��";
                        break;
                    case 1:
                        list[0] = "�� ���� �������� �̷��� ���� ���� �ž�?";
                        break;
                    case 2:
                        list[0] = " �ߵ��ڳ� ������ �� ���� ���̾�";
                        break;
                    case 3:
                        list[0] = "�� ���� 10��� ���ؼ� ���ô� �� ��õ��";
                        break;
                    case 4:
                        list[0] = " ������ ������ ���ܼ� ������ �� ��� �� ���Ծ�";
                        break;
                    case 5:
                        list[0] = " ������ ������ �� �и���. ������ ���� �����";
                        break;
                    case 6:
                        list[0] = "�� �����ϴ� ���Ե� �ʹ� ���ؼ� �ƽ���";
                        break;
                    case 7:
                        list[0] = "�� �Կ� �������� ���Ƽ� ���� ������";
                        break;
                    case 8:
                        list[0] = " ������ �� �� �ٽ� ��� ���� �ʾ�";
                        break;
                    case 9:
                        list[0] = "�� �󸶳� �־�� �մ��� ������ ����� �����?";
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
                        list[0] += "�� ���� ������. �׳�. ����. ������";
                        break;
                    case 1:
                        list[0] += "�� ���ð� ���� ���� �۰� �;���";
                        break;
                    case 2:
                        list[0] += "�� �����ϴ� ����� ���� �����Ϸ���?";
                        break;
                    case 3:
                        list[0] += "������ �̻��� ������ ���� ���� ���ξ�";
                        break;
                    case 4:
                        list[0] += "�� ��ü �� �ĳ��� �ž�? ���� �� �ִ� �� ����?";
                        break;
                    case 5:
                        list[0] += "�� ����. ���ð� ������ �� �Ծ ������";
                        break;
                    case 6:
                        list[0] += "�� ���ݱ����� �λ� �־��� �����̾�. �Ƹ� �����ε�";
                        break;
                    case 7:
                        list[0] += "! �װ� ���� �� ������";
                        break;
                    case 8:
                        list[0] += "�� ���ῡ�� ���̰� ���̰� ��������. å����";
                        break;
                    case 9:
                        list[0] += "�� ����鼭 ������� ���߳���. �׳� ������ �����ų�";
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
                        list[0] += "�� ���� ���ݼ���� ���� �ڵ� ���� ����? �׷��� �ʰ���...";
                        break;
                    case 1:
                        list[0] += "�� �������� ���� �������� ǫ ���� �ɷ� ���̾�!";
                        break;
                    case 2:
                        list[0] += "�� �ñ�â �ٴڿ� �� �̳����� ��Ű�� �����!";
                        break;
                    case 3:
                        list[0] += "�� ���� �ٿ��� �״� �� ����!";
                        break;
                    case 4:
                        list[0] += "�� �������� ���Ͱ� ���� ������ ��������!";
                        break;
                    case 5:
                        list[0] += "�� �� ���ᰡ ������ҿ��� �����ٰ� ü���ƾ�!";
                        break;
                    case 6:
                        list[0] += "? �װ� �����̾��ٰ�? ���� ��ô�� �������� �˾Ҵµ�...";
                        break;
                    case 7:
                        list[0] += "�� ���̴��� ��ĥ° �İ��� �̰��� ���ƿ��� �ʾ�!";
                        break;
                    case 8:
                        list[0] += " ������ �̻��� ������ ���ٰ� ���ο��� ������!";
                        break;
                    case 9:
                        list[0] += "�� ��� ������ ���ư��� �;�...";
                        break;
                }
                score[0] = -2;
            }
        }

        // ���� �Ӽ��� 1.0��, ���� �Ӽ��� 1.1��, ���Ӽ��� 1.2��
        if (potionElemental == 1)
        {
            if (eHigh >= 45)
            {
                random = Random.Range(0, 10);
                switch (random)
                {
                    default:
                    case 0:
                        list[1] += "�� �����ڸ��� �ٷ� ȿ���� ���ͼ� ����";
                        break;
                    case 1:
                        list[1] += " ���п� �̹� ���迡�� ����� ������!";
                        break;
                    case 2:
                        list[1] += "�� ���谡�� �ʼ�ǰ����!";
                        break;
                    case 3:
                        list[1] += "�� �� ���� ì�ܵ� �� ������ �� ����!";
                        break;
                    case 4:
                        list[1] += "�� ���縦 ��ο��� �˸��� �;�!";
                        break;
                    case 5:
                        list[1] += "�� ȿ���� ���� ��������!";
                        break;
                    case 6:
                        list[1] += "�̶�� ������ �ƹ��� ��δ��� ��� �;�!";
                        break;
                    case 7:
                        list[1] += "�� ȯ������ ȿ���� ���� ���� ������ ���� ���� ������!";
                        break;
                    case 8:
                        list[1] += "�� �Բ���� �ƹ��� �賭�� ���赵 �������!";
                        break;
                    case 9:
                        list[1] += "�� ��� ������ �� �˳�������...";
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
                        list[1] += "�� ȿ���� �ٸ� ���ຸ�� Ź���� �� ����";
                        break;
                    case 1:
                        list[1] += " ���п� �̹����� ���⸦ �Ѱ��...";
                        break;
                    case 2:
                        list[1] += "�� �����ٸ� �Ƹ��� ���迡 ó���� �ž�";
                        break;
                    case 3:
                        list[1] += "�� �� �賶�� �ϳ����� �����ص־���";
                        break;
                    case 4:
                        list[1] += "�� �𸥴ٸ� ������ ���谡�� �� �� ����";
                        break;
                    case 5:
                        list[1] += "�� ���� ���ݿ��� �̺��� ���� ������ ����";
                        break;
                    case 6:
                        list[1] += "�� ��õ�޾Ƽ� ó�� ��ôµ� ���� ���Ҿ�";
                        break;
                    case 7:
                        list[1] += "�� ������ �鸱 ���� ������ �� �ٽ� ��߰ھ�";
                        break;
                    case 8:
                        list[1] += "�� ��Ȯ�� ���� ���ϴ� ����� ������ ������";
                        break;
                    case 9:
                        list[1] += "�� ���� ���ݼ����� Ȯ���� ���� �� ����";
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
                        list[1] += " ������ ��� �� ������ ��������";
                        break;
                    case 1:
                        list[1] += "�� ������ �ε巯������ ������ �ؼ���";
                        break;
                    case 2:
                        list[1] += "�� ���� ������ �ȴٸ� �賶�� ì�ܵѸ���";
                        break;
                    case 3:
                        list[1] += "�� ���� ������ �ϴ� ���� ���� �� ������";
                        break;
                    case 4:
                        list[1] += "��� �ʺ� ���谡�鿡�Դ� ��õ�Ҹ� ��";
                        break;
                    case 5:
                        list[1] += "�� ����ϴٰ� ������ �ø� �ٲ�߰ڴ�";
                        break;
                    case 6:
                        list[1] += "�� �ϳ��� ��ּ� ���� �� ����";
                        break;
                    case 7:
                        list[1] += "�� �����̶���� ����غ��� �ո����� �����̾�";
                        break;
                    case 8:
                        list[1] += "���� ���� ������ �ְ����� ��������� ��...";
                        break;
                    case 9:
                        list[1] += "�� ������ ���� ���ϴ� ������ ������";
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
                        list[1] += "�� ���� �� �ְ� ��� �� ������ �ֳ�?";
                        break;
                    case 1:
                        list[1] += "�� ȿ���� ���� ���� �� ������";
                        break;
                    case 2:
                        list[1] += "�� �� ���� ������ �ñ��ϴٸ� �ϳ��� �纼 ������";
                        break;
                    case 3:
                        list[1] += "�� �׳� �ٸ� ���࿡ ������ �൵ ���� �ʳ�?";
                        break;
                    case 4:
                        list[1] += "�� �ٸ� ������� ��õ�� ���� ���� �ž�";
                        break;
                    case 5:
                        list[1] += "�� �� �� �ٽ� ��� ���� �ʾ�";
                        break;
                    case 6:
                        list[1] += "�� ������ ��ڴٸ� ���������� ������ �ž�";
                        break;
                    case 7:
                        list[1] += "���� ���� ������ ���� ������?";
                        break;
                    case 8:
                        list[1] += "�� ����ߴµ��� ���� ȿ���� ��Ÿ���� �ʾҾ�";
                        break;
                    case 9:
                        list[1] += "�� ���� ������ ����� ���� �ʾ�. ������ ���� ���״ϱ�";
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
                        list[1] += "�� ���� ���ݼ���� �߸����� �ʾҾ�. �׳� ���Ѱ���";
                        break;
                    case 1:
                        list[1] += "�� �谡 �������� ���ŵ� ���ٸ� ȿ���� �巯���� ���� �ž�";
                        break;
                    case 2:
                        list[1] += "�� ��� �� ������ �����̾�. ����� �߸� �� �� ������";
                        break;
                    case 3:
                        list[1] += "�� ������ ���������� �츸�� ������ �ƴϾ�";
                        break;
                    case 4:
                        list[1] += "�� ��õ�ϴ� ����� �и� �����̾�. �Ĵ� ����� ������";
                        break;
                    case 5:
                        list[1] += "���� �� �׳� ��̿����� �纸�� ����. ���ʺ�ó��";
                        break;
                    case 6:
                        list[1] += "�� �� �� ��� ��û�̴� ���谡�� �׸��δ� �� ����";
                        break;
                    case 7:
                        list[1] += "�̶�� �� �� �纸�� ������ ������. ���� ������ �ǰŵ�";
                        break;
                    case 8:
                        list[1] += "? �� �װ� �׳� ���� �ƴ϶� �൵ ��� �ִ� �ſ���?";
                        break;
                    case 9:
                        list[1] += "�� �� ��� ���ŵ� ��. �ٸ� ������ ���� ���� �����ϱ�";
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
                        list[1] += "�� �����̶�� �̸����� �Ĵ� �ͺ��� �׳� ����";
                        break;
                    case 1:
                        list[1] += "�� �� �򰡴� �ʿ� ����. �׳� ������â�̰ŵ�";
                        break;
                    case 2:
                        list[1] += "�� �� �ٿ��� �� ���� �¿��� ��ں��� ���Ǵ� �� ����";
                        break;
                    case 3:
                        list[1] += "�� ���� �Ű� �Ĵ� ���ݼ���� ������� �ŷ��� �� ����";
                        break;
                    case 4:
                        list[1] += "�� ���ô� �ͺ��� �ż��� ����� �Դ� �� �� ���� ����";
                        break;
                    case 5:
                        list[1] += " ������ �� �� ������ ��ĥ° ���� ��ġ�� �־�";
                        break;
                    case 6:
                        list[1] += "���� �����⸦ �ȸ鼭 ��� ��縦 �ϰ� �ִ� �ž�?";
                        break;
                    case 7:
                        list[1] += "�� ������ �����ؾ� ��. �Ĵ� �͵�, ����� �͵�, ��� �͵�";
                        break;
                    case 8:
                        list[1] += "�� �� ��ü�� �츮���� ū ������ ����. �ڿ� ��ȣ��� ������";
                        break;
                    case 9:
                        list[1] += "�� �� �پ߿� ������ ���̳� �ϳ� �� ì��� �� ����";
                        break;
                }
                score[1] = -2;
            }
        }
        else if (potionElemental > 7)
        {
            // �� �Ӽ� ���� ���̰� ���� ���� 1.1 ~ 0.9��
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
                        list[1] += "�� �����ڸ��� �ٷ� ȿ���� ���ͼ� ����";
                        break;
                    case 1:
                        list[1] += " ���п� �̹� ���迡�� ����� ������!";
                        break;
                    case 2:
                        list[1] += "�� ���谡�� �ʼ�ǰ����!";
                        break;
                    case 3:
                        list[1] += "�� �� ���� ì�ܵ� �� ������ �� ����!";
                        break;
                    case 4:
                        list[1] += "�� ���縦 ��ο��� �˸��� �;�!";
                        break;
                    case 5:
                        list[1] += "�� ȿ���� ���� ��������!";
                        break;
                    case 6:
                        list[1] += "�̶�� ������ �ƹ��� ��δ��� ��� �;�!";
                        break;
                    case 7:
                        list[1] += "�� ȯ������ ȿ���� ���� ���� ������ ���� ���� ������!";
                        break;
                    case 8:
                        list[1] += "�� �Բ���� �ƹ��� �賭�� ���赵 �������!";
                        break;
                    case 9:
                        list[1] += "�� ��� ������ �� �˳�������...";
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
                        list[1] += "�� ȿ���� �ٸ� ���ຸ�� Ź���� �� ����";
                        break;
                    case 1:
                        list[1] += " ���п� �̹����� ���⸦ �Ѱ��...";
                        break;
                    case 2:
                        list[1] += "�� �����ٸ� �Ƹ��� ���迡 ó���� �ž�";
                        break;
                    case 3:
                        list[1] += "�� �� �賶�� �ϳ����� �����ص־���";
                        break;
                    case 4:
                        list[1] += "�� �𸥴ٸ� ������ ���谡�� �� �� ����";
                        break;
                    case 5:
                        list[1] += "�� ���� ���ݿ��� �̺��� ���� ������ ����";
                        break;
                    case 6:
                        list[1] += "�� ��õ�޾Ƽ� ó�� ��ôµ� ���� ���Ҿ�";
                        break;
                    case 7:
                        list[1] += "�� ������ �鸱 ���� ������ �� �ٽ� ��߰ھ�";
                        break;
                    case 8:
                        list[1] += "�� ��Ȯ�� ���� ���ϴ� ����� ������ ������";
                        break;
                    case 9:
                        list[1] += "�� ���� ���ݼ����� Ȯ���� ���� �� ����";
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
                        list[1] += " ������ ��� �� ������ ��������";
                        break;
                    case 1:
                        list[1] += "�� ������ �ε巯������ ������ �ؼ���";
                        break;
                    case 2:
                        list[1] += "�� ���� ������ �ȴٸ� �賶�� ì�ܵѸ���";
                        break;
                    case 3:
                        list[1] += "�� ���� ������ �ϴ� ���� ���� �� ������";
                        break;
                    case 4:
                        list[1] += "��� �ʺ� ���谡�鿡�Դ� ��õ�Ҹ� ��";
                        break;
                    case 5:
                        list[1] += "�� ����ϴٰ� ������ �ø� �ٲ�߰ڴ�";
                        break;
                    case 6:
                        list[1] += "�� �ϳ��� ��ּ� ���� �� ����";
                        break;
                    case 7:
                        list[1] += "�� �����̶���� ����غ��� �ո����� �����̾�";
                        break;
                    case 8:
                        list[1] += "���� ���� ������ �ְ����� ��������� ��...";
                        break;
                    case 9:
                        list[1] += "�� ������ ���� ���ϴ� ������ ������";
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
                        list[1] += "�� ���� �� �ְ� ��� �� ������ �ֳ�?";
                        break;
                    case 1:
                        list[1] += "�� ȿ���� ���� ���� �� ������";
                        break;
                    case 2:
                        list[1] += "�� �� ���� ������ �ñ��ϴٸ� �ϳ��� �纼 ������";
                        break;
                    case 3:
                        list[1] += "�� �׳� �ٸ� ���࿡ ������ �൵ ���� �ʳ�?";
                        break;
                    case 4:
                        list[1] += "�� �ٸ� ������� ��õ�� ���� ���� �ž�";
                        break;
                    case 5:
                        list[1] += "�� �� �� �ٽ� ��� ���� �ʾ�";
                        break;
                    case 6:
                        list[1] += "�� ������ ��ڴٸ� ���������� ������ �ž�";
                        break;
                    case 7:
                        list[1] += "���� ���� ������ ���� ������?";
                        break;
                    case 8:
                        list[1] += "�� ����ߴµ��� ���� ȿ���� ��Ÿ���� �ʾҾ�";
                        break;
                    case 9:
                        list[1] += "�� ���� ������ ����� ���� �ʾ�. ������ ���� ���״ϱ�";
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
                        list[1] += "�� ���� ���ݼ���� �߸����� �ʾҾ�. �׳� ���Ѱ���";
                        break;
                    case 1:
                        list[1] += "�� �谡 �������� ���ŵ� ���ٸ� ȿ���� �巯���� ���� �ž�";
                        break;
                    case 2:
                        list[1] += "�� ��� �� ������ �����̾�. ����� �߸� �� �� ������";
                        break;
                    case 3:
                        list[1] += "�� ������ ���������� �츸�� ������ �ƴϾ�";
                        break;
                    case 4:
                        list[1] += "�� ��õ�ϴ� ����� �и� �����̾�. �Ĵ� ����� ������";
                        break;
                    case 5:
                        list[1] += "���� �� �׳� ��̿����� �纸�� ����. ���ʺ�ó��";
                        break;
                    case 6:
                        list[1] += "�� �� �� ��� ��û�̴� ���谡�� �׸��δ� �� ����";
                        break;
                    case 7:
                        list[1] += "�̶�� �� �� �纸�� ������ ������. ���� ������ �ǰŵ�";
                        break;
                    case 8:
                        list[1] += "? �� �װ� �׳� ���� �ƴ϶� �൵ ��� �ִ� �ſ���?";
                        break;
                    case 9:
                        list[1] += "�� �� ��� ���ŵ� ��. �ٸ� ������ ���� ���� �����ϱ�";
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
                        list[1] += "�� �����̶�� �̸����� �Ĵ� �ͺ��� �׳� ����";
                        break;
                    case 1:
                        list[1] += "�� �� �򰡴� �ʿ� ����. �׳� ������â�̰ŵ�";
                        break;
                    case 2:
                        list[1] += "�� �� �ٿ��� �� ���� �¿��� ��ں��� ���Ǵ� �� ����";
                        break;
                    case 3:
                        list[1] += "�� ���� �Ű� �Ĵ� ���ݼ���� ������� �ŷ��� �� ����";
                        break;
                    case 4:
                        list[1] += "�� ���ô� �ͺ��� �ż��� ����� �Դ� �� �� ���� ����";
                        break;
                    case 5:
                        list[1] += " ������ �� �� ������ ��ĥ° ���� ��ġ�� �־�";
                        break;
                    case 6:
                        list[1] += "���� �����⸦ �ȸ鼭 ��� ��縦 �ϰ� �ִ� �ž�?";
                        break;
                    case 7:
                        list[1] += "�� ������ �����ؾ� ��. �Ĵ� �͵�, ����� �͵�, ��� �͵�";
                        break;
                    case 8:
                        list[1] += "�� �� ��ü�� �츮���� ū ������ ����. �ڿ� ��ȣ��� ������";
                        break;
                    case 9:
                        list[1] += "�� �� �پ߿� ������ ���̳� �ϳ� �� ì��� �� ����";
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
                    list[1] += "�� �����ڸ��� �ٷ� ȿ���� ���ͼ� ����";
                    break;
                case 1:
                    list[1] += " ���п� �̹� ���迡�� ����� ������!";
                    break;
                case 2:
                    list[1] += "�� ���谡�� �ʼ�ǰ����!";
                    break;
                case 3:
                    list[1] += "�� �� ���� ì�ܵ� �� ������ �� ����!";
                    break;
                case 4:
                    list[1] += "�� ���縦 ��ο��� �˸��� �;�!";
                    break;
                case 5:
                    list[1] += "�� ȿ���� ���� ��������!";
                    break;
                case 6:
                    list[1] += "�̶�� ������ �ƹ��� ��δ��� ��� �;�!";
                    break;
                case 7:
                    list[1] += "�� ȯ������ ȿ���� ���� ���� ������ ���� ���� ������!";
                    break;
                case 8:
                    list[1] += "�� �Բ���� �ƹ��� �賭�� ���赵 �������!";
                    break;
                case 9:
                    list[1] += "�� ��� ������ �� �˳�������...";
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
