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
        SceneManager.LoadScene(sceneToLoad);

    }

}
