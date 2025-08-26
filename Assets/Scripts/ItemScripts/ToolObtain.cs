using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObtain : MonoBehaviour
{
    public GameObject player;
    public string scriptName;
    public ItemText itemText;
    public InventoryItem item;
    public int slot;
    public InventoryManager inventoryManager;
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(itemText != null)
                itemText.DisplayText();

            if(player != null)
            {
                if (scriptName != "")
                {
                    (coll.gameObject.GetComponent(scriptName) as MonoBehaviour).enabled = true;
                }
                if (item != null)
                {
                    InventoryManager.Instance.AddInventoryItem(item, slot);
                }
            }
            
            Destroy(this.gameObject);
        }
    }

}
