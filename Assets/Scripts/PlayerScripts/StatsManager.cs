using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [Header("Test")]
    public static StatsManager Instance;
    public InventoryItem equippedItemOne;
    public InventoryItem equippedItemTwo;
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


    [Header("SceneFlags")]
    public List<FlagDictionary> flagList = new List<FlagDictionary>();

    public Dictionary<string, bool> flags = new Dictionary<string, bool>();




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

        foreach (FlagDictionary entry in flagList)
        {
            flags.Add(entry.key, entry.value);
        }
    }

    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            foreach (FlagDictionary entry in flagList)
            {
                flags[entry.key] = entry.value;
            
            }
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void UpdateEquipedItemOne(InventoryItem newItem)
    {
        if(newItem == equippedItemTwo)
        {
            equippedItemTwo = equippedItemOne;
            GameManager.Instance.uiManager.item2.sprite = equippedItemTwo.itemImage;

        }
        equippedItemOne = newItem;
        GameManager.Instance.uiManager.item1.sprite = equippedItemOne.itemImage;

        // Debug.Log("Equiped Item 1: " + equippedItemOne);
        // Debug.Log("Equiped Item 2: " + equippedItemTwo);
    }
    public void UpdateEquipedItemTwo(InventoryItem newItem)
    {
        if (newItem == equippedItemOne)
        {
            equippedItemOne = equippedItemTwo;
            GameManager.Instance.uiManager.item1.sprite = equippedItemOne.itemImage;
        }
        equippedItemTwo = newItem;
        GameManager.Instance.uiManager.item2.sprite = newItem.itemImage;

        //Debug.Log("Equiped Item 1: " + equippedItemOne);
        // Debug.Log("Equiped Item 2: " + equippedItemTwo);

    }
}

[System.Serializable]
public class FlagDictionary
{
    public string key;
    public bool value;
}

public enum EquippedItem
{
    EmptyItem,
    BaseFork,
    BaseShield,
    MatchStick,
    SpearThrower,
    Blob,
    PeaFlower,
    Antidote,
    Quill,

}
