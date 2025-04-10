using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyState enemyState;
   // private Animator anim;

    private Rigidbody2D rb;
    private Transform player;
    private float attackCoolDownTimer;
    public Vector2 facing = new Vector2();
    public GameObject attackPoint;
    public float waitTime;
    public float atkTime;


    public float attackRange = 2;
    public float attackCoolDown = 2;
    public float speed;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private EnemyCombat enemyCombat;


    private bool isKnockedBack;

    public bool isDirectional;
    public bool cardinalMovement;
    public bool chaser;
    public bool rangedAttacker;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
        facing = new Vector2(0, -1);
        attackPoint.transform.localPosition = StatsManager.Instance.facing;
    }

    // Update is called once per frameif (isKnockedBack == false)
    void Update()
    {
        if (enemyState != EnemyState.KnockedBack)
        {
            CheckForPlayer();
            Debug.Log(enemyState);
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Chasing && enemyState != EnemyState.KnockedBack)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking && enemyState != EnemyState.KnockedBack)
            {
                rb.velocity = Vector2.zero;

            }
        }

       
    }

    private void CheckForPlayer()
    {
        //checks if player is in sight
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            //checks if player is in attack range and cd is ready
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCoolDownTimer <= 0)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                attackCoolDownTimer = attackCoolDown;
                ChangeState(EnemyState.Attacking);
                //Debug.Log("atack");
                
                StartCoroutine(AttackCD(atkTime, waitTime, direction));
            }

            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
                //Debug.Log("chase");

            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }


    public void Chase()
    {

        

        Vector2 direction = (player.position - transform.position).normalized;
        float hori = direction.x;
        float vert = direction.y;

        if ((Mathf.Abs(hori) > Mathf.Abs(vert)) && isDirectional == true)
        {
           // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
            facing = new Vector2(hori, 0).normalized;
            attackPoint.transform.localPosition = facing;
            //Debug.Log(facing);
            if (cardinalMovement == true)
            {
                rb.velocity = facing * speed;
            }
            else
            {
                rb.velocity = direction * speed;
            }
        }
        else if ((Mathf.Abs(vert) > Mathf.Abs(hori))&& isDirectional == true)
        {
            //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
            facing = new Vector2(0, vert).normalized;
            attackPoint.transform.localPosition = facing;
            //Debug.Log("v: " + vert);
            //Debug.Log(facing);
            if (cardinalMovement == true)
            {
                rb.velocity = facing * speed;
            }
            else
            {
                rb.velocity = direction * speed;
            }


        }
        else if (isDirectional == false)
        {
            attackPoint.transform.localPosition = direction;
            rb.velocity = direction * speed;

        }




    }


    void ChangeState(EnemyState newState)
    {
        /*exit current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("IsIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("IsChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("IsAttacking", false);
        }
        */
        //update current state

        enemyState = newState;

        /*enter current animation

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("IsIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("IsChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("IsAttacking", true);
        }
        */
    }



    public void Knockback(Transform player, float knockBackDist, float stunTime, float knockBackTime)
    {

        ChangeState(EnemyState.KnockedBack);
        // Debug.Log(transform.position - enemy.position);
        Vector2 direction = (transform.position - player.position).normalized;
        //Debug.Log(knockBackDist);
        // Debug.Log(direction);
        //rb.AddForce(direction * knockBackDist, ForceMode2D.Impulse);
        rb.velocity = direction * knockBackDist;
        // Debug.Log(rb.velocity);
        StartCoroutine(KnockbackCounter(stunTime, knockBackTime));

    }

    IEnumerator KnockbackCounter(float stunTime, float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        ChangeState(EnemyState.Idle);
    }

    //temp until animations are in
    IEnumerator AttackCD(float atkTime, float wait, Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(atkTime);
        if (rangedAttacker == false)
        {
            enemyCombat.Attack();
        }
        else if (rangedAttacker == true)
        {
           // enemyCombat.HandleAiming(direction);
            enemyCombat.Shoot(direction);
        }
        yield return new WaitForSeconds(wait);


        ChangeState(EnemyState.Idle);
    }
}



public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    KnockedBack,
}