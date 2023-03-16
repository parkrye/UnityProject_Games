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
                                return "����";
                            case 1:
                                return "�츮";
                            case 2:
                                return "����";
                            case 3:
                                return "����";
                            case 4:
                                return "����";
                            case 5:
                                return "��Ǯ";
                            case 6:
                                return "����";
                            case 7:
                                return "���";
                        }
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "�ϴ� ����� ������ �� ����";
                            case 1:
                                return "�Ƹ��� ����� �츮�� �� ����";
                            case 2:
                                return "���� ����� ������ �� ����";
                            case 3:
                                return "�׷����� ����� ������ �� ����";
                            case 4:
                                return "�׷��� ����� ������ �� ����";
                            case 5:
                                return "��¼�� ����� ��Ǯ�� �� ����";
                            case 6:
                                return "�ƹ�ư ����� ������ �� ����";
                            case 7:
                                return "����� ����̴�";
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
                        return "������?";
                    case 1:
                        return "����";
                    case 2:
                        return "���";
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
                        return "������?";
                    case 1:
                        return "����";
                    case 2:
                        return "���";
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
                        return "����";
                    case 1:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "����";
                            case 1:
                                return "�θ�";
                            case 2:
                                return "�� ��";
                            case 3:
                                return "���� ��";
                            case 4:
                                return "��";
                            case 5:
                                return "����";
                            case 6:
                                return "�� ����";
                            case 7:
                                return "���� ����";
                            case 8:
                                return "����";
                        }
                    case 2:
                        return "��";
                    case 3:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "������";
                            case 1:
                                return "�Ķ���";
                            case 2:
                                return "�ϴû�";
                            case 3:
                                return "Ȳ����";
                            case 4:
                                return "Ȳ���";
                            case 5:
                                return "�ʷϻ�";
                            case 6:
                                return "�����";
                            case 7:
                                return "��ȸ��";
                            case 8:
                                return "��ȸ��";
                            case 9:
                                return "�����";
                            case 10:
                                return "ûȸ��";
                            case 11:
                                return "������";
                            case 12:
                                return "��ȫ��";
                            case 13:
                                return "�Ͼ��";
                            case 14:
                                return "�����";
                            case 15:
                                return "��ȫ��";
                        }
                    case 4:
                        return "��ĥ�ϱ�!";
                    case 5:
                        return "�Ϸ�";
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
                        return "�� ����";
                    case 1:
                        return "���� ����";
                    case 2:
                        return "����";
                    case 3:
                        return "����";
                    case 4:
                        return "�ں���";
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
                        return "����";
                    case 1:
                        return "ä���ϱ�";
                    case 2:
                        return "���� ����";
                    case 3:
                        return "���� ����";
                    case 4:
                        return "���� ����";
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
                        return "����";
                    case 1:
                        return "�����";
                    case 2:
                        return "ȿ����";
                    case 3:
                        return "���";
                    case 4:
                        switch (num)
                        {
                            default:
                            case 0:
                                return "����";
                            case 1:
                                return "�ѱ���";
                        }
                    case 5:
                        return "�Ϸ�";
                    case 6:
                        return "�ʱ�ȭ";
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
                        return "�������� Ŭ����!";
                    case 1:
                        return "����";
                    case 2:
                        return "����";
                    case 3:
                        return "�Ͻ�����";
                    case 4:
                        return "����";
                    case 5:
                        return "����";
                    case 6:
                        return "���� ����";
                    case 7:
                        return "����";
                    case 8:
                        return "����";
                    case 9:
                        return "���� Ŭ����!\n���ϵ帳�ϴ�!";
                }
        }
    }
}
