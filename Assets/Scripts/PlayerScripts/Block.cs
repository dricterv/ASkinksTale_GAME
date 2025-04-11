using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit");
        Debug.Log("Block: " + coll.gameObject.tag);

       // Destroy(coll.gameObject);
    }
}
