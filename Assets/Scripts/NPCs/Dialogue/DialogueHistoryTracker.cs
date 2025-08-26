using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{
    public static DialogueHistoryTracker Instance;

    private readonly HashSet<ActorSO> spokenNPCs = new HashSet<ActorSO>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }


    public void RecordNpc(ActorSO actorSO)
    {
        spokenNPCs.Add(actorSO);
        Debug.Log("spoke to " + actorSO.actorName);
    }

    public bool HasSpokenWith(ActorSO actorSO)
    {
        return spokenNPCs.Contains(actorSO);
    }


}
