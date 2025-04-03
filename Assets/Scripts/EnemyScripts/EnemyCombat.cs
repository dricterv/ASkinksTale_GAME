using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;
    public float knockBackForce;
    public float stunTime;

    private void OnCollisionEnter2D(Collision2D coll)
    {
         if (coll.gameObject.tag == "Player")
         {
             coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
         } 
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
           // hits[0].GetComponent<Player>().Knockback(transform, knockBackForce, stunTime);
        }
    }

   
}
