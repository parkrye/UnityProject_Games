using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSetting : MonoBehaviour
{
    new public Rigidbody rigidbody;
    public Material[] colors;
    public MeshRenderer[] meshes;
    public float power;

    void Start()
    {
        Material color = colors[Random.Range(0, colors.Length)];
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.material = color;
        }
    }

    void Update()
    {
        rigidbody.AddForce(transform.forward * power + Vector3.up, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
