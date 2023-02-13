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
        moneyText.text = "보유자금\n$" + money;

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
                levelText.text = "풋내기\n연금술사";
                break;
            case 1:
                levelText.text = "견습\n연금술사";
                break;
            case 2:
                levelText.text = "평범한\n연금술사";
                break;
            case 3:
                levelText.text = "솜씨좋은\n연금술사";
                break;
            case 4:
                levelText.text = "유명한\n연금술사";
                break;
            case 5:
                levelText.text = "전설적인\n연금술사";
                break;
            case 6:
                levelText.text = "위대한\n연금술사";
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
        ReputationText.text = "명성\n" + reputation;
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
            moneyText.text = "보유자금\n$" + money;
            LevelUp();
            return true;
        }
        else if (money + intValue >= 0)
        {
            money += intValue;
            PlayerPrefs.SetInt("Money", money);
            moneyText.text = "보유자금\n$" + money;
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
        ReputationText.text = "명성\n" + reputation;
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
                            levelText.text = "견습\n연금술사";
                            break;
                        case 2:
                            levelText.text = "평범한\n연금술사";
                            break;
                        case 3:
                            levelText.text = "솜씨좋은\n연금술사";
                            break;
                        case 4:
                            levelText.text = "유명한\n연금술사";
                            break;
                        case 5:
                            levelText.text = "전설적인\n연금술사";
                            break;
                        case 6:
                            levelText.text = "위대한\n연금술사";
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
        moneyText.text = "보유자금\n$" + money;
        levelText.text = "풋내기\n연금술사";
        ReputationText.text = "명성\n" + reputation;
    }
}
