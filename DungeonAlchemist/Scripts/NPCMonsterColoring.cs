using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMonsterColoring : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[7];
    public SkinnedMeshRenderer[] monsterColor;

    // Start is called before the first frame update
    void Start()
    {
        weapons[Random.Range(0, 7)].SetActive(true);
        for (int i = 0; i < monsterColor.Length; i++)
        {
            monsterColor[i].material.color = Coloring();
        }
    }

    /// <summary>
    /// 색을 반환하는 함수
    /// </summary>
    Color Coloring()
    {
        Color color = Color.black;
        int random = Random.Range(0, 9);
        switch (random)
        {
            case 0:
                color = Color.red;
                break;
            case 1:
                color = Color.green;
                break;
            case 2:
                color = Color.blue;
                break;
            case 3:
                color = Color.magenta;
                break;
            case 4:
                color = Color.yellow;
                break;
            case 5:
                color = Color.white;
                break;
            case 6:
                color = Color.cyan;
                break;
            case 7:
                color = Color.gray;
                break;
        }
        return color;
    }
}
