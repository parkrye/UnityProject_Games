using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data
{
    public string farmName;     // 농장 이름
    public bool[] open;         // 밭 해금
    public int[] seeds;         // 작물 종자 개수(당근, )
    public string[] fields;     // 밭 상태
    public int money;           // 재화
    public int[] prices;        // 시세
    public string[] plantNames; // 작물 이름들
    public int fieldPrice;      // 현재 밭 해금 가격

    public void Initialize()
    {
        farmName = "행복";
        open = new bool[25];
        open[0] = true;
        seeds = new int[16];
        seeds[0] = 1;
        fields = new string[25];
        for (int i = 0; i < 25; i++)
            fields[i] = "false/-1/-1/-1";
        money = 10;
        prices = new int[16] { 1, 6, 9, 12, 14, 18, 21, 24, 25, 27, 32, 34, 36, 40, 45, 49 };
        plantNames = new string[16] { "당근", "양배추", "양파", "호박", "귤", "감자", "사과", "토마토", "배", "바나나", "복숭아", "옥수수", "고추", "망고", "가지", "포도" };
        fieldPrice = 10;
    }

    public void SetFarmName(string n)
    {
        farmName = n;
    }

    public string GetFarmName()
    {
        return farmName;
    }

    public void SetOpen(bool[] o)
    {
        open = o;
    }

    public bool[] GetOpen()
    {
        return open;
    }

    public void AddSeed(int i, int s)
    {
        seeds[i] += s;
    }

    public int[] GetSeeds()
    {
        return seeds;
    }

    public void SetMoney(int m)
    {
        money = m;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetPrices(int[] c)
    {
        prices = c;
    }

    public int[] GetPrices()
    {
        return prices;
    }

    public string GetPlantName(int i)
    {
        return plantNames[i];
    }

    public void SetField(int i, string f)
    {
        fields[i] = f;
    }

    public string GetField(int i)
    {
        return fields[i];
    }

    public void SetFieldPrice(int p)
    {
        fieldPrice = p;
    }

    public int GetFieldPrice()
    {
        return fieldPrice;
    }
}
