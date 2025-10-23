using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "QuestSO")]
public class QuestSO : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescirption;

    public List<QuestObjective> objectives;
}

[System.Serializable]
public class QuestObjective
{
    public string description;
    [SerializeField] private Object target;

    public InventoryItem targetItem => target as InventoryItem;
    public ActorSO targetNPC => target as ActorSO;
    public LocationSO targetLocation => target as LocationSO;
    public string targetFlag;

    public int requiredAmount;

}
