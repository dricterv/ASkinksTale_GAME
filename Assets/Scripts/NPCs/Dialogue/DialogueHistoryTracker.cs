using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{

    private readonly HashSet<ActorSO> spokenNPCs = new HashSet<ActorSO>();


  


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
