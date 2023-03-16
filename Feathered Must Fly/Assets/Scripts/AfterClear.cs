using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterClear : MonoBehaviour
{
    public GameObject door, house;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Stage") == 8)
        {
            door.transform.localEulerAngles = new Vector3(0, 120, 0);
            house.SetActive(true);
        }
    }
}
