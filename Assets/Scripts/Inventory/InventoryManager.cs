using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButtonOne;
    [SerializeField] private GameObject useButtonTwo;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable selectedButton;

    public InventoryItem currentItem;

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
        
        GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
        temp.transform.SetParent(inventoryPanel.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);

        InventorySlot newSlot = temp.GetComponent<InventorySlot>();
        newSlot.Setup(item, this);
    }
    void MakeInventorySlots()
    {
        if(playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                temp.transform.localScale = new Vector3(1, 1, 1);

                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                newSlot.Setup(playerInventory.myInventory[i], this);
                if (newSlot.thisItem.isUsable && newSlot.thisItem.thisItem != EquippedItem.EmptyItem)
                {
                    Debug.Log("selected");
                    newSlot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    newSlot.GetComponent<SetUIToMoveTo>().elementToSelect = selectedButton;
                }
                else
                {
                     newSlot.GetComponent<SetUIToMoveTo>().eventSystem = eventSystem;
                    newSlot.GetComponent<SetUIToMoveTo>().elementToSelect = newSlot.GetComponent<Button>();
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
}
