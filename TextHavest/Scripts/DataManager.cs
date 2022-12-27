using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // 저장용 클래스 변수
    public Data data;

    // 저장 데이터 파일 이름
    string gameDataFileName;

    public FarmManager farmManager;
    public FieldsManager fieldsManager;
    public PricesManager pricesManager;

    public void Initialize()
    {
        data = new Data();
        data.Initialize();
        gameDataFileName = "GameData.json";

        // 저장 경로: Asset
        string filePath = Application.dataPath + "/" + gameDataFileName;

        // 파일 존재 확인
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(fromJsonData);
        }
    }

    // 저장하기
    public void SaveGameData()
    {
        // data 수정
        data.SetFarmName(farmManager.GetFarmName());
        data.SetMoney(farmManager.GetFarmMoney());
        data.SetFieldPrice(fieldsManager.GetFieldPrice());
        data.SetOpen(fieldsManager.GetFieldOpens());
        data.SetPrices(pricesManager.GetPrices());
        for (int i = 0; i < 25; i++)
        {
            data.SetField(i, fieldsManager.fieldList[i].GetComponent<Field>().ToSaveStyle());
        }

        // 클래스를 Json 형식으로 전환. bool: 가독성 좋게 작성
        string toJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.dataPath + "/" + gameDataFileName;

        // 저장된 파일이 있다면 덮어쓰고, 없다면 새로 생성
        File.WriteAllText(filePath, toJsonData);
    }

}
