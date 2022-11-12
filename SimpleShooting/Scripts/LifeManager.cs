using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static int life;
    public Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        lifeText.text = "LIFE : " + life.ToString();
    }

    private void FixedUpdate()
    {
        lifeText.text = "LIFE : " + life.ToString();
        if (life <= 0)
        {
            GameOverManager.GameOver();
        }
    }

    public static void AddLife(int diff)
    {
        life += diff;
    }

    public static int GetLife()
    {
        return life;
    }
}
