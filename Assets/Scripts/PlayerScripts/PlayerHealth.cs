using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public TMP_Text healthText;
    //public Animator healthTextAnim;
    public PlayerController playerController;
    public float invulnTimer;
    private float timer;
    public SpriteRenderer playerSpriteRenderer;


    private void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
           // Debug.Log(timer);
        }
    }
    IEnumerator DamageColour()
    {
        // Debug.Log("Roll Timer S");
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        playerSpriteRenderer.color = Color.white;


    }
    public void ChangeHealth(int amount)
    {
        if (((playerController.isRolling == false || amount < 0) && timer <= 0))
        {
            
            StatsManager.Instance.currentHealth += amount;
            timer = invulnTimer;
            Debug.Log("dmg: " + amount);
            OnPlayerDamaged?.Invoke();
            if (amount < 0 && StatsManager.Instance.currentHealth > 0)
            {
                StartCoroutine(DamageColour());
            }
        }
        else
        {
            if(amount > 0)
            {
                StatsManager.Instance.currentHealth += amount;
                OnPlayerDamaged?.Invoke();
            }
            
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
            GameManager.Instance.MainMenuLoad();
        }

        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;

    }
}
