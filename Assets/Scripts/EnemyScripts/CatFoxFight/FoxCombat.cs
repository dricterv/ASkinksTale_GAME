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
    
    public Vector2 facing;
    private bool attackPlayer = false;
    [Header("Projectile")]
    public GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            if (attackPlayer == false)
            {
                RangedAttack();
                attackPlayer = true;
            }

        }

    }
 
    public void RangedAttack()
    {
        Transform t = launchPointLeft;

        Vector2 direction = (player.position - transform.position).normalized;
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
        else if (facing == new Vector2(0, 0))
        {
            t = launchPointRight;
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
