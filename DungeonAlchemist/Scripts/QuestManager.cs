using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public MarketManager marketManager;
    public PotionReview potionReview;
    public TextMesh[] quests = new TextMesh[3];
    public int[] quest = new int[3] { 0, 0, 0 };
    public AudioSource bellAudio;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, 9);
            switch (random)
            {
                case 0:
                    quest[i] = 2;
                    break;
                case 1:
                    quest[i] = 3;
                    break;
                case 2:
                    quest[i] = 5;
                    break;
                case 3:
                    quest[i] = 7;
                    break;
                case 4:
                    quest[i] = 2 * 5;
                    break;
                case 5:
                    quest[i] = 2 * 7;
                    break;
                case 6:
                    quest[i] = 3 * 5;
                    break;
                case 7:
                    quest[i] = 3 * 7;
                    break;
                case 8:
                    quest[i] = 1;
                    break;
            }

            if (quest[i] % 2 == 0)
            {
                if (quest[i] % 5 == 0)
                {
                    quests[i].text = (i + 1) + ".희망의 물약";
                }
                else if (quest[i] % 7 == 0)
                {
                    quests[i].text = (i + 1) + ".정의의 물약";
                }
                else
                {
                    quests[i].text = (i + 1) + ".용기의 물약";
                }
            }
            else if (quest[i] % 3 == 0)
            {
                if (quest[i] % 5 == 0)
                {
                    quests[i].text = (i + 1) + ".성장의 물약";
                }
                else if (quest[i] % 7 == 0)
                {
                    quests[i].text = (i + 1) + ".규율의 물약";
                }
                else
                {
                    quests[i].text = (i + 1) + ".지혜의 물약";
                }
            }
            else if (quest[i] % 5 == 0)
            {
                quests[i].text = (i + 1) + ".자유의 물약";
            }
            else if (quest[i] % 7 == 0)
            {
                quests[i].text = (i + 1) + ".인내의 물약";
            }
            else
            {
                quests[i].text = (i + 1) + ".순수의 물약";
            }
        }

        StartCoroutine("Request", 0);
        StartCoroutine("Request", 1);
        StartCoroutine("Request", 2);
    }

    IEnumerator Request(int i)
    {
        int timer = 0;
        while (true)
        {
            if (quest[i] == 0)
            {
                if(timer == 60 + i)
                {
                    int random = Random.Range(0, 9);
                    switch (random)
                    {
                        case 0:
                            quest[i] = 2;
                            break;
                        case 1:
                            quest[i] = 3;
                            break;
                        case 2:
                            quest[i] = 5;
                            break;
                        case 3:
                            quest[i] = 7;
                            break;
                        case 4:
                            quest[i] = 2 * 5;
                            break;
                        case 5:
                            quest[i] = 2 * 7;
                            break;
                        case 6:
                            quest[i] = 3 * 5;
                            break;
                        case 7:
                            quest[i] = 3 * 7;
                            break;
                        case 8:
                            quest[i] = 1;
                            break;
                    }

                    if (quest[i] % 2 == 0)
                    {
                        if (quest[i] % 5 == 0)
                        {
                            quests[i].text = (i + 1) + ".희망의 물약";
                        }
                        else if (quest[i] % 7 == 0)
                        {
                            quests[i].text = (i + 1) + ".정의의 물약";
                        }
                        else
                        {
                            quests[i].text = (i + 1) + ".용기의 물약";
                        }
                    }
                    else if (quest[i] % 3 == 0)
                    {
                        if (quest[i] % 5 == 0)
                        {
                            quests[i].text = (i + 1) + ".성장의 물약";
                        }
                        else if (quest[i] % 7 == 0)
                        {
                            quests[i].text = (i + 1) + ".규율의 물약";
                        }
                        else
                        {
                            quests[i].text = (i + 1) + ".지혜의 물약";
                        }
                    }
                    else if (quest[i] % 5 == 0)
                    {
                        quests[i].text = (i + 1) + ".자유의 물약";
                    }
                    else if (quest[i] % 7 == 0)
                    {
                        quests[i].text = (i + 1) + ".인내의 물약";
                    }
                    else
                    {
                        quests[i].text = (i + 1) + ".순수의 물약";
                    }
                    timer = 0;
                }
            }
            else
            {
                if(timer == 90 + i * 3)
                {
                    quest[i] = 0;
                    quests[i].text = "";
                    timer = 0;
                }
            }
            yield return new WaitForSeconds(1);
            timer++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Potion")
        {
            for(int i = 0; i < 3; i++)
            {
                if (quest[i] == collision.gameObject.GetComponent<ProductManager>().GetElemental())
                {
                    bellAudio.Play();
                    potionReview.EnQueue(collision.gameObject.GetComponent<ProductManager>());
                    float productPrice = collision.gameObject.GetComponent<ProductManager>().GetPrice() * 1.1f;
                    marketManager.ModifyMoney(productPrice);
                    quest[i] = 0;
                    quests[i].text = "";
                    Destroy(collision.gameObject);
                    break;
                }
            }
        }
    }
}
