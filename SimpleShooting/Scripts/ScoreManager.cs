using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int[] stage;
    int index;
    static int score;
    public RectTransform bar;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        score = 0;
    }

    private void FixedUpdate()
    {
        if (index < stage.Length)
        {
            if (score > stage[index])
            {
                score = stage[index];
            }
            Vector2 tmp = new Vector2(0f, (331f * score) / stage[index] - 331f);
            bar.offsetMax = tmp;
            if (score == stage[index])
            {
                index += 1;
                score = 0;
                LifeManager.AddLife(1);
            }
        }
        else
        {
            StageClearManager.StageClear();
        }
    }

    public static void AddScore(int _score)
    {
        score += _score;
    }

    public static int GetScore()
    {
        return score;
    }
}
