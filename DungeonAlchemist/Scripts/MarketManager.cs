using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public int level;
    public int money, totalEarn;
    public int reputation;
    public TextMesh levelText, moneyText, ReputationText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            money = 10;
            PlayerPrefs.SetInt("Money", money);
        }
        if (PlayerPrefs.HasKey("TotalEarn"))
        {
            totalEarn = PlayerPrefs.GetInt("TotalEarn");
        }
        else
        {
            totalEarn = 0;
            PlayerPrefs.SetInt("TotalEarn", totalEarn);
        }
        moneyText.text = "�����ڱ�\n$" + money;

        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            level = 0;
            PlayerPrefs.SetInt("Level", level);
        }
        switch (level)
        {
            case 0:
                levelText.text = "ǲ����\n���ݼ���";
                break;
            case 1:
                levelText.text = "�߽�\n���ݼ���";
                break;
            case 2:
                levelText.text = "�����\n���ݼ���";
                break;
            case 3:
                levelText.text = "�ؾ�����\n���ݼ���";
                break;
            case 4:
                levelText.text = "������\n���ݼ���";
                break;
            case 5:
                levelText.text = "��������\n���ݼ���";
                break;
            case 6:
                levelText.text = "������\n���ݼ���";
                break;
        }

        if (PlayerPrefs.HasKey("Reputation"))
        {
            reputation = PlayerPrefs.GetInt("Reputation");
        }
        else
        {
            reputation = 0;
            PlayerPrefs.SetInt("Reputation", reputation);
        }
        ReputationText.text = "��\n" + reputation;
    }

    public bool ModifyMoney(float value, bool earn = true)
    {
        int intValue = (int) value;
        if(intValue >= 0)
        {
            money += intValue;
            PlayerPrefs.SetInt("Money", money);
            if (earn)
            {
                totalEarn += intValue;
                PlayerPrefs.SetInt("TotalEarn", totalEarn);
            }
            moneyText.text = "�����ڱ�\n$" + money;
            LevelUp();
            return true;
        }
        else if (money + intValue >= 0)
        {
            money += intValue;
            PlayerPrefs.SetInt("Money", money);
            moneyText.text = "�����ڱ�\n$" + money;
            if (money < 5)
            {
                StopCoroutine(BasicEarn());
                StartCoroutine(BasicEarn());
            }
            return true;
        }
        return false;
    }

    public void ModifyReputation(int value)
    {
        reputation += value;
        if(reputation < 0)
        {
            reputation = 0;
        }
        PlayerPrefs.SetInt("Reputation", reputation);
        ReputationText.text = "��\n" + reputation;
        LevelUp();
    }

    void LevelUp()
    {
        if (level < 6)
        {
            if (totalEarn > ((level + 1) * (level + 1)) * 100)
            {
                if (reputation > ((level + 1) * (level + 1)) * 10)
                {
                    level++;
                    PlayerPrefs.SetInt("Level", level);
                    switch (level)
                    {
                        case 1:
                            levelText.text = "�߽�\n���ݼ���";
                            break;
                        case 2:
                            levelText.text = "�����\n���ݼ���";
                            break;
                        case 3:
                            levelText.text = "�ؾ�����\n���ݼ���";
                            break;
                        case 4:
                            levelText.text = "������\n���ݼ���";
                            break;
                        case 5:
                            levelText.text = "��������\n���ݼ���";
                            break;
                        case 6:
                            levelText.text = "������\n���ݼ���";
                            break;
                    }
                }
            }
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetLevel()
    {
        return level;
    }

    IEnumerator BasicEarn()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(10f);
            if (money > 5)
            {
                break;
            }
            ModifyMoney(1, false);
        }
    }

    public void ResetData()
    {
        money = 10;
        PlayerPrefs.SetInt("Money", money);
        totalEarn = 0;
        PlayerPrefs.SetInt("TotalEarn", totalEarn);
        level = 0;
        PlayerPrefs.SetInt("Level", level);
        reputation = 0;
        PlayerPrefs.SetInt("Reputation", reputation);
        moneyText.text = "�����ڱ�\n$" + money;
        levelText.text = "ǲ����\n���ݼ���";
        ReputationText.text = "��\n" + reputation;
    }
}
