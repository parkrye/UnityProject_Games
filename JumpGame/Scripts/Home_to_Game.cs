using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home_to_Game : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
