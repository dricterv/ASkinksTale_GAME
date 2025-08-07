using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Fields")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Item Variables")]
  
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        
        if(thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            if(thisItem.numberHeld == -10)
            {
                itemNumberText.text = "";
            }
            else
            {
                itemNumberText.text = "" + thisItem.numberHeld;
            }
        }
    }

   public void ClickedOn()
    {
        if(thisItem)
        {
            thisManager.SetupDescriptionNameAndButtons(thisItem.itemDescription, thisItem.itemName, thisItem.isUsable, thisItem);
        }
    }
}
