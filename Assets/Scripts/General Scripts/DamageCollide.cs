using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(1);
        }
    }
}
