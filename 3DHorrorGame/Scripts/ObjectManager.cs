using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public Text ObjectText, menuObjectText;
    public GameObject staminaBar;
    public MouseLook mouseLook;
    public PlayerMovement player;
    public Enemy enemy;
    public GameObject uiCanvas, menuCanvas, successCanvas, failCanvas;

    public enum Mode {normal, infinite };
    public Mode mode;

    int difficulty = 0;
    int objective = 0;
    int goal = 10;

    private void Start()
    {
        ModifyDifficulty(PlayerPrefs.GetInt("Difficulty"));
        if (mode == Mode.normal)
        {
            ObjectText.text = "목표\n" + objective + " / 10";
            menuObjectText.text = "목표\n" + objective + " / 10";
        }
        else
        {
            ObjectText.text = "기록\n" + objective + " / " + PlayerPrefs.GetInt("Score");
            menuObjectText.text = "기록\n" + objective + " / " + PlayerPrefs.GetInt("Score");
        }
    }

    public void ObjectCollect()
    {
        enemy.LevelUp(objective, difficulty);
        objective += 1;
        if(mode == Mode.normal)
        {
            ObjectText.text = "목표\n" + objective + " / 10";
            menuObjectText.text = "목표\n" + objective + " / 10";
            if (objective == goal)
            {
                player.ControllOut();
                mouseLook.OpenMenu();
                uiCanvas.SetActive(false);
                successCanvas.SetActive(true);
            }
        }
        else
        {
            ObjectText.text = "기록\n" + objective + " / " + PlayerPrefs.GetInt("Score");
            menuObjectText.text = "기록\n" + objective + " / " + PlayerPrefs.GetInt("Score");
        }
    }

    public void StaminaModified(float stamina, float maxStamina)
    {
        if (stamina >= maxStamina)
        {
            staminaBar.GetComponent<Image>().enabled = false;
            staminaBar.GetComponent<RectTransform>().offsetMin = new Vector2(100, 30);
            staminaBar.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -1000);
        }
        else
        {
            staminaBar.GetComponent<Image>().enabled = true;
            staminaBar.GetComponent<RectTransform>().offsetMin = new Vector2(100 + ((1 - (stamina / maxStamina)) * 860), 30);
            staminaBar.GetComponent<RectTransform>().offsetMax = new Vector2(-100 - ((1 - (stamina / maxStamina)) * 860), -1000);

            staminaBar.GetComponent<Image>().color = Color.white;
            if (stamina < maxStamina / 2f)
            {
                staminaBar.GetComponent<Image>().color = Color.yellow;
                if (stamina < maxStamina / 4f)
                {
                    staminaBar.GetComponent<Image>().color = Color.red;
                }
            }
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;
        uiCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        mouseLook.OpenMenu();
    }

    public void QuitMenu()
    {
        Time.timeScale = 1f;
        uiCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        mouseLook.QuitMenu();
        player.ControllOut();
    }

    void ModifyDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
        RenderSettings.fogDensity = 0.03f * (difficulty + 1);
        enemy.LevelUp(objective, difficulty);
    }

    public void GameOver()
    {
        player.ControllOut();
        mouseLook.OpenMenu();
        uiCanvas.SetActive(false);
        failCanvas.SetActive(true);
    }

    public int GetObjective()
    {
        return objective;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
}
