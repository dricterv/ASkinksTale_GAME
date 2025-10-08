using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;

    public List<DialogueSO> conversations;
    public DialogueSO currentConversation;


    private void Awake()
    {
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        if(anim != null)
        {
            anim.Play("Idle");

        }
        if(interactAnim != null)
            interactAnim.Play("Open");
    }
    private void OnDisable()
    {
        if (interactAnim != null)
            interactAnim.Play("Close");

    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckForNewConversation()
    {
        for(int i = 0; i < conversations.Count; i++)
        {
            //can reverse for loop if wanting to skip to latest unlocked dialogue // for(int i = conversations.Count -1; i >= 0; i--)
            var convo = conversations[i];
            if(convo != null && convo.IsConditionMet())
            {
                currentConversation = convo;

                //remove if onetime
                if(convo.removeAfterPlay)
                {
                    conversations.RemoveAt(i);
                }
                //remove quest dialogue
                if (convo.removeTheseOnPlay != null && convo.removeTheseOnPlay.Count > 0)
                {
                    foreach (var toRemove in convo.removeTheseOnPlay)
                    {
                        conversations.Remove(toRemove);
                    }
                }
                
                break;
            }
        }
    }
}
