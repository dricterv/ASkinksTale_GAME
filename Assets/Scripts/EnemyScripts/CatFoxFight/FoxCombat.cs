using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCombat : MonoBehaviour
{
    private Transform player;
    public float playerDetectRange;
    public LayerMask playerLayer;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    public Transform launchPointUp;
    private Animator anim;

    public GameObject leftBush;
    public GameObject rightBush;
    public GameObject bottomBush;



    public Vector2 facing;
    [Header("Projectile")]
    public GameObject projectilePrefab;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

        }
      
        if (timer <= 0)
        {
            /*(Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, playerDetectRange, playerLayer);
            if (hits.Length > 0)
            {
                player = hits[0].transform;

                RangedAttack();
                timer = 2;


            }*/
            //ChangeBush;
        }

    }

    public void ChangeBush()
    {
        int bush = Random.Range(1, 4);
        if(bush == 1)
        {
            gameObject.transform.position = leftBush.transform.position;
            gameObject.transform.position += new Vector3(2, 0, 0);
            leftBush.GetComponent<Animator>().Play("BushTailWaggle");
            anim.Play("isInvisible");
            anim.SetFloat("xFacing", 1);
            anim.SetFloat("yFacing", 0);
            facing = new Vector2(1,0);


        }
        if (bush == 2)
        {
            gameObject.transform.position = rightBush.transform.position;
            gameObject.transform.position += new Vector3(-2, 0, 0);
            rightBush.GetComponent<Animator>().Play("BushTailWaggle");
            anim.Play("isInvisible");

            anim.SetFloat("xFacing", -1);
            anim.SetFloat("yFacing", 0);
            facing = new Vector2(-1, 0);


        }
        if (bush == 3)
        {
            gameObject.transform.position = bottomBush.transform.position;
            gameObject.transform.position += new Vector3(0, 2, 0);
            bottomBush.GetComponent<Animator>().Play("BushTailWaggle");
            anim.Play("isInvisible");

            anim.SetFloat("xFacing", 0);
            anim.SetFloat("yFacing", 1);
            facing = new Vector2(0, -1);



        }

    }
    public void StartAttack()
    {
        Debug.Log(facing);
        anim.Play("isAttacking");
        leftBush.GetComponent<Animator>().Play("BushIdle");
        rightBush.GetComponent<Animator>().Play("BushIdle");
        bottomBush.GetComponent<Animator>().Play("BushIdle");
    }
    public void StopAttack()
    {
        anim.Play("isIdle");
        leftBush.GetComponent<Animator>().Play("BushIdle");
        rightBush.GetComponent<Animator>().Play("BushIdle");
        bottomBush.GetComponent<Animator>().Play("BushIdle");

    }

    public void RangedAttack()
    {
        
        Transform t = transform;

        Vector2 direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
        float hori = direction.x;
        float vert = direction.y;

        if (facing == new Vector2(-1, 0))
        {
            t = launchPointLeft;

        }
        else if (facing == new Vector2(1, 0))
        {
            t = launchPointRight;
        }
        else if (facing == new Vector2(0, 1))
        {
            t = launchPointUp;
        }

        // enemyCombat.HandleAiming(direction);
        Shoot(direction, t);
    }

    public void Shoot(Vector2 direction, Transform t)
    {

        Projectile projectile = Instantiate(projectilePrefab, t.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = direction.normalized;
        // shootTimer = shootCooldown;
    }
}
