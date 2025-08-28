using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    
    public CanvasGroup liveCanvas;
    public CanvasGroup pauseCanvas;
    public GameObject inventoryStartButton;
    // Start is called before the first frame update
    void Start()
    {
        liveCanvas.alpha = 1;
        liveCanvas.interactable = false;
        liveCanvas.blocksRaycasts = false;

        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (liveCanvas.alpha == 1)
        {
            Time.timeScale = 0;
            pauseCanvas.alpha = 1;
            pauseCanvas.interactable = true;
            pauseCanvas.blocksRaycasts = true;
            liveCanvas.alpha = 0;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
            EventSystem.current.SetSelectedGameObject(inventoryStartButton);
        }
        else if (liveCanvas.alpha == 0)
        {
            Time.timeScale = 1;
            pauseCanvas.alpha = 0;
            pauseCanvas.interactable = false;
            pauseCanvas.blocksRaycasts = false;
            liveCanvas.alpha = 1;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
        }
    }
}
