using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;
    public Animator fadeAnim;
    public CameraMovement mainCamera;
    public GameObject player;
    public UIManager uiManager;
    public DialogueManager DialogueManager;
    public DialogueHistoryTracker DialogueHistoryTracker;
    public LocationHistoryTracker LocationHistoryTracker;
    public QuestManager questManager;
    public EventSystem eventSystem;


    private void Awake()
    {
        if(Instance != null)
        {
            CleanUpAndDestroy();
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
   

    private void Update() 
    {
       /* if(Input.GetKeyDown(KeyCode.Escape) && DialogueManager.isDialogueActive == false && DialogueManager.isButtonActive == false)
        {
            uiManager.MainMenuOn();
            SceneManager.LoadScene("MainMenu");
            //QuitGame();
        }*/
       
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2))
        {
            GameObject buttonGO = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }


     

    }

    private void MarkPersistentObjects()
    {
        foreach(GameObject obj in persistentObjects)
        {
            if(obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuLoad()
    {
        uiManager.MainMenuOn();
        SceneManager.LoadScene("MainMenu");
        player.GetComponent<PlayerHealth>().ChangeHealth(100);
    }
}
