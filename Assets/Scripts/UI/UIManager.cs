using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{

    public CanvasGroup liveCanvas;
    public CanvasGroup inventoryCanvas;
    public CanvasGroup mainMenuCanvas;
    public CanvasGroup pauseCanvas;
    public CanvasGroup journalCanvas;
    public CanvasGroup questDetailsCanvas;


    public GameObject inventoryStartButton;
    public GameObject pauseMenuStartButton;
    public GameObject jounalStartButton;
    public GameObject startButton;
    public Image item1;
    public Image item2;
    public Image item1Inventory;
    public Image item2Inventory;
    public bool inventoryOpen;

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

        inventoryOpen = false;
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && mainMenuCanvas.alpha != 1)
        {
            ToggleInventory();
        }
        if (Input.GetButtonDown("Menu") && mainMenuCanvas.alpha != 1)
        {
            if(liveCanvas.alpha == 1 && GameManager.Instance.DialogueManager.isDialogueActive == false)
            {
                OpenPauseMenu();
                inventoryOpen = true;
                Time.timeScale = 0;
                liveCanvas.alpha = 0;
                liveCanvas.interactable = false;
                liveCanvas.blocksRaycasts = false;
                EventSystem.current.SetSelectedGameObject(pauseMenuStartButton);
            }
            else if(liveCanvas.alpha == 0)
            {
                LiveUIOn();
            }
        }
    }

    public void ToggleInventory()
    {
        if (liveCanvas.alpha == 1 && GameManager.Instance.DialogueManager.isDialogueActive == false)
        {
            inventoryOpen = true;
            Time.timeScale = 0;
            inventoryCanvas.alpha = 1;
            inventoryCanvas.interactable = true;
            inventoryCanvas.blocksRaycasts = true;
            liveCanvas.alpha = 0;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
            EventSystem.current.SetSelectedGameObject(inventoryStartButton);
        }
        else if (liveCanvas.alpha == 0)
        {
            Time.timeScale = 1;
            inventoryCanvas.alpha = 0;
            inventoryCanvas.interactable = false;
            inventoryCanvas.blocksRaycasts = false;
            liveCanvas.alpha = 1;
            liveCanvas.interactable = false;
            liveCanvas.blocksRaycasts = false;
            inventoryOpen = false;
            pauseCanvas.alpha = 0;
            pauseCanvas.interactable = false;
            pauseCanvas.blocksRaycasts = false;
            journalCanvas.alpha = 0;
            journalCanvas.interactable = false;
            journalCanvas.blocksRaycasts = false;
            questDetailsCanvas.alpha = 0;
            questDetailsCanvas.interactable = false;
            questDetailsCanvas.blocksRaycasts = false;

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
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;
        questDetailsCanvas.alpha = 0;
        questDetailsCanvas.interactable = false;
        questDetailsCanvas.blocksRaycasts = false;
        Time.timeScale = 1;
        inventoryOpen = false;

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
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        inventoryOpen = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;
        Time.timeScale = 1;

        EventSystem.current.SetSelectedGameObject(startButton);
        //Time.timeScale = 0;
        GameManager.Instance.player.SetActive(false);
    }

    public void SetEventSystemGO(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }

    public void OpenPauseMenu()
    {
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        pauseCanvas.alpha = 1;
        pauseCanvas.interactable = true;
        pauseCanvas.blocksRaycasts = true;
        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;

    }
    public void OpenInventoryMenu()
    {
        inventoryCanvas.alpha = 1;
        inventoryCanvas.interactable = true;
        inventoryCanvas.blocksRaycasts = true;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;
    }
    public void OpenJournalMenu()
    {
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        journalCanvas.alpha = 1;
        journalCanvas.interactable = true;
        journalCanvas.blocksRaycasts = true;
    }

    public void OpenQuestDetailsMenu()
    {
        questDetailsCanvas.alpha = 1;
    }
}
