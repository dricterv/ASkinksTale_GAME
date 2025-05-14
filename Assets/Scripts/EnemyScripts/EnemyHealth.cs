using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Vector2 spawn;
    public SpriteRenderer enemySpriteRenderer;

    public RoomTransition entry;
    public RoomTransition exit;
    public NumberCounter counter;


    private void Start()
    {
        currentHealth = maxHealth;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();

    }
    IEnumerator DamageColour()
    {
        // Debug.Log("Roll Timer S");
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.color = Color.white;


    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(amount < 0)
        {
            StartCoroutine(DamageColour());

        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
        else if (currentHealth <= 0)
        {
            if (counter != null)
            {
                counter.AddToCount(1);
                Debug.Log("counter up");
            }
            if (entry != null)
            {
             entry.RemoveEntry(this.gameObject);
            }
            if (exit != null)
            {
                exit.RemoveExit(this.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
