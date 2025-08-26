using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVistedTrigger : MonoBehaviour
{
    [SerializeField] private LocationSO locationVisited;
    [SerializeField] private bool destroyOnTouch = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            LocationHistoryTracker.Instance.RecordLocation(locationVisited);
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
        }
    }
}
