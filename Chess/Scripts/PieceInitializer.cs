using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PieceManager[] pieces = gameObject.GetComponentsInChildren<PieceManager>();
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Initialize();
        }
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].MoveRule();
        }
    }
}
