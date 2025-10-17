using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogueStart : MonoBehaviour
{

    public string boolString;
    private void Awake()
    {
        if (boolString != "")
        {
            bool started = StatsManager.Instance.flags[boolString];
            if (started == true)
            {
                gameObject.SetActive(false);
            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            GameManager.Instance.DialogueManager.StartDialogue(GetComponent<Dialogue>().currentConversation, GetComponent<Dialogue>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            if (StatsManager.Instance.flags[boolString] == false)
            {
                StatsManager.Instance.flags[boolString] = true;
            }
        }
    }
}
