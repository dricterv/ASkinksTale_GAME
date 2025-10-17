using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Vector2 spawn;
    public SpriteRenderer enemySpriteRenderer;

    public List<RoomTransition> entry = new List<RoomTransition>();

    public List<RoomTransition> exit = new List<RoomTransition>();
    public NumberCounter counter;
    public bool destroyOnDeath = false;
    public bool deathAnim = false;
    private Animator anim;


    private void Start()
    {
        currentHealth = maxHealth;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemySpriteRenderer.color = Color.white;
        anim = GetComponent<Animator>();

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
                //Debug.Log("counter up");
            }
            if (entry.Count > 0)
            {
                //Debug.Log("en Count");
                for (int i = 0; i < entry.Count; i++)
                {
                    if (entry[i].enemiesEntry.Contains(this.gameObject))
                    {
                        entry[i].RemoveEntry(this.gameObject);
                    }
                }
            }
            if (exit.Count > 0)
            {
                //Debug.Log("ex Count");
                for (int i = 0; i < exit.Count; i++)
                {
                    if (exit[i].enemiesExit.Contains(this.gameObject))
                    {
                        exit[i].RemoveExit(this.gameObject);
                    }
                }
            }
            if (destroyOnDeath == true)
            {
                Destroy(gameObject);
                //gameObject.GetComponent<Animator>().Play("Dead");
            }
            else if (deathAnim == true)
            {
                if (anim != null)
                {
                    anim.Play("Death");
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
