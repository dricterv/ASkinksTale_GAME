using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public EquippedItem thisItem;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool isUsable;
    public bool isUnique;
    public UnityEvent thisEventOne;
    public UnityEvent thisEventTwo;
    public int slot;


    public void EquipOne()
    {
        //Debug.Log("Equiping item");
        //thisEventOne.Invoke();
        StatsManager.Instance.UpdateEquipedItemOne(this);
    }
    public void EquipTwo()
    {
       // Debug.Log("Equiping item");
        //thisEventTwo.Invoke();
        StatsManager.Instance.UpdateEquipedItemTwo(this);

    }
}
