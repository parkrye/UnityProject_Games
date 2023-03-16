using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class HomeSceneUI : MonoBehaviour
{
    public GameObject bird;

    public AudioMixer audioMixer;
    public GameObject[] uis;    // quit, color, start, help, option
    public GameObject skinCamera;
    public Dropdown[] dropdowns;    // part, color, stage, language
    public Slider[] volumeSliders;        // bgm, se
    public Toggle[] volumeToggles;  // bgm, se
    public Text[] texts;    // StageText, QuitTextTitle, QuitTextQuit, QuitTextCancel, lw, rw, t, g, l, 
    public TextMesh[] textMeshs; // help, color, start, option, quit
    bool firstPlay;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    /// <summary>
    /// 로딩 시간 대기 후 언어 적용
    /// </summary>
    /// <returns></returns>
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);
        if (!PlayerPrefs.HasKey("Stage"))
        {
            firstPlay = true;
            PlayerPrefs.SetInt("Stage", 0);
        }

        Translate();
        if (firstPlay)
        {
            SceneManager.LoadScene("Stage0");
        }
    }

    /// <summary>
    /// Quit UI
    /// </summary>
    public void OnQuitUIButtonQuit()
    {
        Application.Quit(0);
    }

    public void OnQuitUIButtonCancel()
    {
        uis[0].SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// Coloring UI
    /// </summary>
    public void OncoloringUIButtonColoring()
    {
        BirdColor.Player.Coloring(dropdowns[0].value, dropdowns[1].value);
    }

    public void OncoloringUIButtonDone()
    {
        uis[1].SetActive(false);
        skinCamera.SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// Start UI
    /// </summary>
    public void OnStartUIDropdownChanged()
    {
        texts[0].text = ScriptsManager.Instance.GetStageScript(1, dropdowns[2].value);
    }

    public void OnStartUIButtonStart()
    {
        if(dropdowns[2].value <= PlayerPrefs.GetInt("Stage"))
        {
            BirdMove.Player.ShowWaitImage();
            SceneManager.LoadScene("Stage" + dropdowns[2].value);
            BirdMove.Player.gameObject.transform.position = new Vector3(0, 2, 0);
            BirdMove.Player.UseCursor(false);
        }
    }

    public void OnStartUIButtonCancel()
    {
        uis[2].SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// Help UI
    /// </summary>
    public void OnHelpUIButtonQuit()
    {
        uis[3].SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// Options UI
    /// </summary>
    public void OnOptionSliderBGM()
    {
        audioMixer.SetFloat("BGMParam", volumeSliders[0].value * 10f);
    }

    public void OnOptionSliderSE()
    {
        audioMixer.SetFloat("SEParam", volumeSliders[1].value * 10f);
    }

    public void OnOptionToggleBGM()
    {
        if (!volumeToggles[0].isOn) audioMixer.SetFloat("BGMParam", -50f);
        else audioMixer.SetFloat("BGMParam", volumeSliders[0].value * 10f);
    }

    public void OnOptionToggleSE()
    {
        if (!volumeToggles[1].isOn) audioMixer.SetFloat("SEParam", -50f);
        else audioMixer.SetFloat("SEParam", volumeSliders[1].value * 10f);
    }

    public void OnOptionDopdownLanguage()
    {
        ScriptsManager.Instance.SetLanguage(1 - PlayerPrefs.GetInt("Language"));
        Translate();
    }

    public void OnOptionButtonReset()
    {
        volumeSliders[0].value = 0;
        volumeSliders[1].value = 0;
        audioMixer.SetFloat("BGMParam", volumeSliders[0].value * 10f);
        audioMixer.SetFloat("SEParam", volumeSliders[1].value * 10f);
        volumeToggles[0].isOn = true;
        volumeToggles[1].isOn = true;
    }

    public void OnOptionButtonDone()
    {
        uis[4].SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// Reset UI
    /// </summary>
    public void OnResetUIButtonYes()
    {
        PlayerPrefs.DeleteAll();
        BirdMove.Player.ClearGame();
        BirdMove.Player.ShowWaitImage();
        SceneManager.LoadScene("HomeScene");
        BirdMove.Player.UseCursor(false);
    }

    public void OnResetUIButtonNo()
    {
        uis[5].SetActive(false);
        BirdMove.Player.UseCursor(false);
        Translate();
    }

    /// <summary>
    /// 언어 변동시 HomeScene의 언어를 모두 수정하는 함수
    /// </summary>
    public void Translate()
    {
        // 스테이지 이름, 설명
        List<string> list = new List<string>();
        for (int i = 0; i <= PlayerPrefs.GetInt("Stage") && i < 8; i++) list.Add(ScriptsManager.Instance.GetStageScript(0, i));
        dropdowns[2].ClearOptions();
        dropdowns[2].AddOptions(list);
        texts[0].text = ScriptsManager.Instance.GetStageScript(1, dropdowns[2].value);

        // 게임 종료 UI
        texts[1].text = ScriptsManager.Instance.GetQuitGameScript(0);
        texts[2].text = ScriptsManager.Instance.GetQuitGameScript(1);
        texts[3].text = ScriptsManager.Instance.GetQuitGameScript(2);

        // 색칠 UI
        texts[4].text = ScriptsManager.Instance.GetSkinScript(0);
        texts[5].text = ScriptsManager.Instance.GetSkinScript(2);
        texts[6].text = ScriptsManager.Instance.GetSkinScript(4);
        texts[7].text = ScriptsManager.Instance.GetSkinScript(5);
        list.Clear();
        for (int i = 0; i < 9; i++) list.Add(ScriptsManager.Instance.GetSkinScript(1, i));
        dropdowns[0].ClearOptions();
        dropdowns[0].AddOptions(list);
        list.Clear();
        for (int i = 0; i < 16; i++) list.Add(ScriptsManager.Instance.GetSkinScript(3, i));
        dropdowns[1].ClearOptions();
        dropdowns[1].AddOptions(list);

        // HomeScene 오브젝트 표시 이름
        textMeshs[0].text = ScriptsManager.Instance.GetHomeSceneScript(0);
        textMeshs[1].text = ScriptsManager.Instance.GetHomeSceneScript(1);
        textMeshs[2].text = ScriptsManager.Instance.GetHomeSceneScript(2);
        textMeshs[3].text = ScriptsManager.Instance.GetHomeSceneScript(3);
        textMeshs[4].text = ScriptsManager.Instance.GetHomeSceneScript(4);

        // 옵션 UI
        texts[8].text = ScriptsManager.Instance.GetOptionScript(0);
        texts[9].text = ScriptsManager.Instance.GetOptionScript(1);
        texts[10].text = ScriptsManager.Instance.GetOptionScript(2);
        texts[11].text = ScriptsManager.Instance.GetOptionScript(3);
        texts[12].text = ScriptsManager.Instance.GetOptionScript(5);
        texts[13].text = ScriptsManager.Instance.GetOptionScript(6);
        list.Clear();
        list.Add(ScriptsManager.Instance.GetOptionScript(4, PlayerPrefs.GetInt("Language")));
        list.Add(ScriptsManager.Instance.GetOptionScript(4, 1 - PlayerPrefs.GetInt("Language")));
        dropdowns[3].ClearOptions();
        dropdowns[3].AddOptions(list);

        // 게임 리셋 UI
        texts[14].text = ScriptsManager.Instance.GetResetGameScript(0);
        texts[15].text = ScriptsManager.Instance.GetResetGameScript(1);
        texts[16].text = ScriptsManager.Instance.GetResetGameScript(2);

        // 플레이 정보
        BirdMove.Player.Translate();
    }
}
