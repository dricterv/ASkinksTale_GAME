using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    public float lifeSpan = 2;
    public float initialTimer;
    private float timer;
    public float speed;
    public int damage;
    public float knockBackForce;
    public float stunTime;
    public float knockBackTime;
    public bool spawner = false;
    public GameObject preFab;
    public bool spinning = false;
    public float spinSpeed;
    public Transform spawnPoint;
    public bool playerOwned = false;
    public bool isExplosive = false;
    public float explosionRadius;
    public int turnAngle = 90;
    public float duration;
    private float timerTwo;


    public LayerMask playerLayer;

    void Start()
    {
        
        timer = initialTimer;
        rb.velocity = direction * speed;

        RotateProjectile();
        Destroy(gameObject, lifeSpan);

    }

   
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }    
        if (spinning == true)
        {
            // transform.rotation.z = transform.rotation.z * spinSpeed * Time.deltaTime;
        }
      
    }

    private void RotateProjectile()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + turnAngle));
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.tag);
        // if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        /*if(coll.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }*/
        if (coll.gameObject.tag == "Player" && playerOwned == false && isExplosive == false)
        {

            Transform player = coll.transform;
            Vector2 direction = (coll.gameObject.transform.position - transform.position).normalized;
            //Debug.Log(direction);

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
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
                }
            }
            else
            {
                coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }


            //coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            //collision.gameObject.GetComponent<PlayerMovement>().Knockback(transform, knockBackForce, stunTime, knockBackTime);
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Enemy" && playerOwned == true)
        {
            coll.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damage);
        }
        else if (timer <= 0 && isExplosive == false)
        {

            if (preFab != null)
            {

                Instantiate(preFab, spawnPoint.position, new Quaternion(0, 0, 0, 0));
            }
            if (coll.gameObject.tag == "Player" && playerOwned == true)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //Destroy(gameObject);
    }

    public void Land()
    {
        rb.velocity = Vector2.zero;
    }
    public void BlowUp()
    {
        
        Collider2D coll = Physics2D.OverlapCircle(this.transform.position, explosionRadius, playerLayer);
        if (coll != null)
        {
            
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
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
                }
            }
            else
            {
                coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
        }
    }
}
