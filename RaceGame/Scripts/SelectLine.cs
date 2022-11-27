using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLine : MonoBehaviour
{
    public int num;
    public CarSelect carSelect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("CarObject"))
        {
            carSelect.SetFlag(num);
        }
    }
}
