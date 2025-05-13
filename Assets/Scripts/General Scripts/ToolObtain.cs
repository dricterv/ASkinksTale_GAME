using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObtain : MonoBehaviour
{
    public GameObject player;
    public string scriptName;
    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            (coll.gameObject.GetComponent(scriptName) as MonoBehaviour).enabled = true;
            Destroy(this.gameObject);
        }
    }

}
