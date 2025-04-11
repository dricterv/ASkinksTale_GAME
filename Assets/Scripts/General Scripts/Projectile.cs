using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpan = 2;
    public float initialTimer;
    private float timer;
    public float speed;
    public int damage;
    public float knockBackForce;
    public float stunTime;
    public float knockBackTime;

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
        timer -= Time.deltaTime;
    }

    private void RotateProjectile()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.tag);
       // if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        /*if(coll.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }*/
        if(coll.gameObject.tag == "Player")
        {
            
            /*Transform player = collision.transform;
            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
            if (StatsManager.Instance.blocking == true && (direction == -StatsManager.Instance.lockFacing))
            {
                collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
            */
            
            coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            //collision.gameObject.GetComponent<PlayerMovement>().Knockback(transform, knockBackForce, stunTime, knockBackTime);
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        else if(timer <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
