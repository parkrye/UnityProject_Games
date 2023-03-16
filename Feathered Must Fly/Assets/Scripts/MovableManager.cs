using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableManager : MonoBehaviour
{
    public GameObject movable;
    public StageClear stageClear;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMovable());
    }

    IEnumerator CreateMovable()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            GameObject tmp = Instantiate(movable, transform.position, transform.rotation);
            tmp.GetComponent<GameOverManager>().SetStageClear(stageClear);
        }
    }
}
