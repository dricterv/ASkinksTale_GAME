using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    public QuestSO questToGive;
    [Header("Conditional Requirements (Optional)")]

    public ActorSO[] requiredNPCs;

    public LocationSO[] requiredLocations;

    public InventoryItem[] requiredItems;
    public string[] requiredFlags;

    public InventoryItem[] removeItems;
    public InventoryItem giveItems;
    public string currentAnimName;
    


    [Header("Control Flags")]
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
}