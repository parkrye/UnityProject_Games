using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    GameObject player;
    RankSystem rankSystem;
    ItemSpawner itemSpawner;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rankSystem = player.GetComponent<RankSystem>();
        itemSpawner = player.GetComponent<ItemSpawner>();
        StartCoroutine(TimeOver());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarObject")
        {
            string name = gameObject.name;
            if (name == "Fuel")
            {
                rankSystem.LifeUp(10);
            }
            else if (name == "Bad")
            {
                rankSystem.LifeUp(-10);
            }
            else if (name == "Good")
            {
                rankSystem.ScoreUp(10);
            }

            itemSpawner.ItemUsed();
            Destroy(gameObject);
        }
    }

    IEnumerator TimeOver()
    {
        int count = 0;
        float dir = -0.05f;
        while(count < 50)
        {
            for(int i = 0; i < 40; i++)
            {
                gameObject.transform.Translate(0f, dir, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            count += 1;
            dir *= -1f;
        }
        itemSpawner.ItemUsed();
        Destroy(gameObject);
    }
}
