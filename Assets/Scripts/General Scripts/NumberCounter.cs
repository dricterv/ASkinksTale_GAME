using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    public int goal;
    public bool disableObject;
    public bool setActive = true;
    public bool gate = false;
    public Animator anim;
    public List<GameObject> interactedObjects = new List<GameObject>();
    public List<GameObject> animatedObject = new List<GameObject>();
    public string boolString;


    // public GameObject interactedObject;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        if(boolString != "" && boolString != null)
        {
            bool open = StatsManager.Instance.flags[boolString];
            if(open == true)
            {
                count = goal;
                AddToCount(0);
            }

            
        }
    }
    public void AddToCount(int amount)
    {
        count += amount;
        if(count >= goal)
        {
            if (disableObject == true && setActive == true)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    gameObject.SetActive(false);
                }
            }
            else if(disableObject == false && setActive == true)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    gameObject.SetActive(true);
                }
            }

            if (gate == true)
            {
                anim.Play("Opening");
                if (StatsManager.Instance.flags[boolString] == false)
                {
                    StatsManager.Instance.flags[boolString] = true;
                }
            }
        }
        else if (count < goal)
        {
            if (disableObject == true && setActive == true)
            {
                foreach (GameObject gameObject in interactedObjects)
                {
                    if (gameObject.activeSelf == false)
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
            else if (disableObject == false && setActive == true)
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
