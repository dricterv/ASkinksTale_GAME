using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermiteQueenCombat : MonoBehaviour
{
    private EnemyState enemyState;
    private Animator anim;
    private bool starting = false;
    // public enum Direction { Down, Left, Right };
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
    public int damage;
    public float playerDetectRange = 20;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public float attackRange = 2;
    public float attackCoolDown = 2;
    private float attackCoolDownTimer;
    public Vector2 facing = new Vector2();
    private Transform player;
    private bool hasLineOfSight = false;

    public GameObject projectilePrefab;
    private Vector3 launchPoint;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    public Transform launchPointDown;
    public MeleeDamage meleeDown;
    public MeleeDamage meleeLeft;
    public MeleeDamage meleeRight;







    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        starting = true;
        attackCoolDownTimer = attackCoolDown;
        facing = new Vector2(0, -1);
        anim.SetFloat("xFacing", facing.x);
        anim.SetFloat("yFacing", facing.y);
        ChangeState(EnemyState.Idle);

    }


    // Update is called once per frame
    void Update()
    {
        if(starting == true)
        {
            CheckForPlayer();
           // Debug.Log(enemyState);
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }
            if (enemyState == EnemyState.Attacking)
            {

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

            Vector2 direction = (player.position - detectionPoint.position).normalized;
            float hori = direction.x;
            float vert = direction.y;
            //Debug.Log(Vector2.Distance(transform.position, player.position));
            if (Mathf.Abs(hori) > Mathf.Abs(vert))
            {
                // Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
                facing = new Vector2(hori, 0).normalized;

                anim.SetFloat("xFacing", facing.x);
                anim.SetFloat("yFacing", facing.y);
               // Debug.Log(facing);

            }
            else if (Mathf.Abs(vert) > Mathf.Abs(hori))
            {
                //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
                facing = new Vector2(0, vert).normalized;
                anim.SetFloat("xFacing", facing.x);
                anim.SetFloat("yFacing", facing.y);

                //Debug.Log("v: " + vert);
               // Debug.Log(facing);



            }

            RaycastHit2D cast = Physics2D.Raycast(detectionPoint.position, player.transform.position - detectionPoint.position);
            if (cast.collider != null)
            {
                hasLineOfSight = cast.collider.CompareTag("Player");
               // Debug.Log(cast.collider.gameObject.name);
                if (hasLineOfSight == true)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                    
                    //checks if player is in attack range and attack cd is ready
                    if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCoolDownTimer <= 0 && enemyState != EnemyState.AttackingTwo)
                    {
                        attackCoolDownTimer = attackCoolDown;
                        ChangeState(EnemyState.Attacking);
                        Debug.Log("atack");

                        
                    }
                   
                    else if (Vector2.Distance(transform.position, player.position) > attackRange && attackCoolDownTimer <= 0 && enemyState != EnemyState.Attacking)
                    {
                        ChangeState(EnemyState.AttackingTwo);
                        attackCoolDownTimer = attackCoolDown;
                    }
                }
                else
                {
                    Debug.DrawRay(detectionPoint.position, player.transform.position - detectionPoint.position, Color.red);
                    //rb.velocity = Vector2.zero;
                    // ChangeState(EnemyState.Idle);
                    //ChangeState(EnemyState.AttackingThree);
                }
            }
            else
            {
                //rb.velocity = Vector2.zero;
                // ChangeState(EnemyState.Idle);
                ChangeState(EnemyState.Idle);
            }
        }
        else
        {
            //rb.velocity = Vector2.zero;
            // ChangeState(EnemyState.Idle);
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        //exit current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.AttackingTwo)
        {
            anim.SetBool("isAttackingTwo", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (enemyState == EnemyState.AttackingThree)
        {
            anim.SetBool("isAttackingThree", false);
        }

        //update current state

        enemyState = newState;

        //enter current animation

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.AttackingTwo)
        {
            anim.SetBool("isAttackingTwo", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (enemyState == EnemyState.AttackingThree)
        {
            anim.SetBool("isAttackingThree", true);
        }

    }
    public void AttackEnd()
    {
        ChangeState(EnemyState.Idle);
    }
    /*
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Transform player = coll.transform;
            Vector2 direction = (coll.gameObject.transform.position - detectionPoint.position).normalized;
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

    }*/

    public void Shoot()
    {
        if (facing == new Vector2(-1, 0))
        {
            launchPoint = launchPointLeft.position;

        }
        else if (facing == new Vector2(1, 0))
        {
            launchPoint = launchPointRight.position;
        }
        else if (facing == new Vector2(0, -1))
        {
            launchPoint = launchPointDown.position;
        }
        Vector2 direction = player.position - launchPoint;
        Projectile projectile = Instantiate(projectilePrefab, launchPoint, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = direction.normalized;
        // shootTimer = shootCooldown;
    }

    public void SlashAnimation(Direction direction)
    {
        if(direction == Direction.Down)
        {
            meleeDown.SlashAniStart();
        }
        else if (direction == Direction.Left)
        {
            meleeLeft.SlashAniStart();
        }
        else if (direction == Direction.Right)
        {
            meleeRight.SlashAniStart();
        }
    }
    public void SlashAnimationEnd(Direction direction)
    {
        if (direction == Direction.Down)
        {
            meleeDown.SlashAniEnd();
        }
        else if (direction == Direction.Left)
        {
            meleeLeft.SlashAniEnd();
        }
        else if (direction == Direction.Right)
        {
            meleeRight.SlashAniEnd();
        }
    }


}
