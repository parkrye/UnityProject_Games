using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    public GameObject before, after;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.up * 2f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 5f)
        {
            StartCoroutine(AutoBreak());
            before.SetActive(false);
            after.SetActive(true);
            after.transform.eulerAngles = collision.contacts[0].normal;
        }
    }

    IEnumerator AutoBreak()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
