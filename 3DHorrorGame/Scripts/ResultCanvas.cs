using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCanvas : MonoBehaviour
{
    public enum Type {Success, Fail};
    public Type type;
    public enum Mode { Normal, Infinite };
    public Mode mode;

    public ObjectManager objectManager;

    public Text text, score;
    int objective, difficulty;

    private void Start()
    {
        Time.timeScale = 0f;
        if(type == Type.Fail)
        {
            if(mode == Mode.Infinite)
            {
                difficulty = objectManager.GetDifficulty();
                objective = objectManager.GetObjective() * (difficulty + 1);
                if (objective > PlayerPrefs.GetInt("Score"))
                {
                    text.text = "��� ����!";
                    score.text = PlayerPrefs.GetInt("Score") + " => " + objective;
                    PlayerPrefs.SetInt("Score", objective);
                }
                else
                {
                    text.text = "��� ���� ����...";
                    score.text = "�ְ� ��� : " + PlayerPrefs.GetInt("Score") + "\n���� ��� : " + objective;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Scene_00");
        }
    }
}
