using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreater : MonoBehaviour
{
    public GameObject pole;
    public Material material;
    public int stairNum;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < stairNum; i++)
        {
            GameObject tmp = Instantiate(pole);

            tmp.transform.parent = transform;
            tmp.transform.Translate(0f, 0.5f * i, 0f);
            tmp.transform.Rotate(0f, 7f * i, 0f);

            if(i > 0)
            {
                if (i % 7 == 0) tmp.transform.localScale = new Vector3(tmp.transform.localScale.x, tmp.transform.localScale.y, tmp.transform.localScale.z * 1.02f);
                else if (i % 5 == 0) tmp.transform.localScale = new Vector3(tmp.transform.localScale.x, tmp.transform.localScale.y, tmp.transform.localScale.z * 0.98f);
                if (i % 23 == 0) tmp.transform.localScale = new Vector3(tmp.transform.localScale.x / 1.5f, tmp.transform.localScale.y, tmp.transform.localScale.z);
                else if (i % 11 == 0) tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * 1.5f, tmp.transform.localScale.y, tmp.transform.localScale.z);
                if (i % 41 == 0) tmp.SetActive(false);
                else if (i % 37 == 0) tmp.GetComponent<MeshRenderer>().material = material; 
            }
            if (i > 874) tmp.transform.localScale = new Vector3(tmp.transform.localScale.x, tmp.transform.localScale.y, tmp.transform.localScale.z * 2f);
        }
    }
}
