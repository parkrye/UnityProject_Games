using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public GameObject player;
    PlayerController controller;
    public GameObject carPoint;
    public Car[] cars;
    Car car;

    void Start()
    {
        controller = player.GetComponent<PlayerController>();
        cars = carPoint.GetComponentsInChildren<Car>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            controller.Stop(true);
            int random = Random.Range(0, cars.Length);
            car = cars[random];
            car.SetDest(player.transform);
        }
    }
}
