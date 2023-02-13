using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHole : MonoBehaviour
{
    public MarketManager marketManager;
    public AudioSource disappearAudio;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Takable" || collision.gameObject.tag == "Potion")
        {
            disappearAudio.Play();
            marketManager.ModifyMoney(collision.gameObject.GetComponent<ProductManager>().GetPrice() / 2);
            Destroy(collision.gameObject);
        }
    }
}
