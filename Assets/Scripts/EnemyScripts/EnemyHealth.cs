using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Vector2 spawn;
    public SpriteRenderer enemySpriteRenderer;
    public bool damageColour = true;

    public List<RoomTransition> entry = new List<RoomTransition>();

    public List<RoomTransition> exit = new List<RoomTransition>();
    public NumberCounter counter;
    private Animator anim;
    [Header("Death")]
    public bool destroyOnDeath = false;
    public bool deathAnim = false;
    public bool spawnHealth;
    public GameObject healthItem;




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
        if(amount < 0 && damageColour == true)
        {
            StartCoroutine(DamageColour());

        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
        else if (currentHealth <= 0)
        {
           Death();
        }
    }

    public void Death()
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
            if(spawnHealth == true)
            {
                if(StatsManager.Instance.currentHealth == 1)
                {
                    Debug.Log("hp0");
                    Debug.Log(StatsManager.Instance.currentHealth);
                    
                    Instantiate(healthItem, this.transform.position, healthItem.transform.rotation);
                        
                }
                else if(StatsManager.Instance.currentHealth < 5)
                {
                    Debug.Log("hp1");
                    Debug.Log(StatsManager.Instance.currentHealth);
                    int rnd = UnityEngine.Random.Range(1,100);
                    if(rnd > 50)
                    {
                        Instantiate(healthItem, this.transform.position, healthItem.transform.rotation);
                        
                    }
                }
                else if(StatsManager.Instance.currentHealth < (StatsManager.Instance.maxHealth/2))
                {
                    Debug.Log("hp2");
                    int rnd = UnityEngine.Random.Range(1,100);
                    if(rnd > 75)
                    {
                        Instantiate(healthItem, this.transform.position, healthItem.transform.rotation);
                        
                    }
                }
                else if(StatsManager.Instance.currentHealth < StatsManager.Instance.maxHealth)
                {
                    Debug.Log("hp3");
                    int rnd = UnityEngine.Random.Range(1,100);
                    if(rnd > 90)
                    {
                        Instantiate(healthItem, this.transform.position, healthItem.transform.rotation);
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
