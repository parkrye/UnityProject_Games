using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeUIManager : MonoBehaviour
{
    public Text[] texts;    // clear, next, home, pause, retry, home
    public StageClear stage;

    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            texts[i].text = ScriptsManager.Instance.GetESCScript(i);
        }

        if(stage.GetStage() == 8)
        {
            texts[0].text = ScriptsManager.Instance.GetESCScript(9);
        }
    }
}
