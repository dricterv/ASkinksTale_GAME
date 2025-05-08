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
    private Transform player;
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
            Transform player = coll.transform;
            Vector2 direction = (coll.gameObject.transform.position - transform.position).normalized;
           // Debug.Log(direction);

            if (StatsManager.Instance.blocking == true)
            {
                if (StatsManager.Instance.lockFacing == new Vector2(1, 0) && direction.x <= -StatsManager.Instance.blockAngle)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(-1, 0) && direction.x >= StatsManager.Instance.blockAngle)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(0, -1) && direction.y >= StatsManager.Instance.blockAngle)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(0, 1) && direction.y <= -StatsManager.Instance.blockAngle)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
                }
            }
            else
            {
                coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
            }
            
         } 

    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRadius, playerLayer);
        if (hits.Length > 0)
        {
            
            player = hits[0].transform;
            Vector2 direction = (attackPoint.position - transform.position).normalized;
            if (StatsManager.Instance.blocking == true)
            {
                if (StatsManager.Instance.lockFacing == new Vector2(1, 0) && direction.x <= -StatsManager.Instance.blockAngle)
                {
                    hits[0].GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(-1, 0) && direction.x >= StatsManager.Instance.blockAngle)
                {
                    hits[0].GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(0, -1) && direction.y >= StatsManager.Instance.blockAngle)
                {
                    hits[0].GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(0, 1) && direction.y <= -StatsManager.Instance.blockAngle)
                {
                    hits[0].GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else
                {
                    hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
                }

            }
            else
            {
                hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }

            // hits[0].GetComponent<Player>().Knockback(transform, knockBackForce, stunTime);
            //hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
    }


    private void HandleAiming(Vector2 direction)
    {
        if (targetPlayer == true)
        { 
            aimDirection = direction; 
        }
        

    }
    public void Shoot(Vector2 direction, Transform t)
    {

        Projectile projectile = Instantiate(projectilePrefab, t.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = direction.normalized;
       // shootTimer = shootCooldown;
    }
}
