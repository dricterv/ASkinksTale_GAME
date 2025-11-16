using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCombat : MonoBehaviour
{

    private Transform player;
    public float playerDetectRange;
    public LayerMask playerLayer;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    public Vector2 facing;
    [Header("Projectile")]
    public GameObject projectilePrefab;
    private float timer;
    private Animator anim;
    private Vector2 direction;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    public void RangedAttack()
    {
        Transform t = launchPointLeft;
       
        
        
       
        if (facing == new Vector2(-1, 0))
        {
            t = launchPointLeft;

        }
        else if (facing == new Vector2(1, 0))
        {
            t = launchPointRight;
        }
        else if (facing == new Vector2(0, 0))
        {
            t = launchPointRight;
        }
      
        // enemyCombat.HandleAiming(direction);
        Shoot(direction, t);
    }


    public void StartAttack()
    {
        direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
        float hori = direction.x;
        if(hori >= 0)
        {
            facing = new Vector2(1, 0);
            anim.Play("AttackLeft");
        }
        else if(hori < 0)
        {
            facing = new Vector2(-1, 0);
            anim.Play("AttackRight");

        }
    }
    public void EndAttack()
    {
        anim.Play("Idle");
        
    }
    public void Shoot(Vector2 direction, Transform t)
    {
        //Debug.Log(direction);
        
        Projectile projectile = Instantiate(projectilePrefab, t.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = direction.normalized;
        // shootTimer = shootCooldown;
    }
}
