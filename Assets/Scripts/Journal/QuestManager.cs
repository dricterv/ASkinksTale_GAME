using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();

    public List<QuestSO> GetActiveQuests()
    {
        return new List<QuestSO>(questProgress.Keys);
    }

    public void AcceptQuest(QuestSO questSO)
    {
        questProgress[questSO] = new Dictionary<QuestObjective, int>();

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }
    }
    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective)
    {
         
        var progressDictionary = questProgress[questSO];
        int newAmount = 0;
        if(objective.targetItem != null)
        {
            //Debug.Log("item required");
            newAmount = InventoryManager.Instance.GetItemQuantity(objective.targetItem);
        }
        else if(objective.targetLocation != null && GameManager.Instance.LocationHistoryTracker.HasVisited(objective.targetLocation))
        {
            newAmount = objective.requiredAmount;
        }
        else if (objective.targetNPC != null && GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(objective.targetNPC))
        {
            newAmount = objective.requiredAmount;
        }
        else if (objective.targetFlag != "" && StatsManager.Instance.flags[objective.targetFlag])
        {
            newAmount = objective.requiredAmount;
        }

        progressDictionary[objective] = newAmount;
        
    }

    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);
        if (currentAmount >= objective.requiredAmount)
        {
            return "Complete";
        }
        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredAmount}";
        else
            return "in progress";
    }

    public int GetCurrentAmount(QuestSO questSO, QuestObjective objective)
    {
        if(questProgress.TryGetValue(questSO, out var objectiveDictionary))
        {
            if(objectiveDictionary.TryGetValue(objective, out int amount))
            {
                Debug.Log("waw");
                return amount;
            }
            return 0;
        }
        return 0;

    }
}
