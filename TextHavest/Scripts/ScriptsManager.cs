using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    public DataManager dataManager;
    public FieldsManager fieldsManager;
    public FarmManager farmManager;
    public PricesManager pricesManager;

    // 생성시 각 스크립트 초기화
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        dataManager.Initialize();
        fieldsManager.Initialize();
        farmManager.Initialize();
        pricesManager.Initialize();
    }
}
