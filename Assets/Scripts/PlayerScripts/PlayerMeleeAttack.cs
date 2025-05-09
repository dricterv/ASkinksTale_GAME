using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public enum Direction { Down, Left, Right };
    public Direction attackDirection;

    public TermiteQueenCombat terQueCombat;


    public void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.tag);
        if(coll.gameObject.tag == "Player")
        {
            Transform player = coll.transform;
            Vector2 direction = (coll.gameObject.transform.position - transform.position).normalized;
            // Debug.Log(direction);

            if (StatsManager.Instance.blocking == true)
            {
                if (StatsManager.Instance.lockFacing == new Vector2(1, 0) && attackDirection == Direction.Left)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(-1, 0) && attackDirection == Direction.Right)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else if (StatsManager.Instance.lockFacing == new Vector2(0, 1) && attackDirection == Direction.Down)
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(0);
                }
                else
                {
                    coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(terQueCombat.damage);

                }
            }
            else
            {
                coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(terQueCombat.damage);
            }

        }
    }
}
