using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Manager : MonoBehaviour
{
    public Text textHighScore;
    public Text textscore;

    // Start is called before the first frame update
    void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Show()
    {
        int highScore = FindObjectOfType<Score_Manager>().GetHighScore();
        int score = FindObjectOfType<Score_Manager>().GetScore();
        textHighScore.text = "HighScore : " + highScore.ToString();
        textscore.text = "Score : " + score.ToString();
        transform.gameObject.SetActive(true);
    }
    public void OnClick_Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
