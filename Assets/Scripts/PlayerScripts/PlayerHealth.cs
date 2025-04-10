using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //public TMP_Text healthText;
    //public Animator healthTextAnim;
    public PlayerController playerController;
    public float invulnTimer;
    private float timer;


    private void Start()
    {
        //healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
           // Debug.Log(timer);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (((playerController.isRolling == false || amount > 0) && timer <= 0))
        {
            
            StatsManager.Instance.currentHealth += amount;
            timer = invulnTimer;
           // Debug.Log("Timer");
        }
        else
        {
            //Debug.Log("inv");
        }
        
        //healthTextAnim.Play("TextUpdate");
        //healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;

        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }
        else if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }


    }
}
