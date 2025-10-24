using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;

    [SerializeField] private TMP_Text questNameText;

    [SerializeField] private TMP_Text questDescriptionText;

    [SerializeField] private QuestObjectiveSlot[] objectiveSlots;

    [SerializeField] private QuestLogSlot[] questSlots;

    
    private QuestSO questSO;

    private void OnEnable()
    {
        QuestEvents.OnQuestOfferRequested += AddQuest;
    }
    private void OnDisable()
    {
        QuestEvents.OnQuestOfferRequested -= AddQuest;
    }
    

    public void HandleQuestClicked(QuestSO questSO)
    {
        this.questSO = questSO;
        GameManager.Instance.uiManager.OpenQuestDetailsMenu();
        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescirption;

        DisplayObjective();

        foreach (var objective in questSO.objectives)
        {
            Debug.Log($"Objective: {objective.description} => {questManager.GetProgressText(questSO, objective)}");
        }
    }

   

    public void AddQuest(QuestSO questSO)
    {
        Debug.Log("quest Received");
        Debug.Log("name: " + this.gameObject.name);
        questManager.AcceptQuest(questSO);
        Debug.Log("quest a");
        RefreshQuestList();
        Debug.Log("quest w");
    }
    public void RefreshQuestList()
    {
        List<QuestSO> activeQuests = questManager.GetActiveQuests();
        for (int i = 0; i< questSlots.Length; i++)
        {
            if(i < activeQuests.Count)
            {
                questSlots[i].SetQuest(activeQuests[i]);
            }
            else
            {
                questSlots[i].ClearSlot();
            }
        }
    }
    private void DisplayObjective()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if(i < questSO.objectives.Count)
            {
                var objective = questSO.objectives[i];
                questManager.UpdateObjectiveProgress(questSO, objective);

                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;
                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);


            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }

}
