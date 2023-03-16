using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableSelect : MonoBehaviour
{
    public GameObject[] movables;
    new Rigidbody rigidbody;
    public float power;
    int num = -1;

    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(0, movables.Length);
        movables[num].SetActive(true);   
        rigidbody = movables[num].GetComponent<Rigidbody>();
        StartCoroutine(Remover());
    }

    void FixedUpdate()
    {
        if(num != -1) rigidbody.AddForce(movables[num].transform.forward * power, ForceMode.Acceleration);
    }

    IEnumerator Remover()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
