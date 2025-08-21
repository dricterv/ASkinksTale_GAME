using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSO dialogueSO;


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
        anim.Play("Idle");
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
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            {
                DialogueManager.Instance.StartDialogue(dialogueSO);
            }
        }
    }
}
