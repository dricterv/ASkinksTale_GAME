using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Base")]
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRadius;
    public LayerMask playerLayer;
    public float knockBackForce;
    public float stunTime;
    [Header("Projectile")]
    public Transform launchPoint;
    public GameObject projectilePrefab;
    public bool targetPlayer;

    public float shootCooldown = 2f;
    private float shootTimer;
    private Vector2 aimDirection = Vector2.right;


    void Update()
    {
       // shootTimer -= Time.deltaTime;

       /* HandleAiming();

        if (shootTimer <= 0)
        {
            Shoot();

        }*/
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
         if (coll.gameObject.tag == "Player")
         {
             coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
         } 
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRadius, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
           // hits[0].GetComponent<Player>().Knockback(transform, knockBackForce, stunTime);
        }
    }


    private void HandleAiming(Vector2 direction)
    {
        if (targetPlayer == true)
        { 
            aimDirection = direction; 
        }
        

    }
    public void Shoot(Vector2 direction)
    {

        Projectile projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = direction.normalized;
       // shootTimer = shootCooldown;
    }
}
