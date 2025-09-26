using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogueStart : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            GameManager.Instance.DialogueManager.StartDialogue(GetComponent<Dialogue>().currentConversation);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            Destroy(gameObject);
    }
}
