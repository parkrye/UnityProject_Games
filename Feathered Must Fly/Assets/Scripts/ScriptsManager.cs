using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    private static ScriptsManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetInt("Language", 0);
            }
            SetLanguage(PlayerPrefs.GetInt("Language"));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ScriptsManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public enum Language { English, Korean };
    public Language language;

    public void SetLanguage(int _language)
    {
        switch (_language)
        {
            default:
            case 0:
                language = Language.English;
                break;
            case 1:
                language = Language.Korean;
                break;
        }
        PlayerPrefs.SetInt("Language", _language);
    }

    public string GetStageScript(int type, int num)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "Nest";
                            case 1:
                                return "Coop";
                            case 2:
                                return "Yard";
                            case 3:
                                return "Road";
                            case 4:
                                return "Field";
                            case 5:
                                return "Forest";
                            case 6:
                                return "Tree";
                            case 7:
                                return "the Nest";
                        }
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "I think this is Nest";
                            case 1:
                                return "Perhaps this is Coop";
                            case 2:
                                return "Conceivably this is Yard";
                            case 3:
                                return "This could be Road";
                            case 4:
                                return "Maybe this is Field";
                            case 5:
                                return "This looks like Forest";
                            case 6:
                                return "This seems like Tree";
                            case 7:
                                return "This is the Nest.";
                        }
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "둥지";
                            case 1:
                                return "우리";
                            case 2:
                                return "마당";
                            case 3:
                                return "도로";
                            case 4:
                                return "들판";
                            case 5:
                                return "수풀";
                            case 6:
                                return "나무";
                            case 7:
                                return "요람";
                        }
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "일단 여기는 둥지인 것 같다";
                            case 1:
                                return "아마도 여기는 우리인 것 같다";
                            case 2:
                                return "나름 여기는 마당인 것 같다";
                            case 3:
                                return "그럭저럭 여기는 도로인 것 같다";
                            case 4:
                                return "그래도 여기는 들판인 것 같다";
                            case 5:
                                return "어쩌면 여기는 수풀인 것 같다";
                            case 6:
                                return "아무튼 여기는 나무인 것 같다";
                            case 7:
                                return "여기는 요람이다";
                        }
                }
        }
    }

    public string GetQuitGameScript(int type)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Really?";
                    case 1:
                        return "Quit";
                    case 2:
                        return "Cancel";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "정말요?";
                    case 1:
                        return "종료";
                    case 2:
                        return "취소";
                }
        }
    }

    public string GetResetGameScript(int type)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Really?";
                    case 1:
                        return "Yes";
                    case 2:
                        return "No";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "정말요?";
                    case 1:
                        return "리셋";
                    case 2:
                        return "취소";
                }
        }
    }
  
    public string GetSkinScript(int type, int num = 0)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Part";
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "Body";
                            case 1:
                                return "Break";
                            case 2:
                                return "EyeLeft";
                            case 3:
                                return "EyeRight";
                            case 4:
                                return "Hat";
                            case 5:
                                return "Tail";
                            case 6:
                                return "WingLeft";
                            case 7:
                                return "WingRight";
                            case 8:
                                return "Beard";
                        }
                    case 2:
                        return "Color";
                    case 3:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "Black";
                            case 1:
                                return "Blue";
                            case 2:
                                return "BlueGreen";
                            case 3:
                                return "Brown";
                            case 4:
                                return "BrownYellow";
                            case 5:
                                return "Green";
                            case 6:
                                return "GreenYellow";
                            case 7:
                                return "Gray";
                            case 8:
                                return "GrayBlack";
                            case 9:
                                return "Purple";
                            case 10:
                                return "PurpleBlue";
                            case 11:
                                return "Red";
                            case 12:
                                return "RedPurple";
                            case 13:
                                return "White";
                            case 14:
                                return "Yellow";
                            case 15:
                                return "YellowRed";
                        }
                    case 4:
                        return "Coloring!";
                    case 5:
                        return "Done";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "부위";
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "몸통";
                            case 1:
                                return "부리";
                            case 2:
                                return "왼 눈";
                            case 3:
                                return "오른 눈";
                            case 4:
                                return "볏";
                            case 5:
                                return "꼬리";
                            case 6:
                                return "왼 날개";
                            case 7:
                                return "오른 날개";
                            case 8:
                                return "수염";
                        }
                    case 2:
                        return "색";
                    case 3:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "검정색";
                            case 1:
                                return "파란색";
                            case 2:
                                return "하늘색";
                            case 3:
                                return "황갈색";
                            case 4:
                                return "황토색";
                            case 5:
                                return "초록색";
                            case 6:
                                return "연녹색";
                            case 7:
                                return "연회색";
                            case 8:
                                return "검회색";
                            case 9:
                                return "보라색";
                            case 10:
                                return "청회색";
                            case 11:
                                return "붉은색";
                            case 12:
                                return "선홍색";
                            case 13:
                                return "하얀색";
                            case 14:
                                return "노란색";
                            case 15:
                                return "주홍색";
                        }
                    case 4:
                        return "색칠하기!";
                    case 5:
                        return "완료";
                }
        }
    }

    public string GetInGameUIScript(int type)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Left Wing";
                    case 1:
                        return "Right Wing";
                    case 2:
                        return "Tail";
                    case 3:
                        return "Ground";
                    case 4:
                        return "Look Back";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "왼 날개";
                    case 1:
                        return "오른 날개";
                    case 2:
                        return "꼬리";
                    case 3:
                        return "착지";
                    case 4:
                        return "뒤보기";
                }
        }
    }

    public string GetHomeSceneScript(int type)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Help";
                    case 1:
                        return "Coloring";
                    case 2:
                        return "Start Game";
                    case 3:
                        return "Options";
                    case 4:
                        return "Quit Game";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "도움말";
                    case 1:
                        return "채색하기";
                    case 2:
                        return "게임 시작";
                    case 3:
                        return "게임 설정";
                    case 4:
                        return "게임 종료";
                }
        }
    }

    public string GetOptionScript(int type, int num = 0)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Volume";
                    case 1:
                        return "BGM";
                    case 2:
                        return "SE";
                    case 3:
                        return "Language";
                    case 4:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "English";
                            case 1:
                                return "Koraean";
                        }
                    case 5:
                        return "Done";
                    case 6:
                        return "Reset";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "음량";
                    case 1:
                        return "배경음";
                    case 2:
                        return "효과음";
                    case 3:
                        return "언어";
                    case 4:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "영어";
                            case 1:
                                return "한국어";
                        }
                    case 5:
                        return "완료";
                    case 6:
                        return "초기화";
                }
        }
    }

    public string GetESCScript(int type, int num = 0)
    {
        switch (language)
        {
            default:
            case Language.English:
                switch (type)
                {
                    default:
                    case 0:
                        return "Stage Clear!";
                    case 1:
                        return "Next";
                    case 2:
                        return "Home";
                    case 3:
                        return "Paused";
                    case 4: 
                        return "Retry";
                    case 5:
                        return "Home";
                    case 6:
                        return "Game Over";
                    case 7:
                        return "Reset";
                    case 8:
                        return "Main";
                    case 9:
                        return "Game Clear!\nCongratulations!";
                }
            case Language.Korean:
                switch (type)
                {
                    default:
                    case 0:
                        return "스테이지 클리어!";
                    case 1:
                        return "다음";
                    case 2:
                        return "메인";
                    case 3:
                        return "일시정지";
                    case 4:
                        return "리셋";
                    case 5:
                        return "메인";
                    case 6:
                        return "게임 오버";
                    case 7:
                        return "리셋";
                    case 8:
                        return "메인";
                    case 9:
                        return "게임 클리어!\n축하드립니다!";
                }
        }
    }
}
