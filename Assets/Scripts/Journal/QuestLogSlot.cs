using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;

    public QuestSO currentQuest;
    public QuestLogUI questLogUI;

    private void OnValidate()
    {
        if(currentQuest != null)
        {
            SetQuest(currentQuest);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        gameObject.SetActive(true);
       //questDescriptionText.text = questSO.questDescirption

    }

    public void ClearSlot()
    {
        currentQuest = null;
        gameObject.SetActive(false);
    }
    public void OnSlotClicked()
    {
        questLogUI.HandleQuestClicked(currentQuest);
    }
}
