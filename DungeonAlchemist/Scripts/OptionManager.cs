using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionUI;
    public Slider volumeSlider;
    public Slider sensivitySlider;

    public float volume, sensivity;

    public GameObject player;

    bool on;

    PlayerHand playerHand;
    PlayerLook playerLook;
    PlayerMovement playerMovement;

    MarketManager marketManager;

    void Start()
    {
        playerHand = player.GetComponentInChildren<PlayerHand>();
        playerLook = player.GetComponentInChildren<PlayerLook>();
        playerMovement = player.GetComponent<PlayerMovement>();
        marketManager = GetComponent<MarketManager>();
    }

    public void OpenOption()
    {
        on = true;
        OnMenu(on);
        playerLook.CursorMode(1);
        optionUI.SetActive(true);
    }

    public void SetVolume()
    {
        volume = volumeSlider.value;
        if(volume == -40f)
        {
            volume = -80f;
        }
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetSensivity()
    {
        sensivity = sensivitySlider.value;
        playerLook.SetSensivity(sensivity);
    }

    public void OnQuitOptionButton()
    {
        on = false;
        optionUI.SetActive(false);
        playerLook.CursorMode(0);
        OnMenu(on);
    }

    public void OnHomeButton()
    {
        OnMenu(false);
        SceneManager.LoadScene("HomeScene");
    }

    public void OnResetButton()
    {
        marketManager.ResetData();
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
