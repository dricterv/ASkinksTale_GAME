using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public enum Direction {Up, Down, Left, Right };
    public Direction attackDirection;
    private EnemyState enemyState;
    private Animator anim;
    private bool hasLineOfSight =  false;

    private Rigidbody2D rb;
    private Transform player;
    private float attackCoolDownTimer;
    public Vector2 facing = new Vector2();
    public GameObject attackPoint;
    public float atkWaitTime;
    public float atkTime;


    public float attackRange = 2;
    public float attackCoolDown = 2;
    public float speed;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private EnemyCombat enemyCombat;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    public Transform launchPointUp;
    public Transform launchPointDown;


    private bool isKnockedBack;

    public bool isDirectional;
    public bool cardinalMovement;
    public bool chaser;
    public bool rangedAttacker;

    public float changeDirMinTime;
    public float changeDirMaxTime;
    private float changeDirTimer;
    public float waitDirTime;
    private float waitDirTimer;
    private Vector2[] moveDirections = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector3.down };
    private int currentMoveDirection;
    public Vector2 patrolLimitMax;
    public Vector2 patrolLimitMin;

    public float patrolLimitX;
    public float patrolLimitY;
    public float patrolSpeed;


    public Transform patrolOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
        facing = new Vector2(0, -1);
        attackPoint.transform.localPosition = facing;
        changeDirTimer = changeDirMaxTime;
        waitDirTimer = waitDirTime;
    }
    
    // Update is called once per frameif (isKnockedBack == false)
    void Update()
    {
        if (enemyState != EnemyState.KnockedBack)
        {
            CheckForPlayer();
            // Debug.Log(enemyState);
            // Debug.Log("x: " + transform.localPosition.x);
            // Debug.Log("y: " + transform.localPosition.y);

            //transform.localPosition.y
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Chasing && enemyState != EnemyState.KnockedBack )
            {
                if (transform.position.x < patrolLimitMax.x && transform.position.x > patrolLimitMin.x && transform.position.y < patrolLimitMax.y && transform.position.y > patrolLimitMin.y)
                {
                    Chase();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
                
            }
            else if (enemyState == EnemyState.Attacking && enemyState != EnemyState.KnockedBack)
            {
                rb.velocity = Vector2.zero;

            }
            else if (enemyState == EnemyState.Patrolling && enemyState != EnemyState.KnockedBack)
            {
                if (changeDirTimer > 0)
                {
                    changeDirTimer -= Time.deltaTime;
                    Vector2 direc = moveDirections[currentMoveDirection];
                    if (transform.position.x > patrolLimitMax.x || transform.position.x < patrolLimitMin.x || transform.position.y > patrolLimitMax.y || transform.position.y < patrolLimitMin.y)
                    {
                        changeDirTimer = 0;

                       // direc = -moveDirections[currentMoveDirection];
                    }
                    Patrol(direc);
                    //rb.velocity += moveDirections[currentMoveDirection] * speed * Time.deltaTime;
                }
                else if (changeDirTimer <= 0)
                {
                    //rb.velocity = Vector2D.zero;
                    if (transform.position.x > patrolLimitMax.x || transform.position.x < patrolLimitMin.x || transform.position.y > patrolLimitMax.y || transform.position.y < patrolLimitMin.y)
                    {
                        //changeDirTimer = 0;
                        Vector2 direc = -moveDirections[currentMoveDirection];
                        Patrol(direc);
                    }
                    
                    else if (waitDirTimer > 0)
                    {
                        waitDirTimer -= Time.deltaTime;
                        Patrol(Vector2.zero);
                        ChangeState(EnemyState.Idle);

                    }
                    else if (waitDirTimer <= 0)
                    {
                        ChangeDirection();
                    }
                }


                //rb.velocity += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;
            }
        }


    }
 
    private void ChangeDirection()
    {
        int oldMoveDirection = currentMoveDirection;
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
        while(currentMoveDirection == oldMoveDirection)
        {
            currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
        }
        changeDirTimer = Random.Range(changeDirMinTime, changeDirMaxTime);
        waitDirTimer = waitDirTime;
    }

    private void CheckForPlayer()
    {
        //checks if player is in sight
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            RaycastHit2D cast = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
            if (cast.collider != null)
            {
                hasLineOfSight = cast.collider.CompareTag("Player");
                //Debug.Log(cast.collider.gameObject.name);
                if(hasLineOfSight == true)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                    //RaycastHit2D hit = Physics2D.Raycast(transform.position,  player.position - transform.position, playerDetectRange);
                    //if(hit.gameObject.tag == "Player")
                    //checks if player is in attack range and attack cd is ready
                    if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCoolDownTimer <= 0)
                    {
                        //Vector2 direction = (player.position - transform.position).normalized;
                        attackCoolDownTimer = attackCoolDown;
                        ChangeState(EnemyState.Attacking);
                       // Debug.Log("atack");

                       // StartCoroutine(AttackCD(atkTime, atkWaitTime));
                    }
                    /* else if (Mathf.Abs(this.transform.localPosition.x) >= (patrolLimitX - 1f) || Mathf.Abs(this.transform.localPosition.y) >= (patrolLimitY -1f))
                     {
                         rb.velocity = Vector2.zero;

                     }*/
                    else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking && (transform.position.x < patrolLimitMax.x && transform.position.x > patrolLimitMin.x && transform.position.y < patrolLimitMax.y && transform.position.y > patrolLimitMin.y))
                    {
                        ChangeState(EnemyState.Chasing);
                        //Debug.Log("chase");

                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    //rb.velocity = Vector2.zero;
                    // ChangeState(EnemyState.Idle);
                    ChangeState(EnemyState.Patrolling);
                }
            }
            else
            {
                //rb.velocity = Vector2.zero;
                // ChangeState(EnemyState.Idle);
                ChangeState(EnemyState.Patrolling);
            }
        }
        else
        {
            //rb.velocity = Vector2.zero;
            // ChangeState(EnemyState.Idle);
            ChangeState(EnemyState.Patrolling);
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
             changeDirTimer = 0;
            
        }

        // Debug.Log("bump");
    }

    public void Chase()
    {

       

        Vector2 direction = (player.position - transform.position).normalized;
        float hori = direction.x;
        float vert = direction.y;
        //Debug.Log(direction);
        if ((Mathf.Abs(hori) > Mathf.Abs(vert)) && isDirectional == true)
        {
            // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
            facing = new Vector2(hori, 0).normalized;
            attackPoint.transform.localPosition = facing;
            anim.SetFloat("xFacing", facing.x);
            anim.SetFloat("yFacing", facing.y);
           // Debug.Log(facing);
            if (cardinalMovement == true)
            {
                rb.velocity = facing * speed;
            }
            else
            {
                rb.velocity = direction * speed;
            }
        }
        else if ((Mathf.Abs(vert) > Mathf.Abs(hori)) && isDirectional == true)
        {
            //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
            facing = new Vector2(0, vert).normalized;
            anim.SetFloat("xFacing", facing.x);
            anim.SetFloat("yFacing", facing.y);
            attackPoint.transform.localPosition = facing;
            //Debug.Log("v: " + vert);
           // Debug.Log(facing);
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

    public void Patrol(Vector2 direction)
    { 
        Vector2 direc = direction;
        /*if(this.transform.localPosition.x >= patrolLimitX || this.transform.localPosition.y >= patrolLimitY)
        {
            direc = -direction;
        }*/
        float hori = direc.x;
        float vert = direc.y;
        if((Mathf.Abs(hori) == Mathf.Abs(vert)) && isDirectional == true)
        {
            if (cardinalMovement == true)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else if ((Mathf.Abs(hori) > Mathf.Abs(vert)) && isDirectional == true)
        {
            // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
            facing = new Vector2(hori, 0).normalized;
            attackPoint.transform.localPosition = facing;
            anim.SetFloat("xFacing", facing.x);
            anim.SetFloat("yFacing", facing.y);
            //Debug.Log(facing);
            if (cardinalMovement == true)
            {
                rb.velocity = facing * patrolSpeed;
            }
            else
            {
                rb.velocity = direction * patrolSpeed;
            }
        }
        else if ((Mathf.Abs(vert) > Mathf.Abs(hori)) && isDirectional == true)
        {
            //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
            facing = new Vector2(0, vert).normalized;
            attackPoint.transform.localPosition = facing;
            anim.SetFloat("xFacing", facing.x);
            anim.SetFloat("yFacing", facing.y);
            //Debug.Log("v: " + vert);
            //Debug.Log(facing);
            if (cardinalMovement == true)
            {
                rb.velocity = facing * patrolSpeed;
            }
            else
            {
                rb.velocity = direction * patrolSpeed;
            }


        }
        else if (isDirectional == false)
        {
            attackPoint.transform.localPosition = direction;
            rb.velocity = direction * patrolSpeed;

        }
    }


    


    void ChangeState(EnemyState newState)
    {
        //exit current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (enemyState == EnemyState.Patrolling)
        {
            anim.SetBool("isPatrolling", false);
        }

        //update current state

        enemyState = newState;

        //enter current animation

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (enemyState == EnemyState.Patrolling)
        {
            anim.SetBool("isPatrolling", true);
        }

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
    IEnumerator AttackCD(float atkTime, float wait)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(atkTime);
        if (rangedAttacker == false)
        {
            enemyCombat.Attack();
        }
        else if (rangedAttacker == true)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float hori = direction.x;
            float vert = direction.y;
            if ((Mathf.Abs(hori) > Mathf.Abs(vert)) && isDirectional == true)
            {
                // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
                facing = new Vector2(hori, 0).normalized;
                if(hori > 0)
                {
                    attackPoint.transform.localPosition = launchPointRight.position;
                }
                else if(hori < 0 )
                {
                    attackPoint.transform.localPosition = launchPointLeft.position;

                }

                //Debug.Log(facing);

            }
            else if ((Mathf.Abs(vert) > Mathf.Abs(hori)) && isDirectional == true)
            {
                //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
                facing = new Vector2(0, vert).normalized;
                if (vert > 0)
                {
                    attackPoint.transform.localPosition = launchPointUp.position;
                }
                else if (vert < 0)
                {
                    attackPoint.transform.localPosition = launchPointDown.position;

                }

                //Debug.Log("v: " + vert);
                //Debug.Log(facing);

            }
            // enemyCombat.HandleAiming(direction);
            //enemyCombat.Shoot(direction);
        }
        yield return new WaitForSeconds(wait);


        ChangeState(EnemyState.Idle);
    }
    public void MeleeAttack()
    {
        rb.velocity = Vector2.zero;

        enemyCombat.Attack();
    }
    public void RangedAttack(Direction attackDir)
    {
        Transform t = launchPointLeft;
        rb.velocity = Vector2.zero;
        Vector2 direction = (player.position - transform.position).normalized;
        float hori = direction.x;
        float vert = direction.y;
        if ((Mathf.Abs(hori) > Mathf.Abs(vert)) && isDirectional == true)
        {
            // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
            facing = new Vector2(hori, 0).normalized;
            if(attackDir == Direction.Left)
            {
               // t = launchPointLeft;
            }
            else if (attackDir == Direction.Right)
            {
                //t = launchPointRight;
            }

            //Debug.Log(facing);
            //enemyCombat.Shoot(direction, t);
        }
        else if ((Mathf.Abs(vert) > Mathf.Abs(hori)) && isDirectional == true)
        {
            //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
            facing = new Vector2(0, vert).normalized;

            if (attackDir == Direction.Up)
            {
               // t = launchPointUp;
            }
            else if (attackDir == Direction.Down)
            {
              //  t = launchPointDown;
            }
            //Debug.Log("v: " + vert);
            //Debug.Log(facing);
            //enemyCombat.Shoot(direction, t);
        }
        if (facing == new Vector2(-1, 0))
        {
            t = launchPointLeft;

        }
        else if (facing == new Vector2(1, 0))
        {
            t = launchPointRight;
        }
        else if (facing == new Vector2(0, -1))
        {
            t = launchPointDown;
        }
        else if (facing == new Vector2(0, 1))
        {
            t = launchPointUp;
        }
        // enemyCombat.HandleAiming(direction);
        enemyCombat.Shoot(direction, t);
    }

    public void EndAttack()
    {
        ChangeState(EnemyState.Idle);
    }


}




public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    KnockedBack,
    Patrolling,
    AttackingTwo,
    AttackingThree,
}