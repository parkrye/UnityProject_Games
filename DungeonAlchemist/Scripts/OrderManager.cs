using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public MarketManager marketManager;
    public GameObject orderUI;
    public Text money;
    public GameObject player;
    public GameObject content;
    public Button[] contents;
    public AudioSource bellAudio;
    public AudioSource wrongAudio;

    bool on;

    PlayerHand playerHand;
    PlayerLook playerLook;
    PlayerMovement playerMovement;

    void Start()
    {
        playerHand = player.GetComponentInChildren<PlayerHand>();
        playerLook = player.GetComponentInChildren<PlayerLook>();
        playerMovement= player.GetComponent<PlayerMovement>();
        contents = content.GetComponentsInChildren<Button>();

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (contents.Length / 4 + 1) * 100 + 10);
        int j = 0;
        for (int i = 0; i < contents.Length; i++)
        {
            switch (j)
            {
                case 0:
                    contents[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(80, -55 - ((i / 4)) * 100, 0);
                    break;
                case 1:
                    contents[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(220, -55 - ((i / 4)) * 100, 0);
                    break;
                case 2:
                    contents[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -55 - ((i / 4)) * 100, 0);
                    break;
                case 3:
                    contents[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(500, -55 - ((i / 4)) * 100, 0);
                    break;
            }
            contents[i].gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(124, 90);

            j++;
            if(j == 4)
            {
                j = 0;
            }
        }
    }

    public void Order()
    {
        on = true;
        OnMenu(on);
        playerLook.CursorMode(1);
        money.text = "보유 자금 : $" + marketManager.GetMoney();
        orderUI.SetActive(true);
    }

    public int GetMoney()
    {
        return marketManager.GetMoney();
    }

    public bool ModifyMoney(float value)
    {
        if (marketManager.ModifyMoney(value))
        {
            bellAudio.Play();
            money.text = "보유 자금 : $" + marketManager.GetMoney();
            return true;
        }
        wrongAudio.Play();
        StartCoroutine(LowBalance());
        return false;
    }

    public void OnQuitOrderButton()
    {
        on = false;
        orderUI.SetActive(false);
        playerLook.CursorMode(0);
        OnMenu(on);
    }

    IEnumerator LowBalance()
    {
        money.text = "잔액 부족!";
        yield return new WaitForSecondsRealtime(1f);
        money.text = "보유 자금 : $" + marketManager.GetMoney();
    }

    void OnMenu(bool b)
    {
        if (b)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        playerHand.OnMenu(b);
        playerLook.OnMenu(b);
        playerMovement.OnMenu(b);
    }

    public bool GetOn()
    {
        return on;
    }
}
