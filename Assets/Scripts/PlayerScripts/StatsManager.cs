using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public EquippedItem equippedItemOne;
    public EquippedItem equippedItemTwo;
    public TMP_Text healthText;
    public Vector2 facing = new Vector2();
    public Vector2 lockFacing = new Vector2();
    public bool lockFace = false;
    public bool lockHori;
    public bool lockVert;
    public bool canGrabMove;
    public bool isMoving;
    public bool blocking;
    public GameObject grabbedGO;
    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float attackCD;
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

    private void Start()
    {

    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void UpdateEquipedItemOne(EquippedItem newItem)
    {
        if(newItem == equippedItemTwo)
        {
            equippedItemTwo = equippedItemOne;
        }
        equippedItemOne = newItem;
        Debug.Log("Equiped Item 1: " + equippedItemOne);
        Debug.Log("Equiped Item 2: " + equippedItemTwo);
    }
    public void UpdateEquipedItemTwo(EquippedItem newItem)
    {
        if (newItem == equippedItemOne)
        {
            equippedItemOne = equippedItemTwo;
        }
        equippedItemTwo = newItem;
        Debug.Log("Equiped Item 1: " + equippedItemOne);
        Debug.Log("Equiped Item 2: " + equippedItemTwo);

    }
}

public enum EquippedItem
{
    BaseFork,
    BaseShield,
    MatchStick,


}
