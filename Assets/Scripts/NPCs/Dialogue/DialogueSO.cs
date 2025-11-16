using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    
    [Header("Conditional Requirements (Optional)")]

    public ActorSO[] requiredNPCs;

    public LocationSO[] requiredLocations;

    public InventoryItem[] requiredItems;
    public QuestSO[] requiredQuests;
    public string[] requiredFlags;

    public InventoryItem[] removeItems;
    public InventoryItem giveItems;
    public string currentAnimName;



    [Header("Control Flags")]
    public bool isCutscene = false;
    public bool removeAfterPlay;
    public List<DialogueSO> removeTheseOnPlay;


    public bool IsConditionMet()
    {
        if(requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                if(!GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }
        if (requiredLocations.Length > 0)
        {
            foreach (var location in requiredLocations)
            {
                if (!GameManager.Instance.LocationHistoryTracker.HasVisited(location))
                {
                    return false;
                }
            }
        }
        if (requiredItems.Length > 0)
        {
            foreach (var item in requiredItems)
            {
                if (!InventoryManager.Instance.HasItem(item))
                {
                    return false;
                }
            }
        }
        if (requiredFlags.Length > 0)
        {
            foreach (var flag in requiredFlags)
            {
                //Debug.Log("flag");
                if (StatsManager.Instance.flags[flag] == false)
                {
                    //Debug.Log("flag2");
                    return false;
                }
            }
        }
        if (requiredQuests.Length > 0)
        {
            foreach (var quest in requiredQuests)
            {
                if (!GameManager.Instance.questManager.CurrentlyHaveQuest(quest))
                {
                    return false;
                }
            }
        }
        //Debug.Log("flag3");
        return true;
    }
}

[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;
    public bool logInteraction = false;
    public bool changeAnim = false;
    public string animName;

    
}
[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public DialogueSO nextDialogue;
    public QuestSO questToGive;
    public QuestSO questToEnd;
    public string flagToFlag;
}