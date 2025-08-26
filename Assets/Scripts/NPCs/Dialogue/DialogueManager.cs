using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [Header("UI References")]
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public CanvasGroup canvasGroup;
    public Button[] choiceButtons;

    public bool isDialogueActive;
    public bool isButtonActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
    public void StartDialogue(DialogueSO dialogueSO)
    {

        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        ShowDialogue();
        isDialogueActive = true;
    }
    public void AdvanceDialogue()
    {
        if(dialogueIndex < currentDialogue.lines.Length)
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
        DialogueLine line = currentDialogue.lines[dialogueIndex];
        Debug.Log(dialogueIndex);
        DialogueHistoryTracker.Instance.RecordNpc(line.speaker);

        if(portrait != null)
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

        dialogueIndex++;
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
                choiceButtons[i].gameObject.SetActive(true);
                
                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));

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
        
    }

    private void ChooseOption(DialogueSO dialogueSO)
    {
        if(dialogueSO == null)
        {
            EndDialogue();
        }
        else
        {
            ClearChoices();
            isButtonActive = false;

            StartDialogue(dialogueSO);

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
