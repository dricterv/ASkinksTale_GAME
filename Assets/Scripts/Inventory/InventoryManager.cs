using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    public InventorySlot[] inventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButtonOne;
    [SerializeField] private GameObject useButtonTwo;
    [SerializeField] private GameObject equipedItemUIOne;
    [SerializeField] private GameObject equipedItemUITwo;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable selectedButton;


    public InventoryItem currentItem;
    [SerializeField] private InventoryItem startItem1;
    [SerializeField] private InventoryItem startItem2;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    public void SetTextAndButton(string description, string name, bool isButtonActive)
    {
        descriptionText.text = description;
        nameText.text = name;
        if(isButtonActive)
        {
            useButtonOne.SetActive(true);
            useButtonTwo.SetActive(true);
            

        }
        else
        {
            useButtonOne.SetActive(false);
            useButtonTwo.SetActive(false);
           

        }
    }

    public void AddInventoryItem(InventoryItem item, int slot)
    {

        inventory[slot].Setup(item, this);
        if (item.isUsable && item.thisItem != EquippedItem.EmptyItem)
        {
            //Debug.Log("selected");
            inventory[slot].GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
            inventory[slot].GetComponent<SetUIToMoveTo>().elementToSelect = selectedButton;
        }
        else
        {
            inventory[slot].GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
            inventory[slot].GetComponent<SetUIToMoveTo>().elementToSelect = inventory[slot].GetComponent<Button>();
        }
        
    }
    void MakeInventorySlots()
    {
        /*
        if(playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.name = ("InventorySlot" + i);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                newSlot.Setup(playerInventory.myInventory[i], this);
                
                if (newSlot.thisItem.isUsable && newSlot.thisItem.thisItem != EquippedItem.EmptyItem)
                {
                    //Debug.Log("selected");
                    newSlot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    newSlot.GetComponent<SetUIToMoveTo>().elementToSelect = selectedButton;
                }
                else
                {
                     newSlot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    newSlot.GetComponent<SetUIToMoveTo>().elementToSelect = newSlot.GetComponent<Button>();
                }

            }
            
        } */

        foreach( var slot in inventory)
        {
            if(slot.thisItem != null)
            {
                slot.Setup(slot.thisItem, this);
                if (slot.thisItem.isUsable && slot.thisItem.thisItem != EquippedItem.EmptyItem)
                {
                    //Debug.Log("selected");
                    slot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    slot.GetComponent<SetUIToMoveTo>().elementToSelect = selectedButton;
                }
                else
                {
                    slot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    slot.GetComponent<SetUIToMoveTo>().elementToSelect = slot.GetComponent<Button>();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTextAndButton("", "", false);
        MakeInventorySlots();

    }

    public void SetupDescriptionNameAndButtons(string newDescriptionString, string newNameString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        nameText.text = newNameString;
        descriptionText.text = newDescriptionString;
        useButtonOne.SetActive(isButtonUsable);
        useButtonTwo.SetActive(isButtonUsable);
       
    }

    public void EquipOneButtonPress()
    {
        if(currentItem)
        {
            currentItem.EquipOne();
        }
    }
    public void EquipTwoButtonPress()
    {
        if (currentItem)
        {
            currentItem.EquipTwo();
        }
    }
    public void MainMenuEquip()
    {
        startItem1.EquipOne();
        startItem2.EquipTwo();
    }


    public bool HasItem(InventoryItem itemSO)
    {
        foreach (var slot in inventory)
        {
           // Debug.Log("loop");

            if (slot.thisItem == itemSO)
            {
                //Debug.Log("w");
                return true;

            }

        }
        //if(playerInventory.myInventory.Contains(itemSO) &&)
        return false;
    }

    public int GetItemQuantity(InventoryItem itemSO)
    {
        int total = 0;
        foreach( var slot in inventory)
        {
            if (slot.thisItem == itemSO )
            {
                total += slot.thisItem.numberHeld;
                Debug.Log("wa");
            }
        }
        return total;
    }
}
