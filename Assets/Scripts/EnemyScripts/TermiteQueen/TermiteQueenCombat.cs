using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermiteQueenCombat : MonoBehaviour
{
    private bool updat = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        updat = false;
    }


// Update is called once per frame
    void Update()
    {
        if (updat == false)
        {
            Debug.Log("upda");
            updat = true;
        }
    }

}
