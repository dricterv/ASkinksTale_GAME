using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public LayerMask enemyLayer;

    public float weaponRange;
    



    public Animator anim;

    public float cooldown = 2;

    private float timer;
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        // Debug.Log("tried");
        if (timer <= 0)
        {
            //anim.SetBool("IsAttacking", true);
            timer = cooldown;
            DealDamage();
        }
    }
    public void OnTriggerEnter2D(Collider2D coll) 
    {
        //Debug.Log(coll.gameObject.name); 

        if(coll.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            if(coll.GetComponent<EnemyHealth>().currentHealth > 0)
            {
                coll.GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
            }
            if(coll.GetComponent<EnemyMovement>() != null)
            {
                coll.GetComponent<EnemyMovement>().Knockback(StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);

            }
            else if (coll.GetComponent<LarvaeMovement>() != null)
            {
                coll.GetComponent<LarvaeMovement>().Knockback(StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);
              
            }
            else if (coll.GetComponent<EnemyMovementChemical>() != null)
            {
                coll.GetComponent<EnemyMovementChemical>().Knockback(StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);

            }
            else if (coll.GetComponent<EnemyMovementCharger>() != null)
            {
                coll.GetComponent<EnemyMovementCharger>().Knockback(StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);

            }
        }
    }
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);
        Debug.Log("hit");
        if (enemies.Length > 0)
        {
            Debug.Log(enemies[0].gameObject.name);
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
            //enemies[0].GetComponent<EnemyMovement>().Knockback(transform, StatsManager.Instance.knockBackForce, StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);

        }
    }


   
    /*
    private void OnDrawGizmosSelected()
    {
        //weaponRange = StatsManager.Instance.weaponRange;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }*/
}
