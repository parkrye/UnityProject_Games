using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarManager : MonoBehaviour
{
    public Transform[] startPoints;
    public GameObject[] carPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateCar());
    }

    IEnumerator CreateCar()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            Transform point = startPoints[Random.Range(0, startPoints.Length)];
            GameObject car = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], point.position, point.rotation);
        }
    }
}
