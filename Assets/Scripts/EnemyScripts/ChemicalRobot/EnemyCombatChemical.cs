using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatChemical : MonoBehaviour
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
    public Transform launchPoint1;
    public Transform launchPoint2;
    public Transform launchPoint3;
    public Transform launchPoint4;

    public GameObject projectilePrefab;
    public bool targetPlayer;

    public float shootCooldown = 2f;
    private float shootTimer;
    private Vector2 aimDirection = Vector2.right;
    private EnemyMovementChemical enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementChemical>();
    }
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
        float hori = enemyMovement.facing.x;
        float vert = enemyMovement.facing.y;
       // Debug.Log(enemyMovement.facing.x + " : " + enemyMovement.facing.y);
        //instantiate acid floor object
        if (Mathf.Abs(hori) < Mathf.Abs(vert))
        {
            AcidFloor acidFloor1 = Instantiate(projectilePrefab, launchPoint1.position, Quaternion.identity).GetComponent<AcidFloor>();
            AcidFloor acidFloor2 = Instantiate(projectilePrefab, launchPoint2.position, Quaternion.identity).GetComponent<AcidFloor>();
        }
        else if (Mathf.Abs(vert) < Mathf.Abs(hori))
        {
            AcidFloor acidFloor3 = Instantiate(projectilePrefab, launchPoint3.position, Quaternion.identity).GetComponent<AcidFloor>();
            AcidFloor acidFloor4 = Instantiate(projectilePrefab, launchPoint4.position, Quaternion.identity).GetComponent<AcidFloor>();
        }


          //  Debug.Log("Acid");
    }


    private void HandleAiming(Vector2 direction)
    {
        if (targetPlayer == true)
        { 
            aimDirection = direction; 
        }
        

    }
    public void Shoot(Vector2 direction)
    {

       // Projectile projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity).GetComponent<Projectile>();
        //projectile.direction = direction.normalized;
       // shootTimer = shootCooldown;
    }
}
