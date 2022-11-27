using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankSystem : MonoBehaviour
{
    public Text rankText;
    public Text scoreText;
    public Text lifeText;
    public int score = 0;
    public int life = 100;
    public int dec = 1;

    public Text rankUpText;
    public Text nowScoreText;
    public Text highScoreText;

    public GameObject playUI;
    public GameObject overUI;

    // Start is called before the first frame update
    void Start()
    {
        overUI.SetActive(false);

        rankText.text = "RANK : " + PlayerPrefs.GetInt("Rank");
        scoreText.text = "SCORE : " + score;
        lifeText.text = life.ToString();
        StartCoroutine(Scoring());
    }

    IEnumerator Scoring()
    {
        int counter = 0;
        bool loop = true;
        while (loop)
        {
            yield return new WaitForSeconds(1f);
            life -= dec;
            lifeText.text = life.ToString();
            if(life <= 0)
            {
                GameOver();
                loop = false;
            }
            else
            {
                counter += 1;
                if (counter == 2)
                {
                    score += dec;
                    counter = 0;
                }
                scoreText.text = "SCORE : " + score.ToString();
            }
        }
    }

    public void DecSet(int point)
    {
        dec = point;
    }

    public void LifeUp(int point)
    {
        life += point;
        if(life < 0)
        {
            life = 0;
        }
        lifeText.text = life.ToString();
    }

    public void ScoreUp(int point)
    {
        score += point;
        if (score < 0)
        {
            int diff = score + point;
            score = 0;
            LifeUp(diff);
        }
        scoreText.text = "SCORE : " + score.ToString();
    }

    public void Reset()
    {
        life = 100;
        score = 0;
        lifeText.text = life.ToString();
        scoreText.text = "SCORE : " + score.ToString();
    }

    private void GameOver()
    {
        StopCoroutine(Scoring());
        Time.timeScale = 0;
        nowScoreText.text = "SCORE : " + score.ToString();
        if (!PlayerPrefs.HasKey("High") || PlayerPrefs.GetInt("High") < score)
            PlayerPrefs.SetInt("High", score);
        highScoreText.text = "RECORD : " + PlayerPrefs.GetInt("High").ToString();
        int rank = PlayerPrefs.GetInt("Rank");
        int rankScore = (score / 100) + 1;
        if(rank < rankScore)
        {
            rankUpText.text = "RANK UP!! " + rank.ToString() + " > " + rankScore.ToString();
            PlayerPrefs.SetInt("Rank", rankScore);
        }
        else
        {
            rankUpText.text = "YOE ARE RANK " + rank.ToString();
        }

        playUI.SetActive(false);
        overUI.SetActive(true);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("Track01");
        Time.timeScale = 1;
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Track00");
        Time.timeScale = 1;
    }
}
