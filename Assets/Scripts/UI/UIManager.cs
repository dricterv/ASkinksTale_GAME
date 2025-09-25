using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    
    public CanvasGroup liveCanvas;
    public CanvasGroup pauseCanvas;
    public CanvasGroup mainMenuCanvas;

    public GameObject inventoryStartButton;
    public GameObject startButton;
   

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            mainMenuCanvas.alpha = 1;
            mainMenuCanvas.interactable = true;
            mainMenuCanvas.blocksRaycasts = false;
            liveCanvas.alpha = 0;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
            EventSystem.current.SetSelectedGameObject(startButton);
            //Time.timeScale = 0;
            GameManager.Instance.player.SetActive(false);
        }
        else
        {
            liveCanvas.alpha = 1;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
            mainMenuCanvas.alpha = 0;
            mainMenuCanvas.interactable = false;
            mainMenuCanvas.blocksRaycasts = false;
            EventSystem.current.SetSelectedGameObject(inventoryStartButton);
        }
        

        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && mainMenuCanvas.alpha != 1)
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

    public void LiveUIOn()
    {
        liveCanvas.alpha = 1;
        liveCanvas.interactable = false;
        liveCanvas.blocksRaycasts = false;
        mainMenuCanvas.alpha = 0;
        mainMenuCanvas.interactable = false;
        mainMenuCanvas.blocksRaycasts = false;
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(inventoryStartButton);
    }

    public void MainMenuOn()
    {
        mainMenuCanvas.alpha = 1;
        mainMenuCanvas.interactable = true;
        mainMenuCanvas.blocksRaycasts = false;
        liveCanvas.alpha = 0;
        liveCanvas.interactable = false;
        liveCanvas.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(startButton);
        //Time.timeScale = 0;
        GameManager.Instance.player.SetActive(false);
    }

    public void SetEventSystemGO(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
}
