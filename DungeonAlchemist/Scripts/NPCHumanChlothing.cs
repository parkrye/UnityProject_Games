using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHumanChlothing : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[7];

    public SkinnedMeshRenderer eyeBrows, eyeLashes, eyes, hair, beard, moustache;
    public SkinnedMeshRenderer[] body = new SkinnedMeshRenderer[7];
    public GameObject[] boots = new GameObject[2];
    public GameObject[] chests = new GameObject[2];
    public GameObject[] pants = new GameObject[2];
    public GameObject gloves, helmet, shoulders;

    // Start is called before the first frame update
    void Start()
    {
        weapons[Random.Range(0, 7)].SetActive(true);
        Painter();
        Remover();
    }

    void Painter()
    {
        eyeBrows.material.color = Coloring();
        eyeLashes.material.color = Coloring();
        eyes.material.color = Coloring();
        hair.material.color = Coloring();
        beard.material.color = Coloring();
        moustache.material.color = Coloring();

        Color bodyColor = Coloring();
        for(int i = 0; i < 7; i++)
        {
            body[i].material.color = bodyColor;
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

    /// <summary>
    /// 머리카락, 수염, 의상을 제거하는 함수
    /// </summary>
    void Remover()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            hair.enabled = false;
        }
        random = Random.Range(0, 2);
        if (random == 0)
        {
            beard.enabled = false;
        }
        random = Random.Range(0, 2);
        if (random == 0)
        {
            moustache.enabled = false;
        }

        random = Random.Range(0, 3);
        if (random == 0)
        {
            boots[0].SetActive(true);
        }
        else if(random == 1)
        {
            boots[1].SetActive(true);
        }
        random = Random.Range(0, 3);
        if (random == 0)
        {
            chests[0].SetActive(true);
        }
        else if (random == 1)
        {
            chests[1].SetActive(true);
        }
        random = Random.Range(0, 3);
        if (random == 0)
        {
            pants[0].SetActive(true);
        }
        else if (random == 1)
        {
            pants[1].SetActive(true);
        }
        random = Random.Range(0, 2);
        if (random == 0)
        {
            gloves.SetActive(true);
        }
        random = Random.Range(0, 2);
        if (random == 0)
        {
            helmet.SetActive(true);
        }
        random = Random.Range(0, 2);
        if (random == 0)
        {
            shoulders.SetActive(true);
        }
    }
}
