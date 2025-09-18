using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHistoryTracker : MonoBehaviour
{

    private readonly HashSet<LocationSO> visitedLocations = new HashSet<LocationSO>();


  


    public void RecordLocation(LocationSO locationSO)
    {
        if (visitedLocations.Add(locationSO))
        {
            Debug.Log("visited " + locationSO.displayName);
        }
    }

    public bool HasVisited(LocationSO locationSO)
    {
        return visitedLocations.Contains(locationSO);
    }
}
