using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    public int goal;
    public bool disableObject;
    public GameObject interactedObject;
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
                interactedObject.SetActive(false);

            }
            else if(disableObject == false)
            {
                interactedObject.SetActive(true);

            }
        }
    }
}
