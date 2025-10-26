using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public CanvasGroup canvasGroup;
    public Button[] choiceButtons;

    public bool isDialogueActive;
    public bool isButtonActive;

    private DialogueSO currentDialogue;
    private Dialogue currentNpc;
    private int dialogueIndex;

    private float lastDialogueEndTime;
    private float dialogueCooldown = .1f;

    private void Awake()
    {
       
        isDialogueActive = false;
        isButtonActive = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach(var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
    public bool CanStartDialogue()
    {
        return Time.unscaledTime - lastDialogueEndTime >= dialogueCooldown;
    }
    public void StartDialogue(DialogueSO dialogueSO, Dialogue npcDialogue)
    {
        if(dialogueSO == null)
        {
            return;
        }
        
        currentNpc = npcDialogue;
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
       
        ShowDialogue();
        isDialogueActive = true;
    }
    public void AdvanceDialogue()
    {
        if (Time.unscaledTime - lastDialogueEndTime < dialogueCooldown)
            return;
        if (dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            ShowChoices();
            
        }
    }
    private void ShowDialogue()
    {
        if(currentDialogue.lines.Length > 0)
        {
            DialogueLine line = currentDialogue.lines[dialogueIndex];
            //Debug.Log(dialogueIndex);
            if (line.logInteraction == true)
            {
                GameManager.Instance.DialogueHistoryTracker.RecordNpc(line.speaker);
            }

            if (portrait != null)
            {
                portrait.sprite = line.speaker.portrait;
            }

            if (line.speaker.actorName == null)
            {
                actorName.text = "Missing Name";
            }
            else
            {
                actorName.text = line.speaker.actorName;
            }

            if (line.text == null)
            {
                dialogueText.text = "Error: Missing Dialogue";
            }
            else
            {
                dialogueText.text = line.text;
            }
            if (line.changeAnim == true)
            {
                currentNpc.anim.Play(line.animName);
            }
            lastDialogueEndTime = Time.unscaledTime;

            dialogueIndex++;
        }
        
    }

    private void ShowChoices()
    {
        ClearChoices();
        isButtonActive = true;
        if (currentDialogue.options.Length > 0)
        {
            for(int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                if(option.questToEnd != null)
                {
                    if(GameManager.Instance.questManager.IsQuestComplete(option.questToEnd) == true)
                    {
                        choiceButtons[i].gameObject.SetActive(true);
                        choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue, option.questToGive, option.questToEnd));
                    }
                    
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue, option.questToGive, option.questToEnd));
                }
                
              
            }
            EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
        }
        else
        {
            EndDialogue();
            /*
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "*Leave*";
            choiceButtons[0].gameObject.SetActive(true);

            choiceButtons[0].onClick.AddListener(EndDialogue);
            */
        }
        lastDialogueEndTime = Time.unscaledTime;

    }

    private void ChooseOption(DialogueSO dialogueSO, QuestSO questStartSO, QuestSO questEndSO)
    {
        if(dialogueSO == null)
        {
            EndDialogue();
        }
        else
        {
            if(dialogueSO.giveItems != null)
            {
                InventoryManager.Instance.AddInventoryItem(dialogueSO.giveItems, dialogueSO.giveItems.slot );
            }
            if (questStartSO != null)
            {
               // Debug.Log("quest sent");
                
                QuestEvents.OnQuestOfferRequested?.Invoke(questStartSO);
                //Debug.Log("quest given");
           
            }
            if (questEndSO != null)
            {
                //Debug.Log("quest sent");

                QuestEvents.OnQuestEnd?.Invoke(questEndSO);
               // Debug.Log("quest given");

            }

            ClearChoices();
            isButtonActive = false;

            StartDialogue(dialogueSO, currentNpc);
            
            
        }
    }
    private void EndDialogue()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        dialogueIndex = 0;
        ClearChoices();
        isButtonActive = false;
        lastDialogueEndTime = Time.unscaledTime;

        isDialogueActive = false;
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {


            button.gameObject.SetActive(false);

            button.onClick.RemoveAllListeners();

        }
    }
}
