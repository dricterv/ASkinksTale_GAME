using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetUIToMoveTo : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField]private Selectable elementToSelect;
    // Start is called before the first frame update
    

    public void JumpToElement()
    {
        if(eventSystem == null)
        {
            Debug.Log("EventSystem Missing");
        
        } 
       
       eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }
}
