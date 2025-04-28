using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public LayerMask enemyLayer;

    public float weaponRange;




    //public Animator anim;

    public float cooldown = 2;

    private float timer;

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
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            Debug.Log(enemies[0].gameObject.name);
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
            //enemies[0].GetComponent<EnemyMovement>().Knockback(transform, StatsManager.Instance.knockBackForce, StatsManager.Instance.stunTime, StatsManager.Instance.knockBackTime);

        }
    }

    public void FinishAttacking()
    {
       // anim.SetBool("IsAttacking", false);

    }
    /*
    private void OnDrawGizmosSelected()
    {
        //weaponRange = StatsManager.Instance.weaponRange;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }*/
}
