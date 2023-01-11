using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager manager = null;

    void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DataManager Manager
    {
        get
        {
            if(manager == null)
            {
                return null;
            }
            return manager;
        }
    }

    public GameObject menuCanvas;
    int open;
    bool onMenu;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Open"))
        {
            PlayerPrefs.SetInt("Open", 0);
        }
        open = PlayerPrefs.GetInt("Open");
        onMenu = false;
    }

    public void SetOpen(int o)
    {
        open = o;
        PlayerPrefs.SetInt("Open", open);
    }

    public int GetOpen()
    {
        return open;
    }

    public void SetMenu(bool m)
    {
        onMenu = m;
    }

    public bool GetMenu()
    {
        return onMenu;
    }

    public void OpenManu()
    {
        SetMenu(true);
        Instantiate(menuCanvas);
    }
}
