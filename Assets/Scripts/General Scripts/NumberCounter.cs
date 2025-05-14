using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    public int goal;
    public bool disableObject;
    public List<GameObject> interactedObjects = new List<GameObject>();
   // public GameObject interactedObject;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }
    public void AddToCount(int amount)
    {
        count += amount;
        if(count >= goal)
        {
            if(disableObject == true)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    gameObject.SetActive(false);
                }
            }
            else if(disableObject == false)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    gameObject.SetActive(true);
                }
            }
        }
        else if (count < goal)
        {
            if (disableObject == true)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    if (gameObject.activeSelf == false)
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
            else if (disableObject == false)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    if (gameObject.activeSelf == true)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
