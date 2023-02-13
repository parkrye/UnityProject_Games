using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialUI;
    public Image image;
    public Sprite[] sprites = new Sprite[10];
    public GameObject prevButton, nextButton;
    int page;

    // Start is called before the first frame update
    void Start()
    {
        page = 0;
        image.sprite = sprites[page];
    }

    public void StartTutorial()
    {
        tutorialUI.SetActive(true);
    }

    public void OnPrevButton()
    {
        if(page > 1)
        {
            prevButton.SetActive(true);
            page--;
            image.sprite = sprites[page];
        }
        else
        {
            if(page == 1)
            {
                prevButton.SetActive(false);
                page--;
                image.sprite = sprites[page];
            }
        }
    }

    public void OnNextButton()
    {
        if (page < 8)
        {
            nextButton.GetComponentInChildren<Text>().text = "다음으로";
            page++;
            image.sprite = sprites[page];
        }
        else
        {
            if(page == 8)
            {
                nextButton.GetComponentInChildren<Text>().text = "종료하기";
                page++;
                image.sprite = sprites[page];
            }
            else
            {
                tutorialUI.SetActive(false);
            }
        }

        if (page > 0)
        {
            prevButton.SetActive(true);
        }
        else
        {
            prevButton.SetActive(false);
        }
    }
}
