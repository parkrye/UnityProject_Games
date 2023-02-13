using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public PotionSellerManager potionSellerManager;

    public Transform[] startPoints = new Transform[3];
    public Transform[] goalPoints = new Transform[3];
    public GameObject[] npcPrefabs = new GameObject[5];
    public Transform buyPoint;

    public float spawnTerm;
    float randomSpeed;
    int randomNPC, randomStart, randomGoal;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        while (true)
        {
            randomNPC = Random.Range(0, 5);
            randomStart = Random.Range(0, 3);
            randomGoal = Random.Range(0, 3);
            randomSpeed = Random.Range(1.0f, 4.0f);

            GameObject npc = Instantiate(npcPrefabs[randomNPC], startPoints[randomStart].position, startPoints[randomStart].rotation);
            npc.GetComponent<NPCBehavior>().Setting(potionSellerManager, randomSpeed, goalPoints[randomGoal]);
            npc.GetComponent<NPCBehavior>().BeCustomer();

            yield return new WaitForSeconds(spawnTerm);
        }
    }
}
