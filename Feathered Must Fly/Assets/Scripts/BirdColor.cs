using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdColor : MonoBehaviour
{
    private static BirdColor player = null;

    void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static BirdColor Player
    {
        get
        {
            if (player == null)
            {
                return null;
            }
            return player;
        }
    }

    public enum State { Black, Blue, BlueGreen, Brown, BrownYellow, Green, GreenYellow, Grey, GreyBlack, Purple, PurpleBlue, Red, RedPurple, White, Yellow, YellowRed };
    public State state;

    public Material[] materials = new Material[17];
    public MeshRenderer[] parts = new MeshRenderer[9];

    private void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);

        if (!PlayerPrefs.HasKey("Body")) PlayerPrefs.SetInt("Body", 14);
        else parts[0].material = materials[PlayerPrefs.GetInt("Body")];
        if (!PlayerPrefs.HasKey("Break")) PlayerPrefs.SetInt("Break", 15);
        else parts[1].material = materials[PlayerPrefs.GetInt("Break")];
        if (!PlayerPrefs.HasKey("EyeL")) PlayerPrefs.SetInt("EyeL", 0);
        else parts[2].material = materials[PlayerPrefs.GetInt("EyeL")];
        if (!PlayerPrefs.HasKey("EyeR")) PlayerPrefs.SetInt("EyeR", 0);
        else parts[3].material = materials[PlayerPrefs.GetInt("EyeR")];
        if (!PlayerPrefs.HasKey("Hat")) PlayerPrefs.SetInt("Hat", 12);
        else parts[4].material = materials[PlayerPrefs.GetInt("Hat")];
        if (!PlayerPrefs.HasKey("Tail")) PlayerPrefs.SetInt("Tail", 14);
        else parts[5].material = materials[PlayerPrefs.GetInt("Tail")];
        if (!PlayerPrefs.HasKey("WingL")) PlayerPrefs.SetInt("WingL", 14);
        else parts[6].material = materials[PlayerPrefs.GetInt("WingL")];
        if (!PlayerPrefs.HasKey("WingR")) PlayerPrefs.SetInt("WingR", 14);
        else parts[7].material = materials[PlayerPrefs.GetInt("WingR")];
        if (!PlayerPrefs.HasKey("Beard")) PlayerPrefs.SetInt("Beard", 12);
        else parts[8].material = materials[PlayerPrefs.GetInt("Beard")];
    }

    public void Coloring(int part, int color)
    {
        switch (part)
        {
            default:
            case 0: PlayerPrefs.SetInt("Body", color); break;
            case 1: PlayerPrefs.SetInt("Break", color); break;
            case 2: PlayerPrefs.SetInt("EyeL", color); break;
            case 3: PlayerPrefs.SetInt("EyeR", color); break;
            case 4: PlayerPrefs.SetInt("Hat", color); break;
            case 5: PlayerPrefs.SetInt("Tail", color); break;
            case 6: PlayerPrefs.SetInt("WingL", color); break;
            case 7: PlayerPrefs.SetInt("WingR", color); break;
            case 8: PlayerPrefs.SetInt("Beard", color); break;
        }
        parts[part].material = materials[color];
    }
}
