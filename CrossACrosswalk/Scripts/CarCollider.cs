using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    public GameObject blood;
    Car car;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponentInParent<Car>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            blood.SetActive(true);
            car.Crash();
        }
    }
}
