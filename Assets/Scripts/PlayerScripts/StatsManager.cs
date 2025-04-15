using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TMP_Text healthText;
    public Vector2 facing = new Vector2();
    public Vector2 lockFacing = new Vector2();
    public bool lockFace = false;
    public bool blocking;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockBackForce;
    public float stunTime;
    public float knockBackTime;
    public float blockAngle;

    [Header("Movement Stats")]
    public float speed;
    public float dragSpeed;
    public float knockBackSpeed;
    public float rollDist;
    public float rollTime;

    [Header("Health Stats")]
    public int currentHealth;
    public int maxHealth;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }
}
