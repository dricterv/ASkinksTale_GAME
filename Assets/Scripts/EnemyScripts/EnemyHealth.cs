using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Vector2 spawn;
    public RoomTransition entry;
    public RoomTransition exit;



    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
        else if (currentHealth <= 0)
        {
           // entry.RemoveEntry(this.gameObject);
            //exit.RemoveExit(this.gameObject);
            Destroy(gameObject);
        }
    }

}
