using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public InventoryItem emptyItem;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            fadeAnim = GameManager.Instance.fadeAnim;
            

            //player = collision.GetComponentInParent<Transform>();
            //fadeAnim = GameManager.Instance.GetComponentInChildren

            StartCoroutine(DelayFade());
        }
    }

    IEnumerator DelayFade()
    {
        fadeAnim.Play("FadeToWhite");
        yield return new WaitForSeconds(fadeTime);
        GameManager.Instance.mainCamera.maxPosition = maxPosition;
        GameManager.Instance.mainCamera.minPosition = minPosition;
        player.position = newPlayerPosition;
        GameManager.Instance.uiManager.LiveUIOn();
        if (player.gameObject.activeSelf == false)
        {
            GameManager.Instance.player.SetActive(true);
        }
        SceneManager.LoadScene(sceneToLoad);
        
    }

    public void StartGame()
    {
        player = GameManager.Instance.player.transform;
        fadeAnim = GameManager.Instance.fadeAnim;
        GameManager.Instance.player.SetActive(true);
        GameManager.Instance.player.GetComponent<PlayerHealth>().ChangeHealth(100);
        StatsManager.Instance.ResetFlags();
        InventoryManager.Instance.AddInventoryItem(emptyItem, 3);
        InventoryManager.Instance.AddInventoryItem(emptyItem, 2);
        InventoryManager.Instance.AddInventoryItem(emptyItem, 4);
        InventoryManager.Instance.AddInventoryItem(emptyItem, 5);
        InventoryManager.Instance.AddInventoryItem(emptyItem, 6);
        InventoryManager.Instance.AddInventoryItem(emptyItem, 7);
        InventoryManager.Instance.MainMenuEquip();


        //GameManager.Instance.uiManager.LiveUIOn();

        StartCoroutine(DelayFade());
    }

    public void QuitButton()
    {
        GameManager.Instance.QuitGame();
    }
    public void MainMenuButton()
    {
        GameManager.Instance.MainMenuLoad();
    }
}
